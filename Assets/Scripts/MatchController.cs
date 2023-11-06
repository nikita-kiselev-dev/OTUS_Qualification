using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class MatchController
    {
        //private List<CellController> _
        
        private CellController[,] _cells;

        private List<CellData> _cellMatchList;
        
        private const string HorizontalAxis = "horizontal";
        private const string VerticalAxis = "vertical";
        private const int MinCellsToMatch = 3;

        public MatchController(CellController[,] cells)
        {
            _cells = cells;
        }

        public void FindMatch()
        {
            do
            {
                for (int rowNumber = 0; rowNumber < _cells.GetLength(0); rowNumber++)
                {
                    for (int cellNumber = 0; cellNumber < _cells.GetLength(1); cellNumber++)
                    {
                        var horizontalCellsMatches = IsNextCellsMatches(rowNumber, cellNumber, HorizontalAxis);
                        var verticalCellsMatches = IsNextCellsMatches(rowNumber, cellNumber, VerticalAxis);

                        if (horizontalCellsMatches || verticalCellsMatches)
                        {
                            Debug.Log("Match!");
                        }
                    }
                }
            } while (true);
        }

        public bool IsNextCellsMatches(int rowNumber, int cellNumber, string axis)
        {
            var currentCellData = _cells[rowNumber, cellNumber].GetCellData();
            _cellMatchList.Add(currentCellData);
            
            if (axis == HorizontalAxis)
            {
                for (int i = 0; i < _cells.GetLength(0); i++)
                {
                    var nextCellData = _cells[rowNumber + 1, cellNumber].GetCellData();
                    if (currentCellData == nextCellData)
                    {
                        _cellMatchList.Add(nextCellData);
                    }
                }

                if (_cellMatchList.Count >= MinCellsToMatch)
                {
                    foreach (var matchedCell in _cellMatchList)
                    {
                        
                    }
                }
            }
            else if (axis == VerticalAxis)
            {
                var nextCellData = _cells[rowNumber, cellNumber + 1].GetCellData();
            }
            else
            {
                Debug.Log("MatchController: Wrong Axis!");
            }
            
            _cellMatchList.Clear();
            return false;
        }
    }
}