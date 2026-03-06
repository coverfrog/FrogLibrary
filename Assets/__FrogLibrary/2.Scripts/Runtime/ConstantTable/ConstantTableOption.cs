using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FrogLibrary
{
    [CreateAssetMenu(menuName = "FrogLibrary/ConstantTableOption", fileName = "ConstantTableOption")]
    public class ConstantTableOption : ScriptableObject
    {
        [Header("# Constant Table")] 
        [SerializeField] private string m_excelFolderPath = "Assets/";
        [SerializeField] private string m_tableFolderPath = "Assets/";
        [SerializeField] private string m_namespace = "";
        [SerializeField] private List<ConstantNameMatch> m_matches = new List<ConstantNameMatch>();

        public string ExcelFolderPath => m_excelFolderPath;
        
        public string TableFolderPath => m_tableFolderPath;

        public string Namespace => m_namespace;
        
        public IReadOnlyDictionary<string, ConstantNameMatch> Matches => m_matches.ToDictionary(m => m.excelName);
    }
}