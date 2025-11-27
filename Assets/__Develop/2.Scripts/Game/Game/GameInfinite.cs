using FrogLibrary;
using UnityEngine;

[CreateAssetMenu(menuName = "OneWeapon/Game/Infinite", fileName = "Infinite")]
public class GameInfinite : GameBase
{
    [Header("# Entity")]
    [SerializeField] private Entity _playerEntity;
    
    public override void Launch(IUser user)
    {
        
    }
}
