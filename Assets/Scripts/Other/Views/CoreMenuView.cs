using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Other
{
    public class CoreMenuView : MonoBehaviour
    {
        [Space, Header("Main UI"), Space]
        [SerializeField] private TextMeshProUGUI m_ScoreText;
        [SerializeField] private Slider m_ScoreSlider;
        
        [SerializeField] private Button m_ExitButton;

        public void Init(UnityAction exitCoreGame)
        {
            m_ExitButton.onClick.AddListener(exitCoreGame);
        }

        public void SetScoreText(string scoreText)
        {
            m_ScoreText.text = scoreText;
        }

        public void SetSliderValue(float sliderValue)
        {
            m_ScoreSlider.value = sliderValue;
        }

        public void SetSliderMaxValue(float sliderMaxValue)
        {
            m_ScoreSlider.maxValue = sliderMaxValue;
        }
    }
}