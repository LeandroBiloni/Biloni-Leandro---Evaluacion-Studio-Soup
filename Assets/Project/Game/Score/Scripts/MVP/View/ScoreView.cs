using TMPro;
using UnityEngine;

namespace ScoreSystem
{
    public class ScoreView : MonoBehaviour, IScoreView
    {
        [Header("References")]
        [SerializeField] private TextMeshProUGUI _levelScore;
        [SerializeField] private TextMeshProUGUI _highScore;

        public void UpdateLevelScore(string levelScore)
        {
            _levelScore.SetText(levelScore);
        }

        public void UpdateHighScore(string highScore)
        {
            _highScore.SetText(highScore);
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }
    }
}
