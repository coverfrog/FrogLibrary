using UnityEngine;

namespace FrogLibrary
{
    public abstract class StateMachineMono<TCtrl> : MonoBehaviour
    {
        private TCtrl _ctrl;
        
        private StateMachine<TCtrl> _stateMachine = new();

        public void Initialize(TCtrl ctrl)
        {
            _ctrl = ctrl;
        }
        
        protected abstract void AddStates();
        
        protected abstract void MakeTransitions();
    }
}