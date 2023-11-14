using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.Other;
using Other.Controllers;
using UnityEngine;

namespace Gameplay.Match3.Controllers
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private int m_RowsNumber = 8;
        [SerializeField] private int m_CellsNumber = 11;
        [Space]
        [SerializeField] private GameObject m_RowPrefab;
        [SerializeField] private GameObject m_CellPrefab;
        [Space]
        [SerializeField] private RectTransform m_RowsContainer;
        [Space]
        [SerializeField] private List<CellData> m_CellDatas;
        [SerializeField] private CellData m_CellEmptyData;
        [SerializeField] private CellViewData m_CellViewData;
        
        [Space, Header("Game Controllers"), Space]
        [SerializeField] private FallController m_FallController;
        
        [Space, Header("UI Controllers"), Space]
        [SerializeField] private CoreMenuController m_CoreMenuController;

        [Space, Header("Popup Controllers"), Space]
        [SerializeField] private WinPopupController m_WinPopupController;

        private List<RectTransform> _rows;
        private BoardController _boardController;
        private ScoreController _scoreController;
        private WinConditionController _winConditionController;

        private const int _scoreGoal = 20;

        private void Start()
        {
            _scoreController = new ScoreController();
            m_CoreMenuController.SetSliderMaxValue(_scoreGoal);

            _winConditionController = new WinConditionController();
            _winConditionController.SetScoreGoal(_scoreGoal);
            
            BuildBoard();
            
            _boardController = new BoardController(
                m_CellPrefab,
                m_CellEmptyData,
                m_CellViewData,
                _rows,
                m_FallController,
                _scoreController,
                _winConditionController,
                m_CoreMenuController,
                m_WinPopupController);
            
            _boardController.CreateCellMatrix(
                m_RowsNumber,
                m_CellsNumber,
                m_CellDatas);

        }
        
        public void BuildBoard()
        {
            _rows = new List<RectTransform>();

            for (int i = 0; i < m_RowsNumber; i++)
            {
                var row = Instantiate(m_RowPrefab, m_RowsContainer);
                row.name = $"Row [{i}]";
                _rows.Add(row.GetComponent<RectTransform>());
            }
        }
    }
}