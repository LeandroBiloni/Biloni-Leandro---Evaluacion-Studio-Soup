using UnityEngine;

namespace ScoreSystem
{
    public interface IScorePresenter
    {
        void UpdateLevelScore(int levelScore);
        void UpdateHighScore(int highScore);
        GameObject GetGameObject();
    }
}
