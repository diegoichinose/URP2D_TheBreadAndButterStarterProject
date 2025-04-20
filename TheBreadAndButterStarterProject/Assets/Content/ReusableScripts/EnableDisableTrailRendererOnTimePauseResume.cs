using UnityEngine;

// USED BY SOME PROJECTILES (the ones that don't follow mouse or player)
public class EnableDisableTrailRendererOnTimePauseResume : MonoBehaviour
{
    void Start()
    {
        GameEventsManager.instance.OnTimePaused += DisableTrailRenderer;
        GameEventsManager.instance.OnTimeResumed += EnableTrailRenderer;
    }

    void OnDestroy()
    {
        GameEventsManager.instance.OnTimePaused -= DisableTrailRenderer;
        GameEventsManager.instance.OnTimeResumed -= EnableTrailRenderer;
    }

    private void DisableTrailRenderer() => GetComponent<TrailRenderer>().enabled = false;
    private void EnableTrailRenderer() => GetComponent<TrailRenderer>().enabled = true;
}