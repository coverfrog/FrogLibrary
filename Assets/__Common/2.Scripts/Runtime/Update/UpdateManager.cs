using System;
using System.Collections.Generic;

namespace FrogLibrary
{
    public class UpdateManager : Singleton<UpdateManager>
    {
        private readonly List<IUpdateAble> _updateList = new List<IUpdateAble>();
        private readonly List<IFixedUpdateAble> _fixedUpdateList = new List<IFixedUpdateAble>();
    
        public void AddUpdate(IUpdateAble able) => _updateList.Add(able);
        public void AddFixedUpdate(IFixedUpdateAble able) => _fixedUpdateList.Add(able);
    
        public void RemoveUpdate(IUpdateAble able) => _updateList.Remove(able);
        public void RemoveFixedUpdate(IFixedUpdateAble able) => _fixedUpdateList.Remove(able);

        private void Update()
        {
            foreach (IUpdateAble able in _updateList)
            {
                able?.OnUpdate();
            }
        }

        private void FixedUpdate()
        {
            foreach (IFixedUpdateAble able in _fixedUpdateList)
            {
                able?.OnFixedUpdate();
            }
        }
    }
}