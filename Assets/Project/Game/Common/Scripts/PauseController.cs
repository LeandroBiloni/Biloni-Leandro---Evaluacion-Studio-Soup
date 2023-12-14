using Game.Ship;
using ObserverPattern;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public class PauseController : MonoBehaviour, IObserver
    {
        [Header("References")]
        [SerializeField] private InputActionAsset _inputActionAsset;
        [SerializeField] private GameObject _pauseWindowObj;
        [SerializeField] private PauseWindow _pauseWindow;

        private bool _isPaused = false;
        private InputActionMap _shipActionMap;

        private Dictionary<string, Action<object>> _actionsDic = new Dictionary<string, Action<object>>();

        // Start is called before the first frame update
        void Start()
        {
            _pauseWindow.Subscribe(this);

            _pauseWindowObj.SetActive(false);
            
            _inputActionAsset.actionMaps[1].Enable();

            _shipActionMap = _inputActionAsset.actionMaps[0];

            _inputActionAsset.FindAction("Pause").performed += OnPauseKeyPressed;

            _actionsDic.Add("ClosePause", OnPauseScreenClosed);
        }

        private void OnPauseScreenClosed(object obj)
        {
            if (_inputActionAsset.FindAction("Pause").IsPressed())
                return;

            TogglePauseScreen();
        }

        private void OnPauseKeyPressed(InputAction.CallbackContext context)
        {
            TogglePauseScreen();
        }

        private void TogglePauseScreen()
        {
            if (_isPaused)
            {
                _isPaused = false;
                _pauseWindowObj.SetActive(false);
                _shipActionMap.Enable();
            }
            else
            {
                _isPaused = true;
                _pauseWindowObj.SetActive(true);
                _shipActionMap.Disable();
            }
        }

        private void OnDestroy()
        {
            _inputActionAsset.FindAction("Pause").performed -= OnPauseKeyPressed;
        }

        public void Notify<T>(string action, T observed)
        {
            if (!_actionsDic.ContainsKey(action))
                return;

            _actionsDic[action](observed);
        }
    }
}

