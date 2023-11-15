using System.Collections.Generic;
using Audio;
using DefaultNamespace;
using DefaultNamespace.Other;
using Other.Controllers;
using UnityEngine;

namespace Gameplay.Match3.Controllers
{
    public class BoardController
    {
        private CellController[,] m_Cells;

        private GameObject _cellPrefab;
        private CellData _cellEmptyData;
        private CellViewData _cellViewData;
        private List<RectTransform> _rows;

        private SwapController _swapController;
        private MatchController _matchController;
        private FallController _fallController;
        
        private ScoreController _scoreController;
        private WinConditionController _winConditionController;

        private CoreMenuController _coreMenuController;

        private WinPopupController _winPopupController;
        
        public BoardController(
            GameObject cellPrefab,
            CellData cellEmptyData,
            CellViewData cellViewData,
            List<RectTransform> rows,
            FallController fallController,
            ScoreController scoreController,
            WinConditionController winConditionController,
            CoreMenuController coreMenuController,
            WinPopupController winPopupController)
        {
            _cellPrefab = cellPrefab;
            _cellEmptyData = cellEmptyData;
            _cellViewData = cellViewData;
            _rows = rows;
            _fallController = fallController;
            _scoreController = scoreController;
            _winConditionController = winConditionController;
            _coreMenuController = coreMenuController;
            _winPopupController = winPopupController;
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
                        _rows[rowNumber],
                        _cellEmptyData);
                    
                    CellConfig cellConfig = new CellConfig($"Cell [{rowNumber}, {cellNumber}]");
                    m_Cells[rowNumber,cellNumber].UpdateCell(cellConfig);
                    m_Cells[rowNumber,cellNumber].Init();
                }
            }

            CreateBoardControllers();
        }

        private void CreateBoardControllers()
        {
            _matchController = new MatchController(m_Cells, _cellEmptyData);
            _matchController.Init();
            
            _swapController = new SwapController(m_Cells, _matchController);

            _fallController.Init(m_Cells, _cellEmptyData, UpdateBoardView, FindMatchAfterFall);
        }

        public void SwapCells(Vector2Int firstCellIndex, Vector2Int secondCellIndex)
        {
            var firstCellIsEmpty = m_Cells[firstCellIndex.x, firstCellIndex.y].CompareCellData(_cellEmptyData);
            var secondCellIsEmpty = m_Cells[secondCellIndex.x, secondCellIndex.y].CompareCellData(_cellEmptyData);
            
            if (firstCellIsEmpty || secondCellIsEmpty)
            {
                return;
            }
            
            bool isSwapped = _swapController.SwapCells(firstCellIndex, secondCellIndex);
            if (isSwapped)
            {
                SoundController.Instance.PlaySound(SoundList.TileSwap);
                
                SetScore();
                
                UpdateBoardView();

                _fallController.StartCellFallCoroutine();
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

        private void FindMatchAfterFall()
        {
            bool needNewIteration;
            do
            {
                needNewIteration = _matchController.FindMatchAfterFall();
                
                SetScore();
                
                if (needNewIteration)
                {
                    _fallController.StartCellFallCoroutine();
                }

            } while (needNewIteration);
        }

        private void SetScore()
        {
            var cellMatchList = _matchController.GetCellMatchList();
            
            if (0 >= cellMatchList.Count)
            {
                return;
            }
            
            foreach (var currentCell in cellMatchList)
            {
                _scoreController.AddScore();
                _coreMenuController.SetScoreText(_scoreController.Score);
                _coreMenuController.SetSliderValue(_scoreController.Score);
                    
                currentCell.SetCellData(_cellEmptyData);
            }
            
            _matchController.ClearCellMatchList();
                
            var isWin = _winConditionController.IsLevelWin(_scoreController.Score);

            if (isWin)
            {
                _winPopupController.Open();
            }
        }
    }
}