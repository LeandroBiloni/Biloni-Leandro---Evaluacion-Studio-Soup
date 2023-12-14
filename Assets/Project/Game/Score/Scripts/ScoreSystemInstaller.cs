using Game.Ship;
using System.Collections;
using UnityEngine;

namespace ScoreSystem
{
    public class ScoreSystemInstaller : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private CurrentHighscoreSave _highscoreSave;
        [SerializeField] private PlayerShip _playerShip;

        private ScoreManager _scoreManager;
        // Start is called before the first frame update
        private IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();

            Install();
        }

        public void Install()
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

