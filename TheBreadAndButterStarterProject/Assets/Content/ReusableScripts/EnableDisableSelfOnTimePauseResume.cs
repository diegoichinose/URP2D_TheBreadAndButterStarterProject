using UnityEngine;

public class EnableDisableSelfOnTimePauseResume : MonoBehaviour
{   
    void Start()
    {
        GameEventsManager.instance.OnTimePaused += DisableSelf;
        GameEventsManager.instance.OnTimeResumed += EnableSelf;
    }

    void OnDestroy()
    {
        GameEventsManager.instance.OnTimePaused -= DisableSelf;
        GameEventsManager.instance.OnTimeResumed -= EnableSelf;
    }

    private void DisableSelf() => gameObject?.SetActive(false);
    private void EnableSelf() => gameObject?.SetActive(true);
}