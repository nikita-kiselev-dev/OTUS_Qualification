using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "CellViewData", menuName = "ScriptableObjects/CellViewData", order = 1)]
    public class CellViewData : ScriptableObject
    {
        public Sprite SelectedBackgroundSprite;
        public Sprite DelesectedBackGroundSprite;
    }
}