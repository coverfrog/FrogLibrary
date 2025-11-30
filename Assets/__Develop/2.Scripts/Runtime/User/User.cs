using UnityEngine;

namespace Develop
{
    public class User : IUser
    {
        private UserData _userData;
        
        public void Initialize()
        {
            // 유저 데이터 초기화
            _userData = new UserData();
        }
    }
}
