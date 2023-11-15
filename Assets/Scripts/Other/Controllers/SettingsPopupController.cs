using Audio;
using Other.Views;
using UnityEngine;

namespace Other.Controllers
{
    public class SettingsPopupController : MonoBehaviour
    {
        [SerializeField] private SettingsPopupView m_SettingsPopupView;
        
        private bool _isOpened;
        
        public void Open()
        {
            if (!_isOpened)
            {
                m_SettingsPopupView.Open();
                _isOpened = true;
            }
        }

        public void Close()
        {
            m_SettingsPopupView.Close();
            _isOpened = false;
        }
        
        private void Awake()
        {
            m_SettingsPopupView.Init(ChangeMusicVolume, ChangeSoundVolume, Close);
        }

        private void Start()
        {
            m_SettingsPopupView.SetMusicSliderValue(MusicController.Instance.Volume);
            m_SettingsPopupView.SetSoundSliderValue(SoundController.Instance.Volume);
        }

        private void ChangeMusicVolume(float volumeValue)
        {
            MusicController.Instance.SetVolumeValue(volumeValue);
        }

        private void ChangeSoundVolume(float volumeValue)
        {
            SoundController.Instance.SetVolumeValue(volumeValue);
        }
    }
}