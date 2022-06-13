using UnityEngine;

namespace Game.Scripts
{
    public class RopeEffectFOV : RopeEffect
    {
        [SerializeField] private float fovMultiplier = 0.9f;
        [SerializeField] private float fadeInSpeed = 1f;
        [SerializeField] private float fadeOutSpeed = 1f;
        
        private float _originalFOV;
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
            _originalFOV = _camera == null ? 70 :  _camera.fieldOfView;
        }

        private void Update()
        {
            float maxDelta = (OnRope ? fadeInSpeed : fadeOutSpeed) * Time.deltaTime;
            
            _camera.fieldOfView =
                Mathf.Lerp(_camera.fieldOfView, _originalFOV * (OnRope ? fovMultiplier : 1), maxDelta);
        }
    }
}