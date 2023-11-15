using Audio;
using DefaultNamespace.Other;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Other.Controllers
{
    public class StartMenuController : MonoBehaviour
    {
        [Space, Header("Controllers"), Space]
        [SerializeField] private SettingsPopupController m_SettingsPopupController;
        
        [Space, Header("Views"), Space]
        [SerializeField] private StartMenuView m_StartMenuView;
        
        private void Awake()
        {
            m_StartMenuView.Init(StartCoreGame, OpenSettings, ExitGame);
        }

        private void Start()
        {
            if (!MusicController.Instance.IsPlaying())
            {
                MusicController.Instance.PlaySoundLoop("Background");
            }
        }

        private void StartCoreGame()
        {
            SceneManager.LoadScene(SceneList.CoreGame);
        }

        private void OpenSettings()
        {
            m_SettingsPopupController.Open();
        }

        private void ExitGame()
        {
            Application.Quit();
        }
    }
}