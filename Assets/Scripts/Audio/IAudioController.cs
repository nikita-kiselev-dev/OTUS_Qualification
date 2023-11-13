using UnityEngine;

namespace Audio
{
    public interface IAudioController<T>
    {
        public static T Instance;

        public void Init();
        public void PlaySound(string clipName);
        public void PlaySoundLoop(string clipName);
        public AudioClip GetAudioClip(string clipName);
    }
}