using UnityEngine;

namespace poetools.Runtime.Systems
{
    [CreateAssetMenu]
    public class FreecamMovement : MovementStrategy
    {
        #region Inspector

            [Header("General Settings")]
            [SerializeField] private float speed = 3f;
            [SerializeField] private float groundedAcceleration = 0.125f;
            [SerializeField] private float airborneAcceleration = 0.6f;
            [SerializeField] private float decelerationRampPercent = 0.5f;

        #endregion

        #region Variables

            private Context _context;

        #endregion
        
        public override Vector3 UpdateVelocity(Context context)
        {
            _context = context;

            if (HasInput)
                return CalculateConstantAcceleration();

            return CalculateSmoothStop();
        }
        
        #region Properties

            private bool HasInput => _context.TargetDirection != Vector3.zero;
            
            private Vector3 TargetVelocity => _context.TargetDirection * speed;
            
            private Vector3 CurrentVelocity => _context.CurrentVelocity;        

        #endregion

        #region Methods

            private Vector3 CalculateConstantAcceleration()
            {
                float acceleration = CalculateAcceleration();
                return Vector3.MoveTowards(CurrentVelocity, TargetVelocity, acceleration);
            }
        
            private Vector3 CalculateSmoothStop()
            {
                Vector3 originalVelocity = _context.CurrentVelocity;
                Vector3 deceleration = originalVelocity * decelerationRampPercent * Time.deltaTime;
                
                return originalVelocity - deceleration;
            }

            private float CalculateAcceleration()
            {
                float groundedResult = GetAccelerationDelta(groundedAcceleration);
                float airborneResult = GetAccelerationDelta(airborneAcceleration);
                
                return _context.Data.IsGrounded ? groundedResult : airborneResult;
            }
        
            private float GetAccelerationDelta(float acceleration)
            {
                return 1 / acceleration * speed * Time.deltaTime;
            }

        #endregion
    }
}