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
                GameObject[] instanceList = await UniTask.WhenAll(Enumerable.Select(bootOption.InstanceAddressList, adr => AddressableUtil.InstantiateAsync<GameObject>(adr)));
                
                IsBoot = true;
                
                // 3. 데이터 로더 탐색 및 Instance 화
                bool isDontDestroyOnLoad = bootOption.IsDontDestroyOnLoad;
                IDataLoader dataLoader = null;
                
                foreach (GameObject go in instanceList)
                {
                    if (go.TryGetComponent(out IDataLoader tempDataLoader))
                    {
                        dataLoader = tempDataLoader;
                    }

                    if (isDontDestroyOnLoad)
                    {
                        Object.DontDestroyOnLoad(go);
                    }
                }

                // 4. 데이터 로딩
                if (dataLoader == null)
                {
                    return;
                }
                
                await dataLoader.LoadData(null, null);

                IsLoadedData = true;
            }
            
            catch (Exception e)
            {
                Debug.Assert(false, e.Message);
            }
        }
    }
}
