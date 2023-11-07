using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class MatchController
    {
        private CellController[,] _cells;
        private CellData _cellEmptyData;
        
        private List<CellData> _cellMatchList;
        private List<CellController> _horizontalCellMatchList;
        private List<CellController> _verticalCellMatchList;
        
        private const int MinCellsToMatch = 3;

        public MatchController(CellController[,] cells, CellData cellEmptyData)
        {
            _cells = cells;
            _cellEmptyData = cellEmptyData;
        }

        public void FindMatch(int rowNumber, int cellNumber)
        {
            if (IsNextCellsMatches(rowNumber, cellNumber))
            {
                Debug.Log("Match!");
            }
        }

        public bool IsNextCellsMatches(int rowNumber, int cellNumber)
        {
            var currentCellData = _cells[rowNumber, cellNumber].GetCellData();
            var currentCellController = _cells[rowNumber, cellNumber];
            
            _cellMatchList = new List<CellData>();
            _horizontalCellMatchList = new List<CellController>();
            _verticalCellMatchList = new List<CellController>();
            
            _horizontalCellMatchList.Add(_cells[rowNumber, cellNumber]);
            _verticalCellMatchList.Add(_cells[rowNumber, cellNumber]);

            for (int i = rowNumber - 1; i > 1; i--)
            {
                var nextCellData = _cells[i, cellNumber].GetCellData();
                var nextCellController = _cells[i, cellNumber];
                if (currentCellController.CompareCellData(nextCellData))
                {
                    _horizontalCellMatchList.Add(nextCellController);
                }
                else
                {
                    break;
                }
            }
            
            for (int i = rowNumber + 1; i < _cells.GetLength(0); i++)
            {
                var nextCellData = _cells[i, cellNumber].GetCellData();
                var nextCellController = _cells[i, cellNumber];
                if (currentCellController.CompareCellData(nextCellData))
                {
                    _horizontalCellMatchList.Add(nextCellController);
                }
                else
                {
                    break;
                }
            }

            if (_horizontalCellMatchList.Count >= MinCellsToMatch)
            {
                for (int i = 0; i < _horizontalCellMatchList.Count; i++)
                {
                    _horizontalCellMatchList[i].SetCellData(_cellEmptyData);
                }
            }
            
            for (int i = cellNumber - 1; i > 1; i--)
            {
                var nextCellData = _cells[rowNumber, i].GetCellData();
                var nextCellController = _cells[rowNumber, i];
                if (currentCellController.CompareCellData(nextCellData))
                {
                    _horizontalCellMatchList.Add(nextCellController);
                }
                else
                {
                    break;
                }
            }
            
            for (int i = cellNumber + 1; i < _cells.GetLength(1); i++)
            {
                var nextCellData = _cells[rowNumber, i].GetCellData();
                var nextCellController = _cells[rowNumber, i];
                if (currentCellController.CompareCellData(nextCellData))
                {
                    _horizontalCellMatchList.Add(nextCellController);
                }
                else
                {
                    break;
                }
            }
            
            if (_verticalCellMatchList.Count >= MinCellsToMatch)
            {
                for (int i = 0; i < _verticalCellMatchList.Count; i++)
                {
                    _verticalCellMatchList[i].SetCellData(_cellEmptyData);
                }
            }
            
            return false;
        }
        
    }
}