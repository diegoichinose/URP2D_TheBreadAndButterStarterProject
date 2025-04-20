using UnityEngine;

public class PauseResumeTimeManager : MonoBehaviour
{
	[Header("DO NOT TOUCH - FOR INSPECTION ONLY")]
    [SerializeField] private int timePausingLayers = 0;
    public float timeSnapshotWhenPaused { get; private set; }
    public bool isPaused;
    public static PauseResumeTimeManager instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
    void OnDestroy()
    {
        StopAllCoroutines();
    }

    public void PhysicallyPauseTime() 
    {
        timePausingLayers++;
        Time.timeScale = 0;
        isPaused = true;
    }

    public void PhysicallyResumeTime() 
    {
        if (timePausingLayers <= 1)
        {
            GameEventsManager.instance.OnTimeResumed?.Invoke();
            isPaused = false;
        }

        timePausingLayers--;
        Time.timeScale = 1;
    }

    public void PauseTime() 
    {
        timePausingLayers++;
        isPaused = true;
        timeSnapshotWhenPaused = Time.time;
        GameEventsManager.instance.OnTimePaused?.Invoke();
    }

    public void TryResumeTime() 
    {
        if (timePausingLayers <= 1)
        {
            GameEventsManager.instance.OnTimeResumed?.Invoke();
            isPaused = false;
        }
        
        timePausingLayers--;
        if (timePausingLayers < 0)
            timePausingLayers = 0;
    }
    
    public void ForceResumeTime() 
    {
        GameEventsManager.instance.OnTimeResumed?.Invoke();
        Time.timeScale = 1f;
        timePausingLayers = 0;
        isPaused = false;
    }
}