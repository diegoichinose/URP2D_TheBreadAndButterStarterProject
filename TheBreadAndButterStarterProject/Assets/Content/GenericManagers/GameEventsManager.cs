using System;
using DG.Tweening;
using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager instance = null;
    
    public Action OnPauseMenuOpen;
    public Action OnPauseMenuClose;
    public Action OnTimePaused;
    public Action OnTimeResumed;

    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);
    }
}