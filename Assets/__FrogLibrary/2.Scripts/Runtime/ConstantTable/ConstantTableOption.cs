using System.Collections.Generic;
using UnityEngine;

namespace FrogLibrary
{
    [CreateAssetMenu(menuName = AssetMenuNames.k_constantTableOptionMenuName, fileName = AssetMenuNames.k_constantTableOptionFileName)]
    public class ConstantTableOption : IdentifiedObject
    {
        [Header("# Constant Table")] 
        [SerializeField] private string m_excelFolderPath = "Assets/";
        [SerializeField] private string m_tableFolderPath = "Assets/";
        [SerializeField] private string m_namespace = "";
        [SerializeField] private UnityDictionary<string, string> m_matchDict = new();   // 엑셀 이름 - So 이름

        public string ExcelFolderPath => m_excelFolderPath;
        
        public string TableFolderPath => m_tableFolderPath;

        public string Namespace => m_namespace;
        
        public IReadOnlyDictionary<string, string> MatchDict => m_matchDict.ToReadOnlyDictionary();
    }
}