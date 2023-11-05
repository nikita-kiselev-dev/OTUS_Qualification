using UnityEngine;
using UnityEngine.EventSystems;
using Object = UnityEngine.Object;

namespace DefaultNamespace
{
    public class CellController
    {
        private readonly BoardModel _boardModel;
        private Vector2Int _cellIndex;
        private GameObject _cellPrefab;
        private CellData _cellData;
        private CellViewData _cellViewData;
        private Canvas _rootCanvas;
        private RectTransform _parent;

        private CellConfig _cellConfig;
        
        private CellView _cellView;

        private bool _isSelected;

        public CellController(
            BoardModel boardModel,
            Vector2Int cellIndex,
            GameObject cellPrefab,
            CellData cellData,
            CellViewData cellViewData,
            Canvas rootCanvas,
            RectTransform parent)
        {
            _boardModel = boardModel;
            _cellIndex = cellIndex;
            _cellPrefab = cellPrefab;
            _cellData = cellData;
            _cellViewData = cellViewData;
            _rootCanvas = rootCanvas;
            _parent = parent;
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
            _cellView.SetBackground(_cellViewData.SelectedBackgroundSprite);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _cellView.SetBackground(_cellViewData.DelesectedBackGroundSprite);

            var eventDataGameObject = eventData.pointerCurrentRaycast.gameObject;
            if (eventDataGameObject.TryGetComponent(out CellView cellToSwap))
            {
                _boardModel.SwapCells(_cellView.CellIndex, cellToSwap.CellIndex);
            }
        }

        public void SetCellData(CellData cellData)
        {
            _cellData = cellData;
        }

        public CellData GetCellData()
        {
            return _cellData;
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