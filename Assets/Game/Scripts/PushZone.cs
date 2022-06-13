using UnityEngine;

namespace Game.Scripts
{
    public class PushZone : MonoBehaviour
    {
        [SerializeField] private new ParticleSystem particleSystem;
        [SerializeField] private Vector3 pushDirection = Vector3.left;
        [SerializeField] private float pushStrength = 1f;
        [SerializeField] private bool randomizeMagnitude = true;
        [SerializeField] private float randomSpeed = 0.5f;

        private Vector3 _pushVector;
        private float _perlinOffset;

        private void Update()
        {
            var velocityModule = particleSystem.velocityOverLifetime;
            _perlinOffset += Time.deltaTime * randomSpeed;
            float multiplier = (Mathf.PerlinNoise(_perlinOffset, 0) - 0.5f) * 2;
            _pushVector = pushDirection * (pushStrength * (randomizeMagnitude ? multiplier : 1));

            velocityModule.x = _pushVector.x;
            velocityModule.y = _pushVector.y;
            velocityModule.z = _pushVector.z;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.attachedRigidbody != null)
                other.attachedRigidbody.AddForce(_pushVector, ForceMode.Acceleration);
        }
    }
}