using System.Collections.Generic;
using UnityEngine;

namespace FrogLibrary
{
    [CreateAssetMenu(menuName = AssetMenuNames.k_bootOptionMenuName, fileName = AssetMenuNames.k_bootOptionFileName)]
    public class BootOption : IdentifiedObject
    {
        [Header("# Boot")]
        [SerializeField] private List<string> m_instanceAddressList = new();

        public IReadOnlyList<string> InstanceAddressList => m_instanceAddressList;
    }
}