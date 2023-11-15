using Audio;
using UnityEngine.UI;

namespace DefaultNamespace.Other
{
    public class ButtonNew : Button
    {
        private void Awake()
        {
            this.onClick.AddListener(PlayButtonSound);
        }
        
        private void PlayButtonSound()
        {
            SoundController.Instance.PlaySound(SoundList.ButtonClick);
        }
    }
}