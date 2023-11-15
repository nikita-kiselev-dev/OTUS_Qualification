using System.Collections.Generic;
using Audio;
using DefaultNamespace;
using DefaultNamespace.Other;
using Unity.VisualScripting;
using UnityEngine;

namespace Gameplay.Match3.Controllers
{
    public class MatchController
    {
        private CellController[,] _cells;
        private CellData _cellEmptyData;
        
        private HashSet<CellController> _cellMatchList;
        private HashSet<CellController> _horizontalCellMatchList;
        private HashSet<CellController> _verticalCellMatchList;
        
        private const int MinCellsToMatch = 3;
        
        public MatchController(CellController[,] cells, CellData cellEmptyData)
        {
            _cells = cells;
            _cellEmptyData = cellEmptyData;
        }

        public void Init()
        {
            _cellMatchList = new HashSet<CellController>();
            _horizontalCellMatchList = new HashSet<CellController>();
            _verticalCellMatchList = new HashSet<CellController>();
        }

        public bool FindMatch(int rowNumber, int cellNumber)
        {
            if (IsMatch(rowNumber, cellNumber))
            {
                SoundController.Instance.PlaySound(SoundList.TileMatch);
                return true;
            }

            return false;
        }

        public bool FindMatchAfterFall()
        {
            bool needIteration = false;
            for (int rowNumber = 0; rowNumber < _cells.GetLength(0); rowNumber++)
            {
                for (int cellNumber = 0; cellNumber < _cells.GetLength(1); cellNumber++)
                {
                    if (!needIteration)
                    {
                        needIteration = FindMatch(rowNumber, cellNumber);
                    }
                }
            }

            return needIteration;
        }

        public HashSet<CellController> GetCellMatchList()
        {
            return _cellMatchList;
        }

        public void ClearCellMatchList()
        {
            _cellMatchList.Clear();
        }

        public bool IsMatch(int rowNumber, int cellNumber)
        {
            var currentCellController = _cells[rowNumber, cellNumber];

            if (currentCellController.CompareCellData(_cellEmptyData))
            {
                return false;
            }
            
            _horizontalCellMatchList.Add(_cells[rowNumber, cellNumber]);
            _verticalCellMatchList.Add(_cells[rowNumber, cellNumber]);
            
            bool horizontalMatch = IsHorizontalMatch(currentCellController, rowNumber, cellNumber);
            bool verticalMatch = IsVerticalMatch(currentCellController, rowNumber, cellNumber);

            if (horizontalMatch)
            {
                _cellMatchList.AddRange(_horizontalCellMatchList);
            }

            if (verticalMatch)
            {
                _cellMatchList.AddRange(_verticalCellMatchList);
            }
            
            _horizontalCellMatchList.Clear();
            _verticalCellMatchList.Clear();

            return horizontalMatch || verticalMatch;
        }

        private bool IsHorizontalMatch(
            CellController currentCellController,
            int rowNumber,
            int cellNumber)
        {
            for (int i = cellNumber - 1; i >= 0; i--)
            {
                var nextCellData = _cells[rowNumber, i].CellData;
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
                var nextCellData = _cells[rowNumber, i].CellData;
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

            if (_horizontalCellMatchList.Count >= MinCellsToMatch)
            {
                return true;
            }

            _horizontalCellMatchList.Clear();
            return false;
        }
        
        private bool IsVerticalMatch(
            CellController currentCellController,
            int rowNumber,
            int cellNumber)
        {
            for (int i = rowNumber - 1; i >= 0; i--)
            {
                var nextCellData = _cells[i, cellNumber].CellData;
                var nextCellController = _cells[i, cellNumber];
                if (currentCellController.CompareCellData(nextCellData))
                {
                    _verticalCellMatchList.Add(nextCellController);
                }
                else
                {
                    break;
                }
            }
            
            for (int i = rowNumber + 1; i < _cells.GetLength(0); i++)
            {
                var nextCellData = _cells[i, cellNumber].CellData;
                var nextCellController = _cells[i, cellNumber];
                if (currentCellController.CompareCellData(nextCellData))
                {
                    _verticalCellMatchList.Add(nextCellController);
                }
                else
                {
                    break;
                }
            }
            
            if (_verticalCellMatchList.Count >= MinCellsToMatch)
            {
                return true;
            }

            _verticalCellMatchList.Clear();
            return false;
        }
        
    }
}