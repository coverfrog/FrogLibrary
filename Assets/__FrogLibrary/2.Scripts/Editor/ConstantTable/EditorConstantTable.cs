#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using ExcelDataReader;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace FrogLibrary
{
    public static class EditorConstantTable
    {
        [MenuItem(AssetMenuNames.k_constantTableRun)]
        public static void Run()
        {
            ConvertExcels();

            AssetDatabase.Refresh();
        }

        private static void ConvertExcels()
        {
            // 경로 추출
            var option = Resources.Load<ConstantTableOption>(AssetMenuNames.k_constantTableOptionFileName);
            var paths = Directory.GetFiles(option.ExcelFolderPath, "*.xlsx");

            // 생성 폴더 없을시 생성
            if (!Directory.Exists(option.TableFolderPath))
                Directory.CreateDirectory(option.TableFolderPath);

            // 경로를 순회
            var matchDict = option.MatchDict;
            foreach (string path in paths)
            {
                // 데이터 추출
                var data = ParseExcel(path);

                // 이름 가져오기
                var assetName = Path.GetFileNameWithoutExtension(path);
                var className = matchDict.GetValueOrDefault(assetName);

                // 저장할 경로와 클래스 타입을 얻는다.
                var assetPath = option.TableFolderPath + "/" + assetName + ".asset";
                var classType = Type.GetType($"{option.Namespace}{(string.IsNullOrEmpty(option.Namespace) ? "." : "")}{className}, Assembly-CSharp");

                // 다이나믹 형식으로 호출 한다.
                ConstantTable so = (ConstantTable)AssetDatabase.LoadAssetAtPath(assetPath, classType);

                // 없으면 새로 생성 한다.
                if (so == null)
                {
                    so = (ConstantTable)ScriptableObject.CreateInstance(classType);
                    AssetDatabase.CreateAsset(so, assetPath);

                    return;
                }

                // 로딩을 시키고 나서 업데이트 한다.
                so.Load(data);
                
                EditorUtility.SetDirty(so);
            }
        }

        private static IReadOnlyDictionary<int, IReadOnlyDictionary<int, IReadOnlyList<object>>> ParseExcel(
            string excelPath)
        {
            var result = new Dictionary<int, IReadOnlyDictionary<int, IReadOnlyList<object>>>();

            try
            {
                using var stream = File.Open(excelPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                using var reader = ExcelReaderFactory.CreateReader(stream);

                using var dataSet = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                });

                for (var s = 0; s < dataSet.Tables.Count; s++)
                {
                    var table = dataSet.Tables[s];

                    var rows = new Dictionary<int, IReadOnlyList<object>>();

                    for (int r = 0; r < table.Rows.Count; r++)
                    {
                        var cols = new List<object>();

                        for (int c = 0; c < table.Columns.Count; c++)
                        {
                            var value = table.Rows[r][c];

                            if (value == null)
                            {
                                break;
                            }

                            cols.Add(value);
                        }

                        rows.Add(r, cols);
                    }

                    result.Add(s, rows);
                }
            }

            catch (Exception e)
            {
                Debug.Assert(false, e.Message);
            }

            return result;
        }
    }
}
#endif