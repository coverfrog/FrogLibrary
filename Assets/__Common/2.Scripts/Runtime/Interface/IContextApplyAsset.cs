using UnityEngine;

public interface IContextApplyAsset<in T> where T : ScriptableObject
{
    public void ApplyAsset(T asset);
}