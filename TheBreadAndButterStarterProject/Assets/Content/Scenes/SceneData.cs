using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "My Scriptable Objects/Scene Data/Default")]
public class SceneData : SerializableScriptableObject
{
	public AssetReference sceneReference;
	public string sceneName; // INFO: WORKAROUND BECAUSE UNITY WON'T ALLOW TO GET THE AssetReference NAME
	[HideInInspector] public SceneInstance sceneInstance = new SceneInstance();
	[HideInInspector] public AsyncOperationHandle<SceneInstance> sceneInstanceAsyncOperation;
	[HideInInspector] public bool isSceneLoaded => sceneInstance.Scene != null && sceneInstance.Scene.isLoaded;

	public void TryLoad()
	{
		if (sceneInstance.Scene != null)
		if (sceneInstance.Scene.isLoaded)
			return;
		
		// INFO: FAILSAFE TO OLD SceneManager TO MAKE SURE IT NEVER TRIES TO LOAD EXISTING SCENE
		if (SceneManager.GetSceneByName(sceneName).isLoaded)
			return;

		Addressables.InitializeAsync();
		sceneReference.LoadSceneAsync(LoadSceneMode.Additive).Completed += OnLoadComplete;
	}

    private void OnLoadComplete(AsyncOperationHandle<SceneInstance> handle) 
	{
        if (handle.Status == AsyncOperationStatus.Succeeded)
            sceneInstance = handle.Result;
        else
            Debug.LogError("Failed to load scene async.");
	}

	// INFO: WORKAROUD TO LOAD AFTER UNLOADING SCENE (eg. try load map only after all maps are unloaded) BECAUSE UNITY DOESNT ALLOW SYNCHRONOUS UNLOADING
	public void TryLoadAfterTime(AsyncOperationHandle<SceneInstance> sceneInstanceAsyncOperation) => ReferencesManager.instance.StartThisCoroutine(TryLoadAfterTime());
	private IEnumerator TryLoadAfterTime()
	{
		yield return new WaitForSecondsRealtime(0.1f);
		TryLoad();
	}

	public void TryLoadAsync(Action<AsyncOperationHandle<SceneInstance>> callback = null)
	{
		if (sceneInstance.Scene != null)
		if (sceneInstance.Scene.isLoaded)
			return;

		sceneInstanceAsyncOperation = sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
		sceneInstanceAsyncOperation.Completed += OnLoadAsyncComplete;

		if (callback != null)
			sceneInstanceAsyncOperation.Completed += callback;
	}

	private void OnLoadAsyncComplete(AsyncOperationHandle<SceneInstance> sceneInstanceAsyncOperation)
	{
		sceneInstance = sceneInstanceAsyncOperation.Result;
	}

	public void TryUnload(Action<AsyncOperationHandle<SceneInstance>> callback = null)
	{
		if (sceneInstance.Scene != null)
		if (sceneInstance.Scene.isLoaded)
		if (sceneReference.OperationHandle.IsValid())
		{
			var unloadAsyncOperation = sceneReference.UnLoadScene();
			unloadAsyncOperation.Completed += OnUnloadAsyncComplete;

			if (callback != null)
				sceneInstanceAsyncOperation.Completed += callback;

			return;
		}

		// FAIL-SAFE TO THE OLD SceneManager IF ADDRESSABLE INSTANCE FAILS
		if (SceneManager.GetSceneByName(sceneName).isLoaded)
			SceneManager.UnloadSceneAsync(sceneName).Yield();
	}

	private void OnUnloadAsyncComplete(AsyncOperationHandle<SceneInstance> handle)
	{
        if (handle.Status == AsyncOperationStatus.Succeeded)
            sceneInstance = handle.Result;
        else
            Debug.LogError("Failed to unload scene async.");
	}

	public void ReloadScene()
	{
		TryUnload();
		TryLoad();
	}
}