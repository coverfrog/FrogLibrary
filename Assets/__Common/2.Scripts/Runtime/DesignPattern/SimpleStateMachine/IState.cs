using UnityEngine;

public interface IState<in TController>
{
    void OnEnter(TController controller);
    
    void OnExit(TController controller);
    
    void OnUpdate(TController controller);
}
