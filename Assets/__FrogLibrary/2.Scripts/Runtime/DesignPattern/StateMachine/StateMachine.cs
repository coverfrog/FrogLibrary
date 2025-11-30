using System.Collections.Generic;

namespace FrogLibrary
{
    public class StateMachine<TCtrl>
    {
        private Dictionary<int, StateLayer<TCtrl>> _layers = new();
    }
}