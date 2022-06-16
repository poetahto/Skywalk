using UnityEngine;

namespace Game.Scripts
{
    [RequireComponent(typeof(Camera))]
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private float fovIncreaseSpeed = 5f;
        [SerializeField] private float fovDecreaseSpeed = 5f;
        
        public float FieldOfView { get; set; }

        private Camera _camera;

        private void Awake()
        {
            _camera = GetComponent<Camera>();

            FieldOfView = _camera.fieldOfView;
        }

        private void Update()
        {
            float maxDelta = _camera.fieldOfView < FieldOfView 
                ? fovIncreaseSpeed 
                : fovDecreaseSpeed;
            
            maxDelta *= Time.deltaTime;
            
            _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, FieldOfView, maxDelta);
        }
    }
}