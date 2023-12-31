﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private protected AudioSource m_AudioSource;
        [SerializeField] private protected List<AudioData> m_AudioData = new List<AudioData>();

        public float Volume => m_AudioSource.volume;
        
        public void PlaySound(string clipName)
        {
            var audioClip = GetAudioClip(clipName);
            m_AudioSource.clip = audioClip;
            m_AudioSource.PlayOneShot(m_AudioSource.clip);
        }
    
        public void PlaySoundLoop(string clipName)
        {
            var audioClip = GetAudioClip(clipName);
            m_AudioSource.clip = audioClip;
            m_AudioSource.loop = true;
            m_AudioSource.Play();
        }

        public bool IsPlaying()
        {
            return m_AudioSource.isPlaying;
        }

        public void SetVolumeValue(float volumeValue)
        {
            m_AudioSource.volume = volumeValue;
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