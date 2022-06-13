using poetools.Runtime.Systems;
using UnityEngine;

namespace Game.Scripts
{
    public class RopeEffectSlowdown : MonoBehaviour
    {
        [SerializeField] private float speedMultiplier = 0.75f;
        [SerializeField] private float fadeInSpeed = 1f;
        [SerializeField] private float fadeOutSpeed = 1f;
        
        private MovementSystem _movementSystem;
        private bool _onRope;

        private void Start()
        {
            _movementSystem = FindObjectOfType<MovementSystem>();
        }

        private void Update()
        {
            float maxDelta = (_onRope ? fadeInSpeed : fadeOutSpeed) * Time.deltaTime;
            _movementSystem.SpeedMultiplier = Mathf.Lerp(_movementSystem.SpeedMultiplier, _onRope ? speedMultiplier : 1, maxDelta);
        }

        #region Rope Collision

            private void FixedUpdate()
            {
                _onRope = false;
            }

            private void OnCollisionStay()
            {
                _onRope = true;
            }

        #endregion
    }
}