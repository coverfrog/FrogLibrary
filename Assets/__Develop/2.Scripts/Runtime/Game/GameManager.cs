using System;
using FrogLibrary;

namespace Develop
{
    public class GameManager : Singleton<GameManager>
    {
        private IUser _user;
        private IGame _game;

        public void Start()
        {
            // 유저 데이터 초기화
            _user = new User();
            _user.Initialize();
            
            // 게임 시작
            _game = new GameInfinite();
            _game.Begin();
        }
    }
}