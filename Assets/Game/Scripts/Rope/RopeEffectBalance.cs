using UnityEngine;

namespace Game.Scripts
{
    public class RopeEffectBalance : RopeEffect
    {
        [SerializeField] private float instabilityAmplitude = 0.05f;
        [SerializeField] private float instabilityFrequency = 0.25f;
        [SerializeField] private float correctionSpeed = 0.25f;
        
        private BalanceSystem _balanceSystem;
        private float _perlinOffset;
        
        private float _velocity;

        private void Start()
        {
            _balanceSystem = FindObjectOfType<BalanceSystem>();
        }

        private void Update()
        {
            if (OnRope)
            {
                
                _velocity += (Mathf.PerlinNoise(_perlinOffset * instabilityFrequency, 0) - 0.5f) * 2 *
                             instabilityAmplitude * Time.deltaTime;

                _perlinOffset += Time.deltaTime;

                if (Input.GetKey(KeyCode.Q))
                    _velocity += correctionSpeed * Time.deltaTime;

                if (Input.GetKey(KeyCode.E))
                    _velocity -= correctionSpeed * Time.deltaTime;
                
                _balanceSystem.Destabilize(_velocity);
            }

            else
            {
                _balanceSystem.Stabilize();
                _velocity = 0;
            }
        }
    }
}