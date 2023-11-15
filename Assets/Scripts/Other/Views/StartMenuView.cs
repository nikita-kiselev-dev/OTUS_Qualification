using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DefaultNamespace.Other
{
    public class StartMenuView : MonoBehaviour
    {
        [SerializeField] private Button m_StartButton;
        [SerializeField] private Button m_SettingsButton;

        public void Init(UnityAction startCoreGame, UnityAction openSettings)
        {
            m_StartButton.onClick.AddListener(startCoreGame);
            m_SettingsButton.onClick.AddListener(openSettings);
        }
    }
}