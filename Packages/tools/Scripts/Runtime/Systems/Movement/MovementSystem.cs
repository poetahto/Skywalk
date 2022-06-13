using UnityEngine;

namespace poetools.Runtime.Systems
{
    /// <summary>
    /// Moves a Rigidbody in a direction.
    /// </summary>
    
    public class MovementSystem : MonoBehaviour
    {
        #region Inspector

            [SerializeField] 
            private bool showDebug;
            
            [SerializeField]
            private MovementStrategy movementSettings;

            [Header("References")] 
            
            [SerializeField]
            private Transform lookDirection;
            
            [SerializeField] 
            [Tooltip("The rigidbody that our movement force is applied to.")]
            private Rigidbody targetRigidbody;
        
            [SerializeField] 
            [Tooltip("Used to determine whether we are grounded or not.")]
            private CollisionData collisionData;

        #endregion

        private void Update()
        {
            MovementStrategy.Context context = GenerateContext(); 
            targetRigidbody.velocity = movementSettings.UpdateVelocity(context);
        }
        
        #region Properties

            public float SpeedMultiplier { get; set; } = 1;
            
            public Vector3 TargetDirection { get; set; }
        
            public float ForwardSpeedMultiplier { get; set; } = 1;

            public float CurrentRunningSpeed
            {
                get
                {
                    Vector3 velocity = targetRigidbody.velocity;
                    velocity.y = 0;
                    return velocity.magnitude;
                }
            }

        #endregion
        
        #region Methods

            private MovementStrategy.Context GenerateContext()
            {
                Vector3 multiplier = CalculateForwardSpeedMultiplier(TargetDirection);

                return new MovementStrategy.Context
                {
                    Data = collisionData,
                    CurrentVelocity = targetRigidbody.velocity,
                    CurrentSpeed = CurrentRunningSpeed,
                    SpeedMultiplier = SpeedMultiplier,
                    TargetDirection = TargetDirection + multiplier
                };
            }
        
            private Vector3 CalculateForwardSpeedMultiplier(Vector3 targetDirection)
            {
                Vector3 forward = lookDirection.forward;
                float forwardSpeed = Vector3.Dot(targetDirection, forward);
                
                if (forwardSpeed > 0.1f)
                    return forward * (forwardSpeed * (ForwardSpeedMultiplier - 1));

                return Vector3.zero;
            }

        #endregion

        #region Debug

            private void OnGUI()
            {
                if (showDebug)
                {
                    GUILayout.Label($"Current Speed: {CurrentRunningSpeed:F1}");
                    GUILayout.Label($"Forward Speed Multiplier: {ForwardSpeedMultiplier:F1}");
                    GUILayout.Label($"Target Direction: {TargetDirection}");
                    
                    ForwardSpeedMultiplier = 
                        GUILayout.HorizontalSlider(ForwardSpeedMultiplier, 0, 2);
                }
            }

        #endregion
    }
}