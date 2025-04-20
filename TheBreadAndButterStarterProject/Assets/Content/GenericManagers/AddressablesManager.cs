using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class AddressablesManager : MonoBehaviour
{
    public static AddressablesManager instance = null;
    private Dictionary<AssetReference, Coroutine> loadCoroutines;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        loadCoroutines = new Dictionary<AssetReference, Coroutine>();
        StartCoroutine(UnloadUnusedAssetsPeriodically());
    }

    void OnDestroy()
    {
        StopAllCoroutines();
    }

    private void TryStopAnyDuplicateCoroutine(AssetReference key)
    {
        if (loadCoroutines.TryGetValue(key, out Coroutine existingCoroutine))
        {
            StopCoroutine(existingCoroutine);
            loadCoroutines.Remove(key);
        }
    }
    
    public void Unload(AssetReference assetReference)
    {
        if (assetReference.IsValid())
            Addressables.Release(assetReference);
    }

    private IEnumerator UnloadUnusedAssetsPeriodically()
    {
        yield return new WaitForSecondsRealtime(10f);
        Resources.UnloadUnusedAssets();
    }

    // --------------------------------------------------------------------------------------------------------- LOAD SPRITE
    public void Load(AssetReferenceSprite assetReference, Action<Sprite> OnSuccess)
    {
        TryStopAnyDuplicateCoroutine(key: assetReference);
        var assetLoadCoroutine = StartCoroutine(LoadToImage(assetReference, OnSuccess));
        loadCoroutines.Add(key: assetReference, assetLoadCoroutine);
    }

    private IEnumerator LoadToImage(AssetReferenceSprite assetReference, Action<Sprite> OnSuccess)
    {
        var loadOperation = Addressables.LoadAssetAsync<Sprite>(assetReference);
        yield return loadOperation;

        if (loadOperation.Status == AsyncOperationStatus.Succeeded)
            OnSuccess?.Invoke(loadOperation.Result);
        else
            Debug.LogError("AssetReference failed to load.");
    }

    // --------------------------------------------------------------------------------------------------------- LOAD GAMEOBJECT PREFAB
    public void Load(AssetReferenceGameObject assetReference, Action<GameObject> OnSuccess, bool preventDuplicates = false)
    {
        if (preventDuplicates)
            TryStopAnyDuplicateCoroutine(key: assetReference);

        var assetLoadCoroutine = StartCoroutine(LoadGameObject(assetReference, OnSuccess));
        
        if (preventDuplicates)
            loadCoroutines.Add(key: assetReference, assetLoadCoroutine);
    }

    private IEnumerator LoadGameObject(AssetReference assetReference, Action<GameObject> OnSuccess)
    {
        var loadOperation = Addressables.LoadAssetAsync<GameObject>(assetReference);
        yield return loadOperation;
        
        if (loadOperation.Status == AsyncOperationStatus.Succeeded)
            OnSuccess?.Invoke(loadOperation.Result);
        else
            Debug.LogError("AssetReference failed to load.");
    }
}

