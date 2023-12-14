using UnityEngine;

namespace ScoreSystem
{
    [CreateAssetMenu(fileName = "Highscore Temporal Save", menuName = "Scriptable Objects/Temporal Highscore")]
    public class CurrentHighscoreSave : ScriptableObject
    {
        public int highscore;

    }
}
