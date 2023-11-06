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
        }

        public void SwapCells(Vector2Int firstCellIndex, Vector2Int secondCellIndex)
        {
            if (IsSwappable(firstCellIndex, secondCellIndex))
            {
                var firstCell = m_Cells[firstCellIndex.x, firstCellIndex.y];
                var firstCellData = firstCell.GetCellData();
            
                var secondCell = m_Cells[secondCellIndex.x, secondCellIndex.y];
                var secondCellData = secondCell.GetCellData();

                if (firstCellData == secondCellData)
                {
                    Debug.Log("BoardModel: Can't swap! Reason: Same cell type");
                    Debug.Log($"BoardModel: First Cell {firstCellIndex} : Second Cell {secondCellIndex}. Swap result: True");
                    return;
                }
            
                var firstCellTempData = firstCell.GetCellData();
                firstCell.SetCellData(secondCell.GetCellData());
                secondCell.SetCellData(firstCellTempData);
            
                UpdateBoardView();
                
                Debug.Log($"BoardModel: First Cell {firstCellIndex} : Second Cell {secondCellIndex}. Swap result: True");
            }
            else
            {
                Debug.Log($"BoardModel: First Cell {firstCellIndex} : Second Cell {secondCellIndex}. Swap result: False");
            }
        }

        private bool IsSwappable(Vector2Int firstCellIndex, Vector2Int secondCellIndex)
        {
            if (firstCellIndex == secondCellIndex)
            {
                Debug.Log("BoardModel: Can't swap! Reason: Same cell index");
                return false;
            }

            var horizontallySwappable = firstCellIndex.y != secondCellIndex.y;

            var verticallySwappable = firstCellIndex.x != secondCellIndex.x;

            if (horizontallySwappable && verticallySwappable)
            {
                Debug.Log($"BoardModel: Can't swap! Reason: Horizontally Swappable: {horizontallySwappable} + Vertically Swappable: {verticallySwappable} = false");
                return false;
            }

            if (horizontallySwappable || verticallySwappable)
            {
                return true;
            }

            Debug.Log($"BoardModel: Can't swap! Reason: Horizontally Swappable: {horizontallySwappable} : Vertically Swappable: {verticallySwappable}");
            return false;
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