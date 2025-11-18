using System;
using System.Collections.Generic;

/// <summary>
/// c# class 에서 update 가 필요한 경우에 사용하기 위함
/// </summary>
public class UpdateManager : Singleton<UpdateManager>
{
    private readonly List<IUpdateable> _updateableList = new List<IUpdateable>();
    
    public void Add(IUpdateable able) => _updateableList.Add(able);
    
    public void Remove(IUpdateable able) => _updateableList.Remove(able);

    private void Update()
    {
        foreach (IUpdateable able in _updateableList)
        {
            able?.OnUpdate();
        }
    }
}