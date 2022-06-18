using System;
using FMODUnity;
using UnityEngine;

namespace Game.Scripts
{
    public class AudioFader : MonoBehaviour
    {
        [SerializeField] private StudioEventEmitter emitter;
        [SerializeField] private float fadeInSpeed = 1f;
        [SerializeField] private float fadeOutSpeed = 1f;
        [SerializeField] private float initialVolume;

        public float TargetVolume { get; set; }

        private void Start()
        {
            TargetVolume = initialVolume;
            emitter.EventInstance.setVolume(initialVolume);
        }

        private void Update()
        {
            emitter.EventInstance.getVolume(out float volume);

            if (Math.Abs(volume - TargetVolume) > 0)
            {
                float maxDelta = (volume > TargetVolume ? fadeInSpeed : fadeOutSpeed) * Time.deltaTime;
                volume = Mathf.MoveTowards(volume, TargetVolume, maxDelta);
                emitter.EventInstance.setVolume(volume);
            }
        }
    }
}