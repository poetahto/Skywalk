using UnityEngine;

namespace Game.Scripts
{
    public class RopeEffectFOV : RopeEffect
    {
        [SerializeField] private float fovMultiplier = 0.85f;
        
        private float _originalFOV;
        private PlayerCamera _camera;

        private void Start()
        {
            _camera = FindObjectOfType<PlayerCamera>();
            _originalFOV = _camera == null ? 70 :  _camera.FieldOfView;
        }

        protected override void OnRopeEnter()
        {
            base.OnRopeEnter();
            _camera.FieldOfView = _originalFOV * fovMultiplier;
        }

        protected override void OnRopeExit()
        {
            base.OnRopeExit();
            _camera.FieldOfView = _originalFOV;
        }
    }
}