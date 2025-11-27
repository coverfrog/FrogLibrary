using System.Collections.Generic;
using UnityEngine;

namespace FrogLibrary
{
    
    public static class FrameStatic
    {
        public static WaitForEndOfFrame EndOfFrame { get; } = new WaitForEndOfFrame();
    
        public static WaitForFixedUpdate FixedUpdate { get; } = new WaitForFixedUpdate();
    
        private static Dictionary<float, WaitForSeconds> _seconds = new Dictionary<float, WaitForSeconds>();

        public static WaitForSeconds Seconds(float seconds)
        {
            if (_seconds.TryGetValue(seconds, out WaitForSeconds result))
            {
                return result;
            }
        
            _seconds.Add(seconds, new WaitForSeconds(seconds));
        
            return _seconds[seconds];
        }
    }
}