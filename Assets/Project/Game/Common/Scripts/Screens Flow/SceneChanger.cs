using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class  SceneChanger : MonoBehaviour
    {
        [SerializeField] private string _sceneToLoad;

        public void LoadScene()
        {
            SceneManager.LoadScene(_sceneToLoad);
        }
    }
}

