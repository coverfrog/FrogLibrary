using UnityEngine;

public interface ISimpleState<in TController>
{
    void OnEnter(TController controller);
    
    void OnExit(TController controller);
    
    void OnUpdate(TController controller);
}
