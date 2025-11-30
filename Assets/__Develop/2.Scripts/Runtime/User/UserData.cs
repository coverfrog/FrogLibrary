using System;
using UnityEngine;

namespace Develop
{
    [Serializable]
    public class UserData
    {
        // 임의 빈 값
        // 초기화 안할 시 사용 가능한 테스트
        public const string KNullId = "tetstjtseojtoisetiohesiohtoisehtihesihtieshtoihseiohtoieshiothesiohtiehirhireerrrr";
        
        [SerializeField] private string _id = KNullId;
        
        public string Id => _id;
        
        public UserData()
        {
            
        }
    }
}