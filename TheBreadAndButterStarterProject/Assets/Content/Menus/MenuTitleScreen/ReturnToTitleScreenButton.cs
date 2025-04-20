using UnityEngine;

public class ReturnToTitleScreenButton : MonoBehaviour
{
    public void OnButtonClick()
    {
        SceneTransitionManager.instance.TransitionToTitleScreen();
    }
}
