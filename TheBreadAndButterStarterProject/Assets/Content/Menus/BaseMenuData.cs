using System;
using UnityEngine;

public class BaseMenuData : ScriptableObject
{
    [SerializeField] protected GameObject menuPrefab;
    public virtual GameObject GetMenuPrefab() => menuPrefab;
    
    public bool isEnabledByDefault;
    public bool onOpenPauseTime;
    public bool onCloseDestroyInstance;
    public bool canCloseWithEsc;
    public bool canCloseWithCancelClick;
    
    [Header("DO NOT TOUCH - FOR INSPECTION ONLY")]
    public bool isOpen;
    public bool canOpen;
    public Action OnMenuOpen;
    public Action OnMenuClose;

    public virtual void OnInstantiate(){}
    public virtual void OnDestroy(){}

    public virtual void OnOpen()
    {
        OnMenuOpen?.Invoke();
    }

    public virtual void OnClose()
    {
        OnMenuClose?.Invoke();
    }

    void OnEnable()
    {
        canOpen = true;
        isOpen = false;
    }
    
    void OnDisable()
    {
        canOpen = true;
        isOpen = false;
    }
}