using poetools.Runtime.Systems;
using UnityEngine;

[CreateAssetMenu]
public class SourceMovement : MovementStrategy
{
    #region Inspector

        [Header("Settings")]
        [SerializeField] public float noFrictionJumpWindow = 0.1f;
        [SerializeField] public float friction = 5.5f;
        [SerializeField] public float airAcceleration = 40f;
        [SerializeField] public float groundAcceleration = 50f;
        [SerializeField] public float maxAirSpeed = 1f;
        [SerializeField] public float maxGroundSpeed = 1f;    

    #endregion
    
    #region Variables

        private Context _context;
        private Vector3 _velocity;

    #endregion

    public override Vector3 UpdateVelocity(Context context)
    {
        _context = context;
        _velocity = context.CurrentVelocity;
        
        return IsGrounded ? MoveGround() : MoveAir();
    }
    
    #region Properties

        private bool HasInput => _context.TargetDirection != Vector3.zero;
        private float Speed => _context.CurrentSpeed;
        private Vector3 Direction => _context.TargetDirection;
        private float TimeSpentOnGround => _context.Data.TimeSpentGrounded;
        private bool IsGrounded => _context.Data.IsGrounded;

    #endregion

    #region Methods

        // todo: friction imp here is still subpar - I don't like how it limits max speed    
    
        private Vector3 MoveGround()
        {
            if (Speed != 0 && TimeSpentOnGround > noFrictionJumpWindow)
            {
                float drop = Speed * friction * Time.deltaTime;
                float originalY = _velocity.y;
                _velocity *= Mathf.Max(Speed - drop, 0) / Speed;
                _velocity.y = originalY;
            }

            return Accelerate(groundAcceleration, maxGroundSpeed * _context.SpeedMultiplier);
        }

        private Vector3 MoveAir()
        {
            return Accelerate(airAcceleration, maxAirSpeed * _context.SpeedMultiplier);
        }
        
        private Vector3 Accelerate(float acceleration, float maxVelocity)
        {
            float projVel = Vector3.Dot(_velocity, Direction.normalized);
            float accelVel = acceleration * Time.deltaTime;

            if (projVel + accelVel > maxVelocity)
                accelVel = Mathf.Max(maxVelocity - projVel, 0);

            return _velocity + Direction * accelVel;
        }

    #endregion
}