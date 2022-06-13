using System;
using poetools.Runtime.Systems;
using UnityEngine;
using UnityEngine.Rendering;

namespace Game.Scripts
{
    public class RopeEffectPostProcessing : MonoBehaviour
    {
        [SerializeField] private Volume postProcessingVolume;
        [SerializeField] private float volumeCutoff = 0.1f;
        [SerializeField] private float fadeInSpeed = 1f;
        [SerializeField] private float fadeOutSpeed = 1f;

        private bool _onRope;

        private void Update()
        {
            float weightTarget = 1;
            float maxDelta = (_onRope ? fadeInSpeed : fadeOutSpeed) * Time.deltaTime;
            postProcessingVolume.weight = Mathf.Lerp(postProcessingVolume.weight, _onRope ? weightTarget : 0, maxDelta);
                       
            if (!_onRope && postProcessingVolume.weight < volumeCutoff)
                postProcessingVolume.weight = 0;
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