using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FrogLibrary
{
    public static class EventBus
    {
        private static readonly IDictionary<string, IDictionary<int, UnityEvent>> _events =
            new Dictionary<string, IDictionary<int, UnityEvent>>();

        #region RegisterEvent

        // # 가장 기본적인 등록
        public static void RegisterEvent(string eventName, Dictionary<int, UnityEvent> events)
        {
            if (_events.TryAdd(eventName, events))
            {
            }
        }

        // # 이름은 string, Event 에 대한 것만 정의
        public static void RegisterEvent<TValue>(string eventName) where TValue : Enum
        {
            if (_events.ContainsKey(eventName))
            {
                Debug.Assert(false, "this is already registered");
                return;
            }

            // # 단순히 길이로만 추가하는 이유는 어처피 내부적으론 해당 Enum 인덱스만 알면 되므로

            int length = Enum.GetValues(typeof(TValue)).Length;

            Dictionary<int, UnityEvent> newEventDict = new Dictionary<int, UnityEvent>();
            for (int i = 0; i < length; i++)
            {
                newEventDict.Add(i, new UnityEvent());
            }

            RegisterEvent(eventName, newEventDict);
        }

        public static void RegisterEvent<TValue>() where TValue : Enum
        {
            string eventName = typeof(TValue).Name;

            RegisterEvent<TValue>(eventName);
        }

        #endregion

        #region UnregisterEvent

        public static bool UnregisterEvent(string eventName)
        {
            return _events.Remove(eventName);
        }

        public static bool UnregisterEvent<TKey>() where TKey : Enum
        {
            string eventName = typeof(TKey).Name;

            return UnregisterEvent(eventName);
        }

        #endregion

        #region Subscribe

        public static void Subscribe(string eventName, int i, UnityAction callback)
        {
            if (_events.TryGetValue(eventName, out IDictionary<int, UnityEvent> events))
            {
                events[i].AddListener(callback);
            }
        }

        public static void Subscribe<TValue>(string eventName, TValue e, UnityAction callback)
        {
            int i = 0;

            foreach (TValue value in Enum.GetValues(typeof(TValue)))
            {
                if (value.Equals(e))
                {
                    Subscribe(eventName, i, callback);
                    return;
                }

                i++;
            }
        }

        public static void Subscribe<TValue>(TValue type, UnityAction callback)
        {
            string eventName = typeof(TValue).Name;

            Subscribe(eventName, type, callback);
        }

        #endregion

        #region Unsubscribe

        public static void Unsubscribe(string eventName, int i, UnityAction callback)
        {
            if (_events.TryGetValue(eventName, out IDictionary<int, UnityEvent> events))
            {
                events[i].RemoveListener(callback);
            }
        }

        public static void Unsubscribe<TValue>(string eventName, TValue e, UnityAction callback)
        {
            int i = 0;

            foreach (TValue value in Enum.GetValues(typeof(TValue)))
            {
                if (value.Equals(e))
                {
                    Unsubscribe(eventName, i, callback);
                    return;
                }

                i++;
            }
        }

        public static void Unsubscribe<TValue>(TValue type, UnityAction callback)
        {
            string eventName = typeof(TValue).Name;

            Unsubscribe(eventName, type, callback);
        }

        #endregion

        #region Publish

        public static void Publish(string eventName, int i)
        {
            if (_events.TryGetValue(eventName, out IDictionary<int, UnityEvent> events))
            {
                events[i]?.Invoke();
            }
        }

        public static void Publish<TValue>(string eventName, TValue e)
        {
            int i = 0;

            foreach (TValue value in Enum.GetValues(typeof(TValue)))
            {
                if (value.Equals(e))
                {
                    Publish(eventName, i);
                    return;
                }

                i++;
            }
        }

        public static void Publish<TValue>(TValue e)
        {
            string eventName = typeof(TValue).Name;

            Publish(eventName, e);
        }

        #endregion
    }
}