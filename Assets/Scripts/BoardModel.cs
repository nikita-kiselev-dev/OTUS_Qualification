using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class BoardModel
    {
        private CellController[,] m_Cells;

        private GameObject _cellPrefab;
        private CellViewData _cellViewData;
        private Canvas _mainCanvas;
        private List<RectTransform> _rows;

        public BoardModel(
            GameObject cellPrefab,
            CellViewData cellViewData,
            Canvas mainCanvas,
            List<RectTransform> rows)
        {
            _cellPrefab = cellPrefab;
            _cellViewData = cellViewData;
            _mainCanvas = mainCanvas;
            _rows = rows;
        }
        
        public void CreateCellMatrix(
            int rowsCounter,
            int cellCounter,
            CellData cellData)
        {
            m_Cells = new CellController[rowsCounter, cellCounter];
            
            for (int rowNumber = 0; rowNumber < rowsCounter; rowNumber++)
            {
                for (int cellNumber = 0; cellNumber < cellCounter; cellNumber++)
                {
                    m_Cells[rowNumber, cellNumber] = new CellController(
                        this,
                        new Vector2Int(rowNumber, cellNumber),
                        _cellPrefab,
                        cellData,
                        _cellViewData,
                        _mainCanvas,
                        _rows[rowNumber]);
                    
                    CellConfig cellConfig = new CellConfig($"Cell [{rowNumber}, {cellNumber}]");
                    m_Cells[rowNumber,cellNumber].UpdateCell(cellConfig);
                    m_Cells[rowNumber,cellNumber].Init();
                }
            }
        }

        public void SwapCells(Vector2Int firstCellIndex, Vector2Int secondCellIndex)
        {
            var firstCell = m_Cells[firstCellIndex.x, firstCellIndex.y];
            var secondCell = m_Cells[secondCellIndex.x, secondCellIndex.y];
            
            var firstCellTempData = firstCell.GetCellData();
            firstCell.SetCellData(secondCell.GetCellData());
            secondCell.SetCellData(firstCellTempData);
        }

        private void UpdateBoardView()
        {
            
        }
    }
}