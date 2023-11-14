namespace Gameplay.Match3.Controllers
{
    public class WinConditionController
    {
        private int _scoreGoal;

        public bool IsLevelWin(int scores)
        {
            if (scores >= _scoreGoal)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SetScoreGoal(int scoreGoal)
        {
            _scoreGoal = scoreGoal;
        }
    }
}