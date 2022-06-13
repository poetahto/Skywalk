using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace poetools.Runtime.Systems
{
    /// <summary>
    /// Allows a Rigidbody to use advanced jumping mechanics like coyote time and air jumping.
    /// </summary>

    public class JumpingSystem : MonoBehaviour
    {
        [Header("Settings")]
    
        [SerializeField] 
        private JumpSettings jumpSettings;
    
        [SerializeField]
        [Tooltip("Writes debugging info to the console.")]
        private bool showDebug;
    
        [Header("Dependencies")] 
    
        [SerializeField] 
        [Tooltip("The rigidbody to apply our jumping force to.")]
        private Rigidbody targetRigidbody;
    
        [SerializeField] 
        [Tooltip("Used to determine if we are grounded.")]
        private CollisionData collisionData;
    
        [SerializeField]
        [Tooltip("Controlled at runtime for special jumping effects, like holding to jump longer.")]
        private CustomGravity customGravity;
    
        [Space(20)]
    
        [SerializeField] 
        [Tooltip("Invoked when we jump.")]
        public UnityEvent onJump;
        
        // Internal State
    
        private bool _coyoteAvailable;
        private bool _groundJumpAvailable;
        private int _remainingAirJumps;

        private bool CoyoteAvailable => _coyoteAvailable && collisionData.TimeSpentFalling < jumpSettings.CoyoteTime;
        public JumpSettings JumpSettings => jumpSettings;
    
        // Methods

        // private void OnEnable()
        // {
        //     collisionData.onEnterCollision.AddListener(HandleEnterCollision);
        //     collisionData.onExitCollision.AddListener(HandleExitCollision);
        // }
        //
        // private void HandleExitCollision(Collider arg0)
        // {
        //     print("exit col");
        //     bool rising = targetRigidbody.velocity.y > 0;
        //     float currentGravity = jumpSettings.GetCurrentGravity(rising, Input.GetKey(KeyCode.Space));
        //
        //     customGravity.gravityMagnitude = currentGravity;
        // }
        //
        // private void HandleEnterCollision(Collider arg0, Vector3 arg1)
        // {
        //     print("enter col");
        //     customGravity.gravityMagnitude = 0;
        // }
        //
        // private void OnDisable()
        // {
        //     collisionData.onEnterCollision.RemoveListener(HandleEnterCollision);
        //     collisionData.onExitCollision.RemoveListener(HandleExitCollision);
        // }

        [PublicAPI] 
        public void RefreshJumps()
        {
            if (showDebug) 
                print("Jumps were refreshed!");
        
            _remainingAirJumps = jumpSettings.AirJumps;
            _coyoteAvailable = true;
            _groundJumpAvailable = true;
        }

        public void TryToJump()
        {
            if (ShouldJump())
                ApplyJump();
        }

        private bool ShouldJump()
        {
            if ((collisionData.IsGrounded || CoyoteAvailable) && _groundJumpAvailable)
            {
                if (showDebug) 
                    print(collisionData.IsGrounded ? "Jumped: Normal" : "Jumped: Coyote");

                _groundJumpAvailable = false;
                return true;
            }

            if (!collisionData.IsGrounded && _remainingAirJumps > 0)
            {
                if (showDebug) 
                    print("Jumped: Air");
            
                _remainingAirJumps--;
                return true;
            }

            return false;
        }

        private void ApplyJump()
        {
            Vector3 currentVelocity = targetRigidbody.velocity;
            currentVelocity.y = jumpSettings.JumpSpeed;
            targetRigidbody.velocity = currentVelocity;
        
            _coyoteAvailable = false;
            onJump.Invoke();
        }

        public void UpdateGravity(bool holdingJump)
        {
            bool rising = targetRigidbody.velocity.y > 0;
            float currentGravity = jumpSettings.GetCurrentGravity(rising, holdingJump);
            
            customGravity.gravityMagnitude = collisionData.IsGrounded ? 0 : currentGravity;
        }
    }
}