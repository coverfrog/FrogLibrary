using System.Collections.Generic;
using UnityEngine;

namespace FrogLibrary
{
    [CreateAssetMenu(menuName = AssetMenuNames.k_bootOptionMenuName, fileName = AssetMenuNames.k_bootOptionFileName)]
    public class BootOption : IdentifiedObject
    {
        [Header("# Boot")]
        [SerializeField] private bool m_isDontDestroyOnLoad = true;
        [SerializeField] private List<string> m_instanceAddressList = new();

        public bool IsDontDestroyOnLoad => m_isDontDestroyOnLoad;
        
        public IReadOnlyList<string> InstanceAddressList => m_instanceAddressList;
    }
}