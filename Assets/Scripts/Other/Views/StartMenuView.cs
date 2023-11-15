using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DefaultNamespace.Other
{
    public class StartMenuView : MonoBehaviour
    {
        [SerializeField] private Button m_StartButton;
        [SerializeField] private Button m_SettingsButton;
        [SerializeField] private Button m_ExitGameButton;

        public void Init(
            UnityAction startCoreGame,
            UnityAction openSettings,
            UnityAction exitGame)
        {
            m_StartButton.onClick.AddListener(startCoreGame);
            m_SettingsButton.onClick.AddListener(openSettings);
            m_ExitGameButton.onClick.AddListener(exitGame);
        }
    }
}