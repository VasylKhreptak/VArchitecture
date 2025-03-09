using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using Infrastructure.Services.Asset.Core;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace Infrastructure.Services.Asset
{
    public class AssetService : IAssetService, IDisposable
    {
        private readonly CompositeDisposable _releaseSubscriptions = new CompositeDisposable();

        public UniTask<T> LoadAsync<T>(AssetReference assetReference) => Addressables.LoadAssetAsync<T>(assetReference).ToUniTask();

        public void Release<T>(T asset) => Addressables.Release(asset);

        public async UniTask<GameObject> InstantiateAsync(AssetReferenceGameObject assetReference)
        {
            GameObject prefab = await LoadAsync<GameObject>(assetReference);

            AsyncInstantiateOperation<GameObject> instantiateOperation = Object.InstantiateAsync(prefab);

            await instantiateOperation.ToUniTask();

            GameObject instance = instantiateOperation.Result.First();

            instance.OnDestroyAsObservable().Subscribe(_ => Release(prefab)).AddTo(_releaseSubscriptions);

            return instance;
        }

        public void Dispose() => _releaseSubscriptions?.Dispose();
    }
}