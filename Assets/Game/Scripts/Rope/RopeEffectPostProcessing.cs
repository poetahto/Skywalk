using UnityEngine;
using UnityEngine.Rendering;

namespace Game.Scripts
{
    public class RopeEffectPostProcessing : RopeEffect
    {
        [SerializeField] private Volume postProcessingVolume;
        [SerializeField] private float volumeCutoff = 0.1f;
        [SerializeField] private float fadeInSpeed = 5f;
        [SerializeField] private float fadeOutSpeed = 5f;

        private void Update()
        {
            float weightTarget = 1;
            float maxDelta = (OnRope ? fadeInSpeed : fadeOutSpeed) * Time.deltaTime;
            postProcessingVolume.weight = Mathf.Lerp(postProcessingVolume.weight, OnRope ? weightTarget : 0, maxDelta);
                       
            if (!OnRope && postProcessingVolume.weight < volumeCutoff)
                postProcessingVolume.weight = 0;
        }
    }
}