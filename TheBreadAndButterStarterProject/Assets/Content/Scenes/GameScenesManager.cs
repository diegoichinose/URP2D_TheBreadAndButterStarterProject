using UnityEngine;
using UnityEngine.AddressableAssets;

// THE "SceneManager" NAME IS ALREADY TAKEN 
public class GameScenesManager : MonoBehaviour
{    
	public static GameScenesManager instance = null;
	
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
			
		Addressables.InitializeAsync();
    }
	
	public void LoadScene(SceneData sceneData)
	{
		sceneData.TryLoad();
	}
	
	public void UnloadScene(SceneData sceneData)
	{
		sceneData.TryUnload();
	}

	[SerializeField] private SceneData _titleScreenSceneData = default;
	public void LoadTitleScreenScene() => LoadScene(_titleScreenSceneData);
	public void UnloadTitleScreenScene() => UnloadScene(_titleScreenSceneData);

	[SerializeField] private SceneData _gameplaySceneData = default;
	public void LoadGameplayScene() => LoadScene(_gameplaySceneData);
	public void UnloadGameplayScene() => UnloadScene(_gameplaySceneData);
}