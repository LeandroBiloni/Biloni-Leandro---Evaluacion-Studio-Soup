using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class InstallersInitializer : MonoBehaviour
    {
        [SerializeField] private List<Installer> _installers = new List<Installer>();

        private void Awake()
        {
            foreach (Installer installer in _installers)
            {
                installer.Install();
            }
        }
    }
}

