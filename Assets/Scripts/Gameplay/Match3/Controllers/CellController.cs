using DefaultNamespace;
using UnityEngine;
using UnityEngine.EventSystems;
using Object = UnityEngine.Object;

namespace Gameplay.Match3.Controllers
{
    public class CellController
    {
        private readonly BoardController _boardController;
        private Vector2Int _cellIndex;
        private GameObject _cellPrefab;
        private CellData _cellData;
        private CellViewData _cellViewData;
        private RectTransform _parent;

        private CellConfig _cellConfig;
        
        private CellView _cellView;

        private bool _isSelected;

        private CellData _cellEmptyData;

        public CellData CellData => _cellData;

        public CellController(
            BoardController boardController,
            Vector2Int cellIndex,
            GameObject cellPrefab,
            CellData cellData,
            CellViewData cellViewData,
            RectTransform parent,
            CellData cellEmptyData)
        {
            _boardController = boardController;
            _cellIndex = cellIndex;
            _cellPrefab = cellPrefab;
            _cellData = cellData;
            _cellViewData = cellViewData;
            _parent = parent;
            _cellEmptyData = cellEmptyData;
        }

        public void Init()
        {
            CreateView();
            ConfigureView();
        }

        public void UpdateCell(CellConfig cellConfig)
        {
            _cellConfig = cellConfig;
        }

        public void OnBeginDrag()
        {
            if (!CompareCellData(_cellEmptyData))
            {
                _cellView.SetBackground(_cellViewData.SelectedBackgroundSprite);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!CompareCellData(_cellEmptyData))
            {
                _cellView.SetBackground(_cellViewData.DelesectedBackGroundSprite);

                var eventDataGameObject = eventData.pointerCurrentRaycast.gameObject;
                if (eventDataGameObject.TryGetComponent(out CellView cellToSwap))
                {
                    _boardController.SwapCells(cellToSwap.CellIndex, _cellView.CellIndex);
                }
            }
        }

        public void SetCellData(CellData cellData)
        {
            _cellData = cellData;
        }

        public bool CompareCellData(CellData cellData)
        {
            var isSameCellData = cellData.Equals(CellData);
            return isSameCellData;
        }

        private void ConfigureView()
        {
            _cellView.Init(this, _cellIndex);
            _cellView.name = _cellConfig.CellName;
            UpdateView();
        }

        public void UpdateView()
        {
            _cellView.SetIcon(_cellData.Icon);
        }

        private void CreateView()
        {
            _cellView = Object.Instantiate(_cellPrefab, _parent).GetComponent<CellView>();
        }
    }
}