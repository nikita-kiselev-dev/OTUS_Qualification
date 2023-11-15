using DefaultNamespace.Other;
using UnityEngine;
using UnityEngine.Events;

namespace Other.Views
{
    public class WinPopupView : MonoBehaviour
    {
        [SerializeField] private ButtonNew m_ApplyButton;
        [SerializeField] private ButtonNew m_DeclineButton;

        public void Init(UnityAction applyAction, UnityAction declineAction)
        {
            m_ApplyButton.onClick.AddListener(applyAction);
            m_DeclineButton.onClick.AddListener(declineAction);
        }

        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}