using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class ExitGame : MonoBehaviour
    {
        public void QuitGame()
        {
            Debug.Log("Quit Game!");
            Application.Quit();
        }
    }
}

