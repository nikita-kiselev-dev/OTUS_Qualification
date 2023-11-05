using System.Collections.Generic;
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
        
        public string Name;
        public Sprite CurrentSprite;
        public List<Sprite> IconList;
        public CellType CurrentCellType;

        public Sprite GetRandomSprite()
        {
            if (IconList != null && IconList.Count > 0)
            {
                return IconList[Random.Range(0, IconList.Count)];
            }

            return null;
        }

    }
}