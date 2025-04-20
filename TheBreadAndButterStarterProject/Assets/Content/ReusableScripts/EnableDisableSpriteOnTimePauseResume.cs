using UnityEngine;

// USED BY SOME PROJECTILES (the ones that don't follow mouse or player)
public class EnableDisableSpriteOnTimePauseResume : MonoBehaviour
{
    void Start()
    {
        GameEventsManager.instance.OnTimePaused += DisableSpriteRenderer;
        GameEventsManager.instance.OnTimeResumed += EnableSpriteRenderer;
    }

    void OnDestroy()
    {
        GameEventsManager.instance.OnTimePaused -= DisableSpriteRenderer;
        GameEventsManager.instance.OnTimeResumed -= EnableSpriteRenderer;
    }

    private void DisableSpriteRenderer() => GetComponent<SpriteRenderer>().enabled = false;
    private void EnableSpriteRenderer() => GetComponent<SpriteRenderer>().enabled = true;
}