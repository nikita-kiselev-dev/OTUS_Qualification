using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "CellData", menuName = "ScriptableObjects/CellData", order = 1)]
    public class CellData : ScriptableObject
    {
        public enum CellType
        {
            NoType,
            General,
            Weapon,
            Booster
        }

        public enum CellColorType
        {
            NoType,
            Blue,
            Green,
            Orange,
            Pink,
            Red,
            Yellow
        }
        
        public Sprite Icon;
        public CellType CurrentCellType;
        public CellColorType CurrentCellColorType;
    }
}