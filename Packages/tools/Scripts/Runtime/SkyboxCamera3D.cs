using UnityEngine;

namespace TrenchBroom
{
    /// <summary>
    /// Allows a camera to function as an origin for a 3D skybox
    /// <remarks>Based on the concept shown here: https://developer.valvesoftware.com/wiki/3D_Skybox</remarks>
    /// </summary>
    public class SkyboxCamera3D : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The base camera, onto which this 3D skybox is projected.")]
        private Camera playerCamera;
        
        [SerializeField] 
        [Tooltip("The skybox camera, which is used to capture the 3D skybox.")]
        private Camera skyboxCamera;
        
        [SerializeField]
        [Range(0, 1)]
        [Tooltip("The scale of the skybox, smaller numbers make the environment feel larger.")]
        private float scale = 0.1f;

        private Vector3 _skyboxStart;
        private Vector3 _playerStart;

        private void Start()
        {
            _skyboxStart = skyboxCamera.transform.position;
            _playerStart = playerCamera.transform.position;

            skyboxCamera.fieldOfView = playerCamera.fieldOfView;
        }

        private void LateUpdate()
        {
            Transform cameraTransform = playerCamera.transform;
            Quaternion rotation = cameraTransform.rotation;
            Vector3 movement = (_playerStart + cameraTransform.position) * scale;
            
            skyboxCamera.transform.SetPositionAndRotation(_skyboxStart + movement, rotation);
        }
    }
}