using UnityEngine;

namespace Gameplay.Match3.Controllers
{
    public class ScoreController
    {
        private int _score;

        public int Score => _score;
        
        public void AddScore()
        {
            _score++;
        }

        public void AddScores(int score)
        {
            if (score > 0)
            {
                _score += score;
            }
            else
            {
                Debug.LogError("Can't add less than 1 score!");
            }
        }
    }
}