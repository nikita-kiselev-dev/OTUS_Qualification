using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class FallController : MonoBehaviour
    {
        private const float fallInterval = 0.06f;
        
        private CellController[,] _cells;
        private CellData _cellEmptyData;

        private UnityAction _updateBoardView;
        private UnityAction _findMatchAfterFall;

        public void Init(
            CellController[,] cells,
            CellData cellEmptyData,
            UnityAction updateBoardView,
            UnityAction findMatchAfterFall)
        {
            _cells = cells;
            _cellEmptyData = cellEmptyData;
            _updateBoardView = updateBoardView;
            _findMatchAfterFall = findMatchAfterFall;
        }

        public void StartCellFallCoroutine()
        {
            StartCoroutine(StartCellFall());
        }

        public IEnumerator StartCellFall()
        {
            bool needNewIteration = false;
            bool needFallAnimation = false;
            
            do
            {
                for (int rowNumber = 0; rowNumber < _cells.GetLength(0) - 1; rowNumber++)
                {
                    var nextRowNumber = rowNumber + 1;
                    for (int cellNumber = 0; cellNumber < _cells.GetLength(1); cellNumber++)
                    {
                        if (rowNumber == 0 && cellNumber == 0)
                        {
                            needNewIteration = false;
                        }
                        
                        var currentCell = _cells[rowNumber, cellNumber];

                        if (currentCell.GetCellData() == _cellEmptyData)
                        {
                            continue;
                        }
                        
                        if (IsBottomCellIsEmpty(nextRowNumber, cellNumber))
                        { 
                            var bottomCell = _cells[nextRowNumber, cellNumber];
                        
                            bottomCell.SetCellData(currentCell.GetCellData());
                            currentCell.SetCellData(_cellEmptyData);
                            
                            _updateBoardView?.Invoke();
                            
                            needNewIteration = true;
                            needFallAnimation = true;
                        }
                    }
                }
                
                if (needFallAnimation)
                {
                    needFallAnimation = false;
                    yield return new WaitForSeconds(fallInterval);
                }
                
            } 
            while (needNewIteration);
            
            _findMatchAfterFall?.Invoke();
            _updateBoardView?.Invoke();
        }

        private bool IsBottomCellIsEmpty(int nextRowNumber, int cellNumber)
        {
            var nextCellIsEmpty = _cells[nextRowNumber, cellNumber].GetCellData() == _cellEmptyData;
            if (nextCellIsEmpty)
            {
                return true;
            }

            return false;
        }
    }
}