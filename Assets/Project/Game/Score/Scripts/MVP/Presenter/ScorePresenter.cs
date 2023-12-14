namespace ScoreSystem
{
    public class ScorePresenter : IScorePresenter
    {
        private readonly IScoreView _view;

        public ScorePresenter(IScoreView view)
        {
            _view = view;
        }

        public void UpdateLevelScore(int levelScore)
        {
            _view.UpdateLevelScore(levelScore.ToString());
        }

        public void UpdateHighScore(int highScore)
        {
            _view.UpdateHighScore(highScore.ToString());
        }
    }
}
