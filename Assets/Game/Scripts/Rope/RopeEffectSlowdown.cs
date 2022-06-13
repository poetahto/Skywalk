using poetools.Runtime.Systems;
using UnityEngine;

namespace Game.Scripts
{
    public class RopeEffectSlowdown : RopeEffect
    {
        [SerializeField] private float speedMultiplier = 0.5f;
        [SerializeField] private float fadeInSpeed = 10f;
        [SerializeField] private float fadeOutSpeed = 10f;
        
        private MovementSystem _movementSystem;
        
        private void Start()
        {
            _movementSystem = FindObjectOfType<MovementSystem>();
        }

        private void Update()
        {
            float maxDelta = (OnRope ? fadeInSpeed : fadeOutSpeed) * Time.deltaTime;
            _movementSystem.SpeedMultiplier = Mathf.Lerp(_movementSystem.SpeedMultiplier, OnRope ? speedMultiplier : 1, maxDelta);
        }
    }
}