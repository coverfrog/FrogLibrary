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
        }
        
        protected virtual void BeforeBoot() {}
        
        protected virtual void AfterBoot() {}
    }
}