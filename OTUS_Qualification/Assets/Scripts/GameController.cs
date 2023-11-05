using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameObject m_RowPrefab;
        [SerializeField] private GameObject m_CellPrefab;
        
        [SerializeField] private RectTransform m_RowsContainer;
        [SerializeField] private Canvas m_MainCanvas;
        
        [SerializeField] private CellData m_CommonCellData;
        [SerializeField] private CellViewData m_CellViewData;

        private List<RectTransform> _rows;
        private BoardModel _boardModel;
        private CellController[,] m_Cells;

        private void Start()
        {
            BuildBoard();
            
            _boardModel = new BoardModel(
                m_CellPrefab,
                m_CellViewData,
                m_MainCanvas,
                _rows);
            
            _boardModel.CreateCellMatrix(5,5,m_CommonCellData);
        }
        
        public void BuildBoard(int rowsCounter = 5)
        {
            _rows = new List<RectTransform>();

            for (int i = 0; i < rowsCounter; i++)
            {
                var row = Instantiate(m_RowPrefab, m_RowsContainer);
                row.name = $"Row [{i}]";
                _rows.Add(row.GetComponent<RectTransform>());
            }
        }
    }
}