using Audio;
using DefaultNamespace.Other;
using Other.Views;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Other.Controllers
{
    public class WinPopupController : MonoBehaviour
    {
        [SerializeField] private WinPopupView m_WinPopupView;
        
        public void Open()
        {
            SoundController.Instance.PlaySound(SoundList.LevelWin);
            m_WinPopupView.Open();
        }
        
        private void Awake()
        {
            m_WinPopupView.Init(RestartLevel, ExitCoreGame);
        }

        private void RestartLevel()
        {
            SceneManager.LoadScene(SceneList.CoreGame);
        }

        private void ExitCoreGame()
        {
            SceneManager.LoadScene(SceneList.StartScene);
        }
    }
}