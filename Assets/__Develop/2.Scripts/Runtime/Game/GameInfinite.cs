using UnityEngine;

namespace Develop
{
    public class GameInfinite : IGame
    {
        private IBattleSystem _battleSystem;
        
        public void Begin()
        {
            Debug.Log("[Game] 게임 시작 - Infinite 모드");
            
            _battleSystem = new BattleSystem();
        }
    }
}