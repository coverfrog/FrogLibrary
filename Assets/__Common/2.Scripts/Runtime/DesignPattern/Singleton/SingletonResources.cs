using System;
using UnityEngine;

public abstract class SingletonResources<T1> : MonoBehaviour where T1 : Component
{
    protected abstract string ResourcePath { get; }

    private static object _lock = new();
    
    private static bool _applicationQuitting = false;
    
    public static T1 Instance
    {
        get
        {
            if (_applicationQuitting)
                return null;

            lock (_lock)
            {
                if (_instance)
                    return _instance;

                _instance = FindAnyObjectByType<T1>();
            
                if (_instance)
                    return _instance;

                string resourcesPath = (Activator.CreateInstance<T1>() as SingletonResources<T1>)?.ResourcePath;
                string objName = NicifyUtil.ToNicifyVariableName(typeof(T1).Name);
            
                if (string.IsNullOrEmpty(resourcesPath))
                {
                    _instance = new GameObject(objName).AddComponent<T1>();
                }

                else
                {
                    T1 resource = Resources.Load<T1>(resourcesPath);

                    if (resource)
                    {
                        _instance = Instantiate(resource);
                        _instance.name = objName;
                    }

                    else
                    {
                        _instance = new GameObject(objName).AddComponent<T1>();
                    }
                }
            }
            
            return _instance;
        }
    }
    
    private static T1 _instance;
    
    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T1;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        _applicationQuitting = true;
    }
}