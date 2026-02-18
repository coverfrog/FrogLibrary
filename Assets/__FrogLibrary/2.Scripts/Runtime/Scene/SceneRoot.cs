using System;
using System.Collections;
using UnityEngine;

namespace FrogLibrary
{
    public abstract class SceneRoot : MonoBehaviour
    {
        protected virtual IEnumerator Start()
        {
            BeforeBoot();
            
            yield return new WaitUntil(() => Bootstrap.IsBoot);
            
            AfterBoot();
            
            yield return new WaitUntil(() => Bootstrap.IsLoadedData);
        }
        
        protected virtual void BeforeBoot() {}
        
        protected virtual void AfterBoot() {}
        
        protected virtual void AfterLoadedData() {}
    }
}