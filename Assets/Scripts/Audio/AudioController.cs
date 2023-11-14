using System;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private protected AudioSource m_AudioSource;
        [SerializeField] private protected List<AudioData> m_AudioData = new List<AudioData>();
        
        public void PlaySound(string clipName)
        {
            var audioClip = GetAudioClip(clipName);
            m_AudioSource.clip = audioClip;
            m_AudioSource.PlayOneShot(m_AudioSource.clip);
        }
    
        public void PlaySoundLoop(string clipName)
        {
            m_AudioSource.loop = true;
            var audioClip = GetAudioClip(clipName);
            m_AudioSource.clip = audioClip;
            m_AudioSource.Play();
        }
        
        private AudioClip GetAudioClip(string clipName)
        {
            AudioClip clip = null;

            foreach (var sound in m_AudioData)
            {
                if (sound.Name == clipName)
                {
                    clip = sound.AudioClip;
                }
            }
        
            return clip;
        }
    }
}