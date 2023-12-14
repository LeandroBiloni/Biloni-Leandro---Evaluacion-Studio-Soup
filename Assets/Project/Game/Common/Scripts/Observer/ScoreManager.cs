using Game.Enemies;
using ObserverPattern;
using ServiceLocating;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ScoreSystem
{
    public class ScoreManager : MonoBehaviour, IScoreService, IObserver
    {
        [SerializeField] private TextMeshProUGUI _levelScore;

        private int _currentScore;
        private int _highScore;

        private Dictionary<string, Action<object>> _actionsDic = new Dictionary<string, Action<object>>();

        private void Start()
        {
            ServiceLocator.Instance.RegisterService<IScoreService>(this);
            _levelScore.SetText("0");

            _actionsDic.Add("AsteroidDeath", AddScore);
        }

        public ScoreManager GetScoreManager()
        {
            return this;
        }

        private void AddScore(object obj)
        {
            Asteroid asteroid = obj as Asteroid;

            _currentScore += asteroid.GetScore();

            _levelScore.SetText(_currentScore.ToString());
        }

        public void Notify<T>(string action, T observed)
        {
            if (!_actionsDic.ContainsKey(action))
                return;

            _actionsDic[action](observed);
        }
    }
}
