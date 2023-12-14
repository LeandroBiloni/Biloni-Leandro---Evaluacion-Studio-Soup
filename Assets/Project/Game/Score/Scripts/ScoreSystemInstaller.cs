using Game;
using Game.Ship;
using UnityEngine;

namespace ScoreSystem
{
    public class ScoreSystemInstaller : Installer
    {
        [Header("References")]
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private CurrentHighscoreSave _highscoreSave;
        [SerializeField] private PlayerShip _playerShip;

        private ScoreManager _scoreManager;

        public override void Install()
        {
            IScoreView view = _scoreView;

            ScorePresenter presenter = new ScorePresenter(view);

            _scoreManager = new ScoreManager(presenter, _highscoreSave, _playerShip);
        }

        private void OnDestroy()
        {
            _scoreManager.Dispose();
        }
    }
}

