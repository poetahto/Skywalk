using JetBrains.Annotations;
using UnityEngine;

namespace poetools.Runtime.Systems
{
    [CreateAssetMenu]
    public class RotationSettings : ScriptableObject
    {
        [SerializeField] 
        [Tooltip("The max and min value for pitch (X).")]
        private Optional<float> pitchConstraint;
    
        [SerializeField]
        [Tooltip("The max and min value for yaw (Y).")]
        private Optional<float> yawConstraint;
    
        [SerializeField]
        [Tooltip("The max and min value for roll (Z).")]
        private Optional<float> rollConstraint;

        // Public API
        
        [PublicAPI] 
        public Optional<float> PitchConstraint => pitchConstraint;
        
        [PublicAPI] 
        public Optional<float> YawConstraint => yawConstraint;
        
        [PublicAPI] 
        public Optional<float> RollConstraint => rollConstraint;
    }
}