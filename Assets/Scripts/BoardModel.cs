using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class BoardModel
    {
        private CellController[,] m_Cells;

        private GameObject _cellPrefab;
        private CellData _cellEmptyData;
        private CellViewData _cellViewData;
        private List<RectTransform> _rows;

        private SwapController _swapController;
        private MatchController _matchController;

        public BoardModel(
            GameObject cellPrefab,
            CellData cellEmptyData,
            CellViewData cellViewData,
            List<RectTransform> rows)
        {
            _cellPrefab = cellPrefab;
            _cellEmptyData = cellEmptyData;
            _cellViewData = cellViewData;
            _rows = rows;
        }
        
        public void CreateCellMatrix(
            int rowsNumber,
            int cellsNumber,
            List<CellData> cellData)
        {
            m_Cells = new CellController[rowsNumber, cellsNumber];
            
            for (int rowNumber = 0; rowNumber < rowsNumber; rowNumber++)
            {
                for (int cellNumber = 0; cellNumber < cellsNumber; cellNumber++)
                {
                    var randomCellData = cellData[Random.Range(0, cellData.Count)];
                    
                    m_Cells[rowNumber, cellNumber] = new CellController(
                        this,
                        new Vector2Int(rowNumber, cellNumber),
                        _cellPrefab,
                        randomCellData,
                        _cellViewData,
                        _rows[rowNumber]);
                    
                    CellConfig cellConfig = new CellConfig($"Cell [{rowNumber}, {cellNumber}]");
                    m_Cells[rowNumber,cellNumber].UpdateCell(cellConfig);
                    m_Cells[rowNumber,cellNumber].Init();
                }
            }

            CreateBoardControllers();
        }

        private void CreateBoardControllers()
        {
            _swapController = new SwapController(m_Cells);
            _matchController = new MatchController(m_Cells, _cellEmptyData);
        }

        public void SwapCells(Vector2Int firstCellIndex, Vector2Int secondCellIndex)
        {
            bool isSwapped = _swapController.SwapCells(firstCellIndex, secondCellIndex);
            if (isSwapped)
            {
                UpdateBoardView();
                _matchController.FindMatch(firstCellIndex.x, firstCellIndex.y);
                _matchController.FindMatch(secondCellIndex.x, secondCellIndex.y);
                UpdateBoardView();
            }
        }

        private void UpdateBoardView()
        {
            for (int rowNumber = 0; rowNumber < m_Cells.GetLength(0); rowNumber++)
            {
                for (int cellNumber = 0; cellNumber < m_Cells.GetLength(1); cellNumber++)
                {
                    m_Cells[rowNumber, cellNumber].UpdateView();
                }
            }
        }
    }
}