using UnityEngine;

namespace ScoreSystem
{
    public interface IScoreView
    {
        void UpdateLevelScore(string levelScore);
        void UpdateHighScore(string highScore);
        GameObject GetGameObject();
    }
}
