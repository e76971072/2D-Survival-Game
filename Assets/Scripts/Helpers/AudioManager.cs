using System;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

namespace Helpers
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        [SerializeField] private AudioSource sfxAudioSource;
        [Range(0.75f, 3f)] [SerializeField] private float minPitchRange;
        [Range(1f, 3f)] [SerializeField] private float maxPitchRange;

        private void OnValidate()
        {
            minPitchRange = Mathf.Clamp(minPitchRange, 0.75f, maxPitchRange);
            maxPitchRange = Mathf.Clamp(maxPitchRange, minPitchRange, 3f);
        }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }

            DontDestroyOnLoad(this);
        }

        public void PlayAdditionalClip(AudioClip clip, AudioMixerGroup targetGroup)
        {
            sfxAudioSource.outputAudioMixerGroup = targetGroup;
            sfxAudioSource.pitch = GetRandomPitch();
            sfxAudioSource.PlayOneShot(clip);
        }

        private float GetRandomPitch()
        {
            return Random.Range(minPitchRange, maxPitchRange);
        }
    }
}