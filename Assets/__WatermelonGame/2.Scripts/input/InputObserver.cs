using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace WatermelonGame
{
    public class InputObserver : MonoBehaviour, IDisposable
    {
        public event Action<bool> OnClick;
        
        private InputModule _module;
        
        public void Init()
        {
            _module = new InputModule();
            _module.Enable();

            _module.Default.Click.started  += _ => OnClick?.Invoke(true);
            _module.Default.Click.canceled += _ => OnClick?.Invoke(false);
        }

        public void Dispose()
        {
            _module?.Dispose();
        }
    }
}