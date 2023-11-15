using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Other.Views
{
    public class SettingsPopupView : MonoBehaviour
    {
        [Space, Header("Music"), Space]
        [SerializeField] private Slider m_MusicSlider;

        [Space, Header("Sound"), Space]
        [SerializeField] private Slider m_SoundSlider;
        [Space]
        
        [SerializeField] private Button m_CloseButton;

        public void Init(
            UnityAction<float> changeMusicVolume,
            UnityAction<float> changeSoundVolume,
            UnityAction closePopup)
        {
            m_MusicSlider.onValueChanged.AddListener(changeMusicVolume);
            m_SoundSlider.onValueChanged.AddListener(changeSoundVolume);
            m_CloseButton.onClick.AddListener(closePopup);
        }
        
        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }

        public void SetMusicSliderValue(float sliderValue)
        {
            m_MusicSlider.value = sliderValue;
        }

        public void SetSoundSliderValue(float sliderValue)
        {
            m_SoundSlider.value = sliderValue;
        }
    }
}