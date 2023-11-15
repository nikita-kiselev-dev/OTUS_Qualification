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

        private bool _isOpened;
        
        public void Open()
        {
            if (!_isOpened)
            {
                SoundController.Instance.PlaySound(SoundList.LevelWin);
                m_WinPopupView.Open();
                _isOpened = true;
            }
        }
        
        private void Awake()
        {
            m_WinPopupView.Init(RestartLevel, ExitCoreGame);
        }

        private void RestartLevel()
        {
            _isOpened = false;
            SceneManager.LoadScene(SceneList.CoreGame);
        }

        private void ExitCoreGame()
        {
            _isOpened = false;
            SceneManager.LoadScene(SceneList.StartScene);
        }
    }
}