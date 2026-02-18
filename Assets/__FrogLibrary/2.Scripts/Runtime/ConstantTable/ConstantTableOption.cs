using UnityEngine;

namespace FrogLibrary
{
    [CreateAssetMenu(menuName = AssetMenuNames.k_constantTableOptionMenuName, fileName = AssetMenuNames.k_constantTableOptionFileName)]
    public class ConstantTableOption : IdentifiedObject
    {
        [Header("# Constant Table")]
        [SerializeField] private UnityDictionary<string, string> m_matchDict = new();   // 엑셀 이름 - So 이름
    }
}