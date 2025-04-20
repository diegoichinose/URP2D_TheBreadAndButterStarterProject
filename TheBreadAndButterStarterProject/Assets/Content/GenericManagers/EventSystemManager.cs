using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EventSystemManager : MonoBehaviour
{
    public static EventSystemManager instance = null;
    [SerializeField] private GameObject eventSystemDefault;
    private GameObject previouslySelectedEventSystem;
    private GameObject currentlySelectedEventSystem;
    public bool isDropdownButtonOpen;
    public Action closeDropdownButtonAction;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
    
    public void SwitchToDefaultEventSystem() => SetEventSystem(eventSystemDefault);
    public void SwitchToPreviousEventSystem() => SetEventSystem(previouslySelectedEventSystem);

    private void SetEventSystem(GameObject eventSystem)
    {
        if (eventSystemDefault == null)
            return;

        DisableAllEventSystems();

        if (eventSystem != null)
            eventSystem.SetActive(true);
        else
            eventSystemDefault.SetActive(true);

        previouslySelectedEventSystem = currentlySelectedEventSystem;
        currentlySelectedEventSystem = eventSystem;
    }

    private void DisableAllEventSystems()
    {
        eventSystemDefault.SetActive(false);
    }
    
    public void InvokeAfterTime(Action action, float time) => StartCoroutine(WaitAndInvoke(action, time));
    private IEnumerator WaitAndInvoke(Action action, float time)
    {
        yield return new WaitForSecondsRealtime(time);
        action();
    }

    public void RunNextFrame(Action action) => StartCoroutine(InvokeNextFrame(action));
    public IEnumerator InvokeNextFrame(Action action)
    {
        yield return new WaitForNextFrameUnit();
        action.Invoke();
    }
}