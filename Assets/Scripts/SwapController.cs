using UnityEngine;

namespace DefaultNamespace
{
    public class SwapController
    {
        private CellController[,] _cells;

        public SwapController(CellController[,] cells)
        {
            _cells = cells;
        }
        
        public bool SwapCells(Vector2Int firstCellIndex, Vector2Int secondCellIndex)
        {
            if (IsSwappable(firstCellIndex, secondCellIndex))
            {
                var firstCell = _cells[firstCellIndex.x, firstCellIndex.y];
                var firstCellData = firstCell.GetCellData();
            
                var secondCell = _cells[secondCellIndex.x, secondCellIndex.y];
                var secondCellData = secondCell.GetCellData();

                if (firstCellData == secondCellData)
                {
                    Debug.Log("BoardModel: Can't swap! Reason: Same cell type");
                    Debug.Log($"BoardModel: First Cell {firstCellIndex} : Second Cell {secondCellIndex}. Swap result: False");
                    return false;
                }
            
                var firstCellTempData = firstCell.GetCellData();
                firstCell.SetCellData(secondCell.GetCellData());
                secondCell.SetCellData(firstCellTempData);

                Debug.Log($"BoardModel: First Cell {firstCellIndex} : Second Cell {secondCellIndex}. Swap result: True");

                return true;
            }
            
            Debug.Log($"BoardModel: First Cell {firstCellIndex} : Second Cell {secondCellIndex}. Swap result: False");
            return false;
        }

        private bool IsSwappable(Vector2Int firstCellIndex, Vector2Int secondCellIndex)
        {
            if (firstCellIndex == secondCellIndex)
            {
                Debug.Log("BoardModel: Can't swap! Reason: Same cell index");
                return false;
            }

            var horizontallySwappable = firstCellIndex.y == secondCellIndex.y + 1 || firstCellIndex.y == secondCellIndex.y - 1;

            var verticallySwappable = firstCellIndex.x == secondCellIndex.x + 1 || firstCellIndex.x == secondCellIndex.x - 1;

            if (horizontallySwappable && verticallySwappable)
            {
                Debug.Log($"BoardModel: Can't swap! Reason: Horizontally Swappable: {horizontallySwappable} + Vertically Swappable: {verticallySwappable} = False");
                return false;
            }

            if (horizontallySwappable || verticallySwappable)
            {
                return true;
            }

            Debug.Log($"BoardModel: Can't swap! Reason: Horizontally Swappable: {horizontallySwappable} : Vertically Swappable: {verticallySwappable}");
            
            return false;
        }
    }
}