using System;
using UnityEngine;

[Serializable]
public class StatController
{
    [SerializeField] private StatContext _context;

    #region Getter

    public string Codename => _context.Codename;
    public float Value => _context.Value;

    #endregion
    
    // ---------------------------------------------------------------------------------
    
    public StatController(StatAsset asset)
    {
        _context = new StatContext(this, asset);
    }
}