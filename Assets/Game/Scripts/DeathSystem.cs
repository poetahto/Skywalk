using System;
using System.Collections;
using FMODUnity;
using UnityEngine;

namespace Game.Scripts
{
    public class DeathSystem : MonoBehaviour
    {
        [SerializeField] private new Rigidbody rigidbody;
        [SerializeField] private float killSpeed;
        [SerializeField] private EventReference deathEventReference;
        
        public Vector3 RespawnLocation { get; set; }

        private bool _isDying;
        
        private void Update()
        {
            if (rigidbody.velocity.y > killSpeed && !_isDying)
            {
                StartCoroutine(KillPlayerCoroutine());
            }
        }

        private IEnumerator KillPlayerCoroutine()
        {
            _isDying = true;
            
            
            RuntimeManager.PlayOneShot(deathEventReference);
            yield return null;
        }
    }
}