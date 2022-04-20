using System;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Managers.AssetsPreload
{
    public class AssetsPreloadManager: MonoBehaviour
    {
        private const string PreloadAssetsKey = "Preload";
        
        private void Awake()
        {
            Addressables.LoadAssetAsync<GameObject>(PreloadAssetsKey);
        }
    }
}