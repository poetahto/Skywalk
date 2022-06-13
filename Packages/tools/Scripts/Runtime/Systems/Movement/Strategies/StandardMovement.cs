using UnityEngine;

namespace poetools.Runtime.Systems
{
    [CreateAssetMenu]
    public class StandardMovement : MovementStrategy
    {
        #region Inspector

            [Header("General Settings")]
            [SerializeField] private float speed = 3f;
            [SerializeField] private float groundedAcceleration = 0.125f;
            [SerializeField] private float groundedDeceleration = 0.2f;
            [SerializeField] private float airborneAcceleration = 0.6f;
            [SerializeField] private float airborneDeceleration = 0.6f;
            [SerializeField] private float decelerationRampPercent = 0.5f;

        #endregion

        #region Variables

            private Context _context;
        
        #endregion
        
        public override Vector3 UpdateVelocity(Context context)
        {
            _context = context;

            if (HasInput || IsGrounded == false)
                return CalculateConstantAcceleration();

            return CalculateSmoothStop();
        }
        
        #region Properties

            private bool HasInput => _context.TargetDirection != Vector3.zero;

            private bool IsGrounded => _context.Data.IsGrounded;

            private float TargetSpeed => _context.SpeedMultiplier * speed;

        #endregion

        #region Methods

            private Vector3 CalculateConstantAcceleration()
            {
                Vector3 currentVelocity = _context.CurrentVelocity;
                Vector3 targetVelocity = CalculateTargetVelocity();
                float acceleration = CalculateAcceleration();

                return Vector3.MoveTowards(currentVelocity, targetVelocity, acceleration);
            }
        
            private Vector3 CalculateSmoothStop()
            {
                Vector3 originalVelocity = _context.CurrentVelocity;
                Vector3 smoothedVelocity = originalVelocity;

                float deceleration = decelerationRampPercent * Time.deltaTime;
                smoothedVelocity.x -= smoothedVelocity.x * deceleration;
                smoothedVelocity.z -= smoothedVelocity.z * deceleration;

                return smoothedVelocity;
            }

            private Vector3 CalculateTargetVelocity()
            {
                Vector3 direction = _context.TargetDirection;
                Vector3 targetVelocity = new Vector3(direction.x * TargetSpeed, 0, direction.z * TargetSpeed);
                targetVelocity = Vector3.ProjectOnPlane(targetVelocity, _context.Data.ContactNormal);
                return new Vector3(targetVelocity.x, _context.CurrentVelocity.y, targetVelocity.z);
            }

            private float CalculateAcceleration()
            {
                float groundedResult = HasInput
                    ? GetAccelerationDelta(groundedAcceleration)
                    : GetAccelerationDelta(groundedDeceleration); 
                
                float airborneResult = HasInput 
                    ? GetAccelerationDelta(airborneAcceleration) 
                    : GetAccelerationDelta(airborneDeceleration);

                return _context.Data.IsGrounded ? groundedResult : airborneResult;
            }
        
            private float GetAccelerationDelta(float acceleration)
            {
                return 1 / acceleration * TargetSpeed * Time.deltaTime;
            }

        #endregion
    }
}