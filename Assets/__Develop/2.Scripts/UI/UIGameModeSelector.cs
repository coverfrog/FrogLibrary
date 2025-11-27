using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameModeSelector : MonoBehaviour
{
    [SerializeField] private UIGameModeSelect _origin;

    private Dictionary<string, UIGameModeSelect> _selectDict = new();
    
    private void Start()
    {
        _origin.gameObject.SetActive(false);
        
        var gameList = GameManager.Instance.GameList;
        
        foreach (IGame g in gameList)
        {
            var key = g.Io.Codename;
            
            if (_selectDict.ContainsKey(key))
            {
                Debug.Assert(false, "Error");
                continue;
            }

            var ins = Instantiate(_origin, transform).Init(g, OnClick);
            
            _selectDict.Add(key, ins);
        }
    }

    private void OnClick(IGame game)
    {
        gameObject.SetActive(false);
        
        GameManager.Instance.Launch(game);
    }
}
