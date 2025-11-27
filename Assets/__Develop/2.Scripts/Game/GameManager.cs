using System;
using System.Collections.Generic;
using FrogLibrary;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private List<GameBase> _gameList = new();
    
    public IReadOnlyList<IGame> GameList => _gameList;
    
    private IUser _user = new User();

    public void Launch(IGame helper)
    {
        Debug.Log($"[GameManager] Launching - {helper.Io.Codename}");
        
        helper.Launch(_user);
    }
}
