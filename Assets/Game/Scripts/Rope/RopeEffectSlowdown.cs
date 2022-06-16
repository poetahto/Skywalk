using poetools.Runtime.Systems;
using UnityEngine;

namespace Game.Scripts
{
    public class RopeEffectSlowdown : RopeEffect
    {
        [SerializeField] private float speedMultiplier = 0.5f;
        
        private MovementSystem _movementSystem;
        
        private void Start()
        {
            _movementSystem = FindObjectOfType<MovementSystem>();
        }

        protected override void OnRopeEnter()
        {
            base.OnRopeEnter();
            _movementSystem.SpeedMultiplier = speedMultiplier;
        }

        protected override void OnRopeExit()
        {
            base.OnRopeExit();
            _movementSystem.SpeedMultiplier = 1;
        }
    }
}