using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class InitializationLoader : MonoBehaviour
{
	[SerializeField] private SceneData _coreSystemSceneData = default;
	[SerializeField] private SceneData _titleScreenSceneData = default;

	private void Start()
	{
		_coreSystemSceneData.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true).Completed += LoadTitleScreen;
	}

	private void LoadTitleScreen(AsyncOperationHandle<SceneInstance> obj)
	{
		_titleScreenSceneData.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true).Completed += UnloadInitializationScene;
	}

	private void UnloadInitializationScene(AsyncOperationHandle<SceneInstance> obj)
	{
		// Initialization is the only scene in BuildSettings, therefore it has index 0
		SceneManager.UnloadSceneAsync(0); 
	}
}