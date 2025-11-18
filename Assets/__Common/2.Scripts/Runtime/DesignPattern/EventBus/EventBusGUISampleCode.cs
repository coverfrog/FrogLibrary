using System;
using System.Collections.Generic;
using UnityEngine.Events;

[Serializable]
public class EventBusGUISampleCode 
{
    public enum GameEventType
    {
        GameStart,
        GamePlaying,
        GameEnd,
    }
    public enum BattleEventType
    {
        Attack,
        Defense,
    }

    #region 이벤트 추가

    private void EventRegister()
    {
        // # 등록 하는 방법 1 ☆☆☆☆☆
        
        // 되도록 쓰지 말 것
        // 매번 Enum 수정시 마다 본인이 직접 수정 해야함 귀찮다...
        
        // 더불어 커스텀 인덱스를 넣어놓는 경우가 있기 때문에
        // 다시 이벤트를 꺼내게 되면 그 순서에 대한 보장이 확실하지 못하단 단점이 있다.

        EventBus.RegisterEvent("Game", new Dictionary<int, UnityEvent>()
        {
            { (int)GameEventType.GameStart  , new UnityEvent() {}},
            { (int)GameEventType.GamePlaying, new UnityEvent() {}},
            { (int)GameEventType.GameEnd    , new UnityEvent() {}}
        });
        
        // -----------------------------------------------------------------------------------
        
        // # 등록 하는 방법 2 ☆☆☆☆☆
        
        // 1번 방법에서는 그래도 개선됨
        // 그렇치만 매번 일일히 이런식으로 캐스팅 하려면 번거로움
        
        // 아래 방법들에선 이 것을 자동적용 시켜놓음

        int length = Enum.GetValues(typeof(GameEventType)).Length;
        
        Dictionary<int, UnityEvent> newEventDict = new Dictionary<int, UnityEvent>();
        for (int i = 0; i < length; i++)
        {
            newEventDict.Add(i, new UnityEvent());
        }
        
        EventBus.RegisterEvent("Game", newEventDict);
        
        // -----------------------------------------------------------------------------------
        
        // # 등록 하는 방법 3 ★☆☆☆☆
        
        // 휴먼 에러가 발생할 가능성이 높음
        // 또한 앞에 이런식으로 강제를 해야되는 부분이 존재 
        
        EventBus.RegisterEvent<GameEventType>("Game");
        
        // -----------------------------------------------------------------------------------
        
        // # 등록 하는 방법 4 ★★★★☆
        
        // Enum 이름 자체를 키로 삼는 방법
        // Enum 안에 이벤트 공간 자체도 자동 생성 됨
        
        EventBus.RegisterEvent<GameEventType>();
    }

    #endregion

    #region 이벤트 제거

    private void EventUnRegister()
    {
        // # 방법 1 ★☆☆☆☆
        
        EventBus.UnregisterEvent("Game");
        
        // # 방법 2 ★★★★☆

        EventBus.UnregisterEvent<GameEventType>();
    }

    #endregion

    #region 이벤트 구독 && 해제

    private void EventSubscribes()
    {
        // # 방법 1 ☆☆☆☆☆
        
        // 아예 쓰지 말기
        // 일일히 Index 를 매핑 해봐야 한다는 단점
        
        EventBus.Subscribe("Game", 0, OnGameStart);
        EventBus.Unsubscribe("Game", 0, OnGameStart);
        
        // # 방법 2 ★☆☆☆☆
        
        // Int 를 강제 고정해서 쓰긴 하지만 아직 불편
        // 휴먼 에러 가능성 높음
        
        EventBus.Subscribe("Game", (int)GameEventType.GameStart, OnGameStart);
        EventBus.Unsubscribe("Game", (int)GameEventType.GameStart, OnGameStart);
        
        // # 방법 3 ★★★★☆
        
        // 편하게 해당 이벤트만 등록을 바로 가능 하다는 장점이 존재
        
        EventBus.Subscribe(GameEventType.GameStart ,OnGameStart);
        EventBus.Unsubscribe(GameEventType.GameStart ,OnGameStart);
    }

    #endregion

    #region Publish

    public void Publish()
    {
        // # 방법 1 ☆☆☆☆☆
        
        EventBus.Publish("Game", 0);
        
        // # 방법 2 ☆☆☆☆☆
        
        EventBus.Publish<GameEventType>("Game", (int)GameEventType.GameStart);
        
        // # 방법 3 ★★★★☆
        
        EventBus.Publish(GameEventType.GameStart);
        
    }

    #endregion

    #region Test

    private void OnGameStart()
    {
        
    }
    
    #endregion
    
}