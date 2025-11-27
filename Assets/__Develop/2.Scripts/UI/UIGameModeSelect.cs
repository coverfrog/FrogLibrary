using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGameModeSelect : MonoBehaviour
{
    [Header("# References")]
    [SerializeField] private Button _btn;
    [SerializeField] private TMP_Text _btnText;

    private IGame _game;
    private Action<IGame> _onClick;
    
    public UIGameModeSelect Init(IGame game, Action<IGame> onClick)
    {
        _onClick = onClick;
        _game = game;
        
        _btnText.text = game.Io.Codename;
        
        _btn.onClick.RemoveAllListeners();
        _btn.onClick.AddListener(OnClick);
        
        gameObject.SetActive(true);
        
        return this;
    }

    private void OnClick()
    {
        _onClick?.Invoke(_game);
    }
}
