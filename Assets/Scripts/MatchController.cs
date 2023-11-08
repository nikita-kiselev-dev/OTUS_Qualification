using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class MatchController
    {
        private CellController[,] _cells;
        
        private List<CellController> _cellMatchList;
        private List<CellController> _horizontalCellMatchList;
        private List<CellController> _verticalCellMatchList;
        
        private const int MinCellsToMatch = 3;

        public MatchController(CellController[,] cells)
        {
            _cells = cells;
        }

        public void Init()
        {
            _cellMatchList = new List<CellController>();
            _horizontalCellMatchList = new List<CellController>();
            _verticalCellMatchList = new List<CellController>();
        }

        public bool FindMatch(int rowNumber, int cellNumber)
        {
            if (IsMatch(rowNumber, cellNumber))
            {
                Debug.Log("Match!");
                return true;
            }

            return false;
        }

        public List<CellController> GetCellMatchList()
        {
            return _cellMatchList;
        }

        public bool IsMatch(int rowNumber, int cellNumber)
        {
            var currentCellController = _cells[rowNumber, cellNumber];
            
            _cellMatchList.Add(_cells[rowNumber, cellNumber]);
            /*_horizontalCellMatchList.Add(_cells[rowNumber, cellNumber]);
            _verticalCellMatchList.Add(_cells[rowNumber, cellNumber]);*/
            bool horizontalMatch = IsHorizontalMatch(_cellMatchList, currentCellController, rowNumber, cellNumber);
            bool verticalMatch = IsVerticalMatch(_cellMatchList, currentCellController, rowNumber, cellNumber);
            
            _horizontalCellMatchList.Clear();
            _verticalCellMatchList.Clear();

            return horizontalMatch || verticalMatch;
        }

        private bool IsHorizontalMatch(
            List<CellController> axisCellMatchList,
            CellController currentCellController,
            int rowNumber,
            int cellNumber)
        {
            for (int i = cellNumber - 1; i >= 0; i--)
            {
                var nextCellData = _cells[rowNumber, i].GetCellData();
                var nextCellController = _cells[rowNumber, i];
                if (currentCellController.CompareCellData(nextCellData))
                {
                    axisCellMatchList.Add(nextCellController);
                }
                else
                {
                    break;
                }
            }
            
            for (int i = cellNumber + 1; i < _cells.GetLength(0); i++)
            {
                var nextCellData = _cells[rowNumber, i].GetCellData();
                var nextCellController = _cells[rowNumber, i];
                if (currentCellController.CompareCellData(nextCellData))
                {
                    axisCellMatchList.Add(nextCellController);
                }
                else
                {
                    break;
                }
            }

            if (axisCellMatchList.Count >= MinCellsToMatch)
            {
                return true;
            }

            return false;
        }
        
        private bool IsVerticalMatch(
            List<CellController> axisCellMatchList,
            CellController currentCellController,
            int rowNumber,
            int cellNumber)
        {
            for (int i = rowNumber - 1; i >= 0; i--)
            {
                var nextCellData = _cells[i, cellNumber].GetCellData();
                var nextCellController = _cells[i, cellNumber];
                if (currentCellController.CompareCellData(nextCellData))
                {
                    axisCellMatchList.Add(nextCellController);
                }
                else
                {
                    break;
                }
            }
            
            for (int i = rowNumber + 1; i < _cells.GetLength(1); i++)
            {
                var nextCellData = _cells[i, cellNumber].GetCellData();
                var nextCellController = _cells[i, cellNumber];
                if (currentCellController.CompareCellData(nextCellData))
                {
                    axisCellMatchList.Add(nextCellController);
                }
                else
                {
                    break;
                }
            }
            
            if (axisCellMatchList.Count >= MinCellsToMatch)
            {
                return true;
            }

            return false;
        }
        
    }
}