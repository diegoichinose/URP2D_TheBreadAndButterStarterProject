using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UserInterfaceManager : MonoBehaviour
{
    public static UserInterfaceManager instance = null;
    private GameInput _playerInput;

    public Menu pauseMenu;
    public Menu reportBugMenu;
    
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        _playerInput = new GameInput();
        _playerInput.Enable();
    }

    void OnDestroy()
    {
        _playerInput.Disable();
        _playerInput = null;
        StopAllCoroutines();
    }
    
    private void TryInstantiateMenu(Menu menu)
    {
        if (menu.menuInstance != null) 
            return;

        menu.menuInstance = Instantiate(menu.menuData.GetMenuPrefab());
        menu.menuInstance.SetActive(menu.menuData.isEnabledByDefault);

        menu.menuData.canOpen = true;
        menu.menuData.isOpen = false;

        menu.menuData.OnInstantiate();

        if (menu.menuInstance.TryGetComponent(out Canvas canvas))
        if (canvas.worldCamera == null)
            canvas.worldCamera = Camera.main;
    }

    private void TryDestroyMenu(Menu menu)
    {
        if (menu.menuInstance == null)
            return;
        
        Destroy(menu.menuInstance);
        menu.menuInstance = null;
        menu.menuData.OnDestroy();
    }

    public void TryInstantiateTheseMenu(List<Menu> menuList)
    {
        menuList.ForEach(menu => 
        {
            TryInstantiateMenu(menu);
            menu.menuInstance.SetActive(menu.menuData.isEnabledByDefault);
        });
    }

    public void TryDestroyTheseMenu(List<Menu> menuList) => menuList.ForEach(menu => TryDestroyMenu(menu));
    public void TryCloseTheseMenu(List<Menu> menuList) => menuList.ForEach(menu => menu.Close());

    public void OpenMenu(Menu menu, Action<InputAction.CallbackContext> subscribeThisToCloseInput)
    {
        var menuData = menu.menuData;

        if (menuData.canOpen == false)
            return;

        if (menuData.isOpen)
            return;

        if (menuData.onOpenPauseTime)
            PauseResumeTimeManager.instance.PauseTime();
        
        TryInstantiateMenu(menu);

        _playerInput.Menu.CloseMenu.performed -= subscribeThisToCloseInput;
        _playerInput.Menu.Cancel.performed -= subscribeThisToCloseInput;

        if (menuData.canCloseWithEsc)
            _playerInput.Menu.CloseMenu.performed += subscribeThisToCloseInput;

        if (menuData.canCloseWithCancelClick)
            _playerInput.Menu.Cancel.performed += subscribeThisToCloseInput;
            
        menu.menuInstance.SetActive(true);
        menuData.isOpen = true;
        menuData.OnOpen();
    }

    public void CloseMenu(Menu menu, Action<InputAction.CallbackContext> unsubscribeThisFromCloseInput, bool destroyInstance = false)
    {
        var menuData = menu.menuData;

        if (menuData == null)
            return;
        
        if (menuData.isOpen == false)
            return;
            
        if (menu.menuInstance == null)
        {
            menuData.isOpen = false;
            return;
        }

        if (menuData.onOpenPauseTime)
            PauseResumeTimeManager.instance.TryResumeTime();
            
        _playerInput.Menu.CloseMenu.performed -= unsubscribeThisFromCloseInput;
        _playerInput.Menu.Cancel.performed -= unsubscribeThisFromCloseInput;

        menu.menuInstance.SetActive(false);
        menuData.isOpen = false;
        menuData.OnClose();

        if (destroyInstance || menuData.onCloseDestroyInstance)
            TryDestroyMenu(menu);
    }
}

[Serializable]
public class Menu
{
    public BaseMenuData menuData;
    public GameObject menuInstance;

    public void Open() => UserInterfaceManager.instance.OpenMenu(this, subscribeThisToCloseInput: Close);
    public void Close() =>  Close(false);
    public void Close(bool destroyInstance) => UserInterfaceManager.instance.CloseMenu(menu: this, unsubscribeThisFromCloseInput: Close, destroyInstance);

    // INFO: USE THIS TO LET PLAY FANCY ANIMATIONS BEFORE CLOSING
    public void CloseAfterTime(float time, bool destroyInstance = false) => EventSystemManager.instance.InvokeAfterTime(() => Close(destroyInstance), time);

    // FROM INPUT
    public void Open(InputAction.CallbackContext context) => Open();
    public void Close(InputAction.CallbackContext context) => Close();
    public void OpenClose(InputAction.CallbackContext context)
    {
        if (menuData.isOpen)
            Close();
        else
            Open();
    }
}