using System;
using System.Collections.Generic;
using UnityEngine;

namespace FrogLibrary
{
    public class YieldManager : Singleton<YieldManager>
    {
        private readonly List<IUpdateAvailable> m_updateList = new();
        
        private readonly List<IFixedUpdateAvailable> m_fixedUpdateList = new();
        
        private readonly List<ILateUpdateAvailable> m_lateUpdateList = new();
        
        public void Add(IUpdateAvailable available)
        {
            m_updateList.Add(available);
        }
        
        public void Add(IFixedUpdateAvailable available)
        {
            m_fixedUpdateList.Add(available);
        }
        
        public void Add(ILateUpdateAvailable available)
        {
            m_lateUpdateList.Add(available);
        }
        
        public void Remove(IUpdateAvailable available)
        {
            for (int i = m_updateList.Count - 1; i >= 0; i--)
            {
                if (m_updateList[i] == available) m_updateList.Remove(m_updateList[i]);
            }
        }
        
        public void Remove(IFixedUpdateAvailable available)
        {
            for (int i = m_fixedUpdateList.Count - 1; i >= 0; i--)
            {
                if (m_fixedUpdateList[i] == available) m_fixedUpdateList.Remove(m_fixedUpdateList[i]);
            }
        }
        
        public void Remove(ILateUpdateAvailable available)
        {
            for (int i = m_lateUpdateList.Count - 1; i >= 0; i--)
            {
                if (m_lateUpdateList[i] == available) m_lateUpdateList.Remove(m_lateUpdateList[i]);
            }
        }

        private void Update()
        {
            for (int i = m_updateList.Count - 1; i >= 0; i--)
            {
                if (m_updateList[i] == null) 
                    m_updateList.Remove(m_updateList[i]);
                else
                    m_updateList[i].OnUpdate();
            }
        }

        private void FixedUpdate()
        {
            for (int i = m_fixedUpdateList.Count - 1; i >= 0; i--)
            {
                if (m_fixedUpdateList[i] == null) 
                    m_fixedUpdateList.Remove(m_fixedUpdateList[i]);
                else
                    m_fixedUpdateList[i].OnFixedUpdate();
            }
        }

        private void LateUpdate()
        {
            for (int i = m_lateUpdateList.Count - 1; i >= 0; i--)
            {
                if (m_lateUpdateList[i] == null) 
                    m_lateUpdateList.Remove(m_lateUpdateList[i]);
                else
                    m_lateUpdateList[i].OnLateUpdate();
            }
        }
    }
}
