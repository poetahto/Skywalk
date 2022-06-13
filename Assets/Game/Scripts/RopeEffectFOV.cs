using UnityEngine;

namespace Game.Scripts
{
    public class RopeEffectFOV : MonoBehaviour
    {
        [SerializeField] private float fovMultiplier = 0.9f;
        [SerializeField] private float fadeInSpeed = 1f;
        [SerializeField] private float fadeOutSpeed = 1f;
        
        private float _originalFOV;
        private Camera _camera;
        private bool _onRope;

        private void Start()
        {
            _camera = Camera.main;
            _originalFOV = _camera == null ? 70 :  _camera.fieldOfView;
        }

        private void Update()
        {
            float maxDelta = (_onRope ? fadeInSpeed : fadeOutSpeed) * Time.deltaTime;
            
            _camera.fieldOfView =
                Mathf.Lerp(_camera.fieldOfView, _originalFOV * (_onRope ? fovMultiplier : 1), maxDelta);
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