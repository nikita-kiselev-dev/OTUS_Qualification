using UnityEngine;

namespace Gameplay.Match3.Controllers
{
    public class SwapController
    {
        private CellController[,] _cells;
        private MatchController _matchController;

        public SwapController(CellController[,] cells, MatchController matchController)
        {
            _cells = cells;
            _matchController = matchController;
        }
        
        public bool SwapCells(Vector2Int firstCellIndex, Vector2Int secondCellIndex)
        {
            if (IsSwappable(firstCellIndex, secondCellIndex))
            {
                var firstCell = _cells[firstCellIndex.x, firstCellIndex.y];
                var firstCellData = firstCell.CellData;
            
                var secondCell = _cells[secondCellIndex.x, secondCellIndex.y];
                var secondCellData = secondCell.CellData;

                if (firstCellData == secondCellData)
                {
                    return false;
                }
                
                DataSwap(firstCell, secondCell);
                
                var firstCellMatch = _matchController.FindMatch(firstCellIndex.x, firstCellIndex.y);
                var secondCellMatch = _matchController.FindMatch(secondCellIndex.x, secondCellIndex.y);
                
                DataSwap(firstCell, secondCell);

                if (!firstCellMatch && !secondCellMatch)
                {
                    return false;
                }
                
                DataSwap(firstCell, secondCell);

                return true;
            }
            return false;
        }

        private bool IsSwappable(Vector2Int firstCellIndex, Vector2Int secondCellIndex)
        {
            if (firstCellIndex == secondCellIndex)
            {
                return false;
            }

            var horizontallySwappable = firstCellIndex.y == secondCellIndex.y + 1 || firstCellIndex.y == secondCellIndex.y - 1;

            var verticallySwappable = firstCellIndex.x == secondCellIndex.x + 1 || firstCellIndex.x == secondCellIndex.x - 1;

            if (horizontallySwappable && verticallySwappable)
            {
                return false;
            }

            if (horizontallySwappable || verticallySwappable)
            {
                return true;
            }
            
            return false;
        }

        private void DataSwap(CellController firstCell, CellController secondCell)
        {
            var firstCellTempData = firstCell.CellData;
            firstCell.SetCellData(secondCell.CellData);
            secondCell.SetCellData(firstCellTempData);
        }
    }
}