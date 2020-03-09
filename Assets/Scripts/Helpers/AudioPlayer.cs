using Interfaces;
using UnityEngine;

namespace Helpers
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioPlayer : MonoBehaviour, IAudioHandler
    {
        [Range(0.75f, 3f)] [SerializeField] private float minPitchRange;
        [Range(1f, 3f)] [SerializeField] private float maxPitchRange;

        private AudioSource audioSource;

        private void OnValidate()
        {
            minPitchRange = Mathf.Clamp(minPitchRange, 0.75f, maxPitchRange);
            maxPitchRange = Mathf.Clamp(maxPitchRange, minPitchRange, 3f);
        }

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void PlayAudioSource()
        {
            audioSource.Stop();
            audioSource.pitch = GetRandomPitch();
            audioSource.Play();
        }

        private float GetRandomPitch()
        {
            return Random.Range(minPitchRange, maxPitchRange);
        }
    }
}