using Game.Enemies;
using Game.Ship;
using ObserverPattern;
using ServiceLocating;
using System;
using System.Collections.Generic;

namespace ScoreSystem
{
    public class ScoreManager : IScoreService, IObserver, IDisposable
    {
        private readonly ScorePresenter _presenter;
        private readonly CurrentHighscoreSave _highscoreSave;
        private readonly PlayerShip _playerShip;

        private int _currentScore = 0;
        private int _highScore = 0;

        private Dictionary<string, Action<object>> _actionsDic = new Dictionary<string, Action<object>>();

        public ScoreManager(ScorePresenter presenter, CurrentHighscoreSave highscoreSave, PlayerShip playerShip)
        {
            _presenter = presenter;
            _highscoreSave = highscoreSave;

            _playerShip = playerShip;
            _playerShip.Subscribe(this);

            _presenter.UpdateLevelScore(0);

            _highScore = _highscoreSave.highscore;

            _presenter.UpdateHighScore(_highScore);

            ServiceLocator.Instance.RegisterService<IScoreService>(this);

            _actionsDic.Add("AsteroidDeath", AddScore);
            _actionsDic.Add("ShipDeath", OnShipDeath);            
        }

        public ScoreManager GetScoreManager()
        {
            return this;
        }

        private void AddScore(object obj)
        {
            Asteroid asteroid = obj as Asteroid;

            _currentScore += asteroid.GetScore();

            _presenter.UpdateLevelScore(_currentScore);

            UpdateHighscore();
        }

        private void OnShipDeath(object obj)
        {
            UpdateHighscore();
        }

        private void UpdateHighscore()
        {
            if (_currentScore > _highScore)
            {
                _highScore = _currentScore;

                _highscoreSave.highscore = _highScore;

                _presenter.UpdateHighScore(_highScore);
            }
        }

        public void Notify<T>(string action, T observed)
        {
            if (!_actionsDic.ContainsKey(action))
                return;

            _actionsDic[action](observed);
        }

        public void Dispose()
        {
            _playerShip.Unsuscribe(this);
        }
    }
}
