using Audio;
using DefaultNamespace.Other;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Other.Controllers
{
    public class StartMenuController : MonoBehaviour
    {
        [SerializeField] private StartMenuView m_StartMenuView;
        
        private void Awake()
        {
            m_StartMenuView.Init(StartCoreGame, OpenSettings);
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
        }
    }
}