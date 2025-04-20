using DG.Tweening;
using UnityEngine;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

	void OnDestroy()
	{
        DOTween.KillAll();
	}
    
    public void OnSaveSlotClicked(bool isSaveSlotEmpty)
    {
        LoadingScreenManager.instance.Show();
        
        GameScenesManager.instance.UnloadTitleScreenScene();
        GameScenesManager.instance.LoadGameplayScene();

        LoadingScreenManager.instance.Hide();
    }

    public void OnSaveAndQuitClick()
    {   
        LoadingScreenManager.instance.Show("<color=yellow>Saving now...</color><br>Please do not exit the game<br>It'll be done in a jiffy");
        EventSystemManager.instance.RunNextFrame(() => 
        {
            TransitionToTitleScreen();
            SaverLoaderManager.instance.SaveGame(); 
            EventSystemManager.instance.InvokeAfterTime(() => LoadingScreenManager.instance.Hide(), time: 0.1f);
        });
    }

    public void TransitionToTitleScreen()
    {   
        GameScenesManager.instance.LoadTitleScreenScene();
        GameScenesManager.instance.UnloadGameplayScene();
        CursorManager.instance.ResetCursor();
    }
}