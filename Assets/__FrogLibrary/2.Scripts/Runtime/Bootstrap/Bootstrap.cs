using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace FrogLibrary
{
    public static class Bootstrap 
    {
        public static bool IsBoot {get; private set;}
        
        public static bool IsLoadedData {get; private set;}
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        public static async void Boot()
        {
            try
            {
                // 1. 옵션 So 호출
                BootOption bootOption = Resources.Load<BootOption>(AssetMenuNames.k_bootOptionFileName);

                // 2. 인스턴스 호출
                var instanceList = await UniTask.WhenAll(Enumerable.Select(bootOption.InstanceAddressList, adr => AddressableUtil.InstantiateAsync<GameObject>(adr)));
                
                IsBoot = true;
                
                // 3. 데이터 로드
                foreach (GameObject go in instanceList)
                {
                    if (!go.TryGetComponent(out IDataLoader dataLoader))
                    {
                        continue;
                    }
                    
                    await dataLoader.LoadData(null, () => IsLoadedData = true);
                }
            }
            
            catch (Exception e)
            {
                Debug.Assert(false, e.Message);
            }
        }
    }
}
