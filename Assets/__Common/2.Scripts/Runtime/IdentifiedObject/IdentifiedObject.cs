using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class IdentifiedObject : ScriptableObject, ICloneable
{
    [Header("# Info")]
    [SerializeField] private int _id;

    public int Id
    {
        get => _id;
    }
    
    [SerializeField] private string _codeName;

    public string CodeName
    {
        get => _codeName;
    }
    
    [SerializeField] private string _displayName;

    public string DisplayName
    {
        get => _displayName;
    }

    [SerializeField, TextArea] private string _description;

    public string Description
    {
        get => _description;
    }

    [SerializeField] private Sprite _icon;

    public Sprite Icon
    {
        get => _icon;
    }

    public virtual object Clone()
    {
        return Instantiate(this);
    }
}