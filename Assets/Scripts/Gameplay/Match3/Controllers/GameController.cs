using System.Collections.Generic;
using Audio;
using DefaultNamespace;
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
        [Space, Header("Controllers"), Space]
        [SerializeField] private FallController m_FallController;
        [SerializeField] private SoundController m_SoundController;
        [SerializeField] private MusicController m_MusicController;

        private List<RectTransform> _rows;
        private BoardController _boardController;

        private void Start()
        {
            BuildBoard();
            
            _boardController = new BoardController(
                m_CellPrefab,
                m_CellEmptyData,
                m_CellViewData,
                _rows,
                m_FallController);
            
            _boardController.CreateCellMatrix(
                m_RowsNumber,
                m_CellsNumber,
                m_CellDatas);
            
            m_SoundController.Init();
            m_MusicController.Init();
            
            MusicController.Instance.PlaySoundLoop("Background");
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