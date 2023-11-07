using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private int m_RowsNumber = 8;
        [SerializeField] private int m_CellsNumber = 11;
        [Space]
        [SerializeField] private GameObject m_RowPrefab;
        [SerializeField] private GameObject m_CellPrefab;
        [Space]
        [SerializeField] private RectTransform m_RowsContainer;
        [Space]
        [SerializeField] private List<CellData> m_CellDatas;
        [SerializeField] private CellData m_CellEmptyData;
        [SerializeField] private CellViewData m_CellViewData;

        private List<RectTransform> _rows;
        private BoardModel _boardModel;

        private void Start()
        {
            BuildBoard();
            
            _boardModel = new BoardModel(
                m_CellPrefab,
                m_CellEmptyData,
                m_CellViewData,
                _rows);
            
            _boardModel.CreateCellMatrix(
                m_RowsNumber,
                m_CellsNumber,
                m_CellDatas);
        }
        
        public void BuildBoard()
        {
            _rows = new List<RectTransform>();

            for (int i = 0; i < m_RowsNumber; i++)
            {
                var row = Instantiate(m_RowPrefab, m_RowsContainer);
                row.name = $"Row [{i}]";
                _rows.Add(row.GetComponent<RectTransform>());
            }
        }
    }
}