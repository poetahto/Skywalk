using JetBrains.Annotations;
using UnityEngine;

// todo v2: turn settings into ScriptableObject and support blending between them?

namespace poetools.Runtime.Systems
{
    /// <summary>
    /// Allows for constrained updating of the pitch, yaw, and roll of an object.
    /// </summary>

    public class RotationSystem : MonoBehaviour
    {
        [SerializeField]
        private RotationSettings settings;
    
        [Header("References")]
    
        [SerializeField] 
        [Tooltip("The transform to control X rotation.")]
        private Transform pitchTransform;
    
        [SerializeField]
        [Tooltip("The transform to control Y rotation.")]
        private Transform yawTransform;
    
        [SerializeField] 
        [Tooltip("The transform to control Z rotation.")]
        private Transform rollTransform;

        private Vector3 _initialRotation;
        private Vector3 _currentRotation;

        private void OnEnable()
        {
            // Find and save our current euler rotation as a starting point.

            Transform targetTransform = transform;

            if (pitchTransform != null) _currentRotation.x = pitchTransform.localRotation.eulerAngles.x;
            if (yawTransform != null) _currentRotation.y = yawTransform.localRotation.eulerAngles.y;
            if (rollTransform != null) _currentRotation.z = rollTransform.localRotation.eulerAngles.z;

            CurrentRotation = targetTransform.rotation.eulerAngles;
            targetTransform.rotation = Quaternion.identity;

            _initialRotation = _currentRotation;
        }

        private void OnDisable()
        {
            transform.rotation = Quaternion.Euler(CurrentRotation);
            CurrentRotation = Vector3.zero;
        }

        #region Helper Methods

        private void SetPitch(float value)
        {
            _currentRotation.x = value % 360;
            pitchTransform.localRotation = Quaternion.Euler(_currentRotation.x, 0, 0);    
        }

        private void SetYaw(float value)
        {
            _currentRotation.y = value % 360;
            yawTransform.localRotation = Quaternion.Euler(0, _currentRotation.y, 0);
        }

        private void SetRoll(float value)
        {
            _currentRotation.z = value % 360;
            rollTransform.localRotation = Quaternion.Euler(0, 0, _currentRotation.z);
        }
        
        private static float Constrain(float value, float initialValue, Optional<float> constraint)
        {
            if (!constraint.shouldBeUsed)
                return value;
        
            // Ensure that the provided value cannot deviate too far from the initial value, specified by the constraint. 
        
            return Mathf.Clamp(value, initialValue - constraint.value, initialValue + constraint.value);
        }

        #endregion
        
        #region Public API

        [PublicAPI] 
        public Vector3 CurrentRotation
        {
            get => _currentRotation;
            set
            {
                Pitch = value.x;
                Yaw = value.y;
                Roll = value.z;
            }
        }

        [PublicAPI] 
        public float Pitch
        {
            get => _currentRotation.x;
            set => SetPitch(Constrain(value, _initialRotation.x, settings.PitchConstraint));
        }

        [PublicAPI] 
        public float Yaw
        {
            get => _currentRotation.y;
            set => SetYaw(Constrain(value, _initialRotation.y, settings.YawConstraint));
        }

        [PublicAPI] 
        public float Roll
        {
            get => _currentRotation.z;
            set => SetRoll(Constrain(value, _initialRotation.z, settings.RollConstraint));
        }

        [PublicAPI] 
        public Vector3 Forward
        {
            get => Quaternion.Euler(CurrentRotation) * Vector3.forward;
            set => CurrentRotation = Quaternion.LookRotation(value).eulerAngles;
        }
        
        #endregion
    }
}