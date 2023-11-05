using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class CellView : MonoBehaviour, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] private Image m_BackGroundImage;
        [SerializeField] private Image m_IconImage;

        private CellController _cellController;
        private Vector2Int _cellIndex;
        
        public void Init(CellController cellController, Vector2Int cellIndex)
        {
            _cellController = cellController;
            _cellIndex = cellIndex;
        }
        
        public void SetIcon(Sprite iconSprite)
        {
            m_IconImage.sprite = iconSprite;
        }

        public void SetBackground(Sprite backgroundSprite)
        {
            m_BackGroundImage.sprite = backgroundSprite;
        }

        public Vector2Int GetCellIndex()
        {
            return _cellIndex;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _cellController.OnBeginDrag(eventData);
        }
        
        public void OnEndDrag(PointerEventData eventData)
        {
            _cellController.OnEndDrag(eventData);
        }
    }
}