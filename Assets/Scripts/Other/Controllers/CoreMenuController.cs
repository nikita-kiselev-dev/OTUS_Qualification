using Other;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace.Other
{
    public class CoreMenuController : MonoBehaviour
    {
        [SerializeField] private CoreMenuView m_CoreMenuView;

        public void SetScoreText(int score)
        {
            m_CoreMenuView.SetScoreText(score.ToString());
        }

        public void SetSliderValue(int sliderValue)
        {
            m_CoreMenuView.SetSliderValue(sliderValue);
        }

        public void SetSliderMaxValue(int sliderMaxValue)
        {
            m_CoreMenuView.SetSliderMaxValue(sliderMaxValue);
        }
        
        private void Awake()
        {
            m_CoreMenuView.Init(ExitCoreGame);
        }

        private void ExitCoreGame()
        {
            SceneManager.LoadScene(SceneList.StartScene);
        }
    }
}