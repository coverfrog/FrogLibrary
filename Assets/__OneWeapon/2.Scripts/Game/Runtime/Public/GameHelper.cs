using System;
using System.Collections;
using UnityEngine;

public class GameHelper : MonoBehaviour
{
    // ---------------------------------------------------------------------------------
    
    private void Awake()
    {
        EventBus.RegisterEvent<GameEventBusType>();
    }

    private void OnEnable()
    {
        StartCoroutine(CoEnable());
    }

    private IEnumerator CoEnable()
    {
        yield return FrameStatic.EndOfFrame;
        
        EventBus.Publish(GameEventBusType.Init);

        yield return FrameStatic.Seconds(2.0f);

        EventBus.Publish(GameEventBusType.Play);
    }
}