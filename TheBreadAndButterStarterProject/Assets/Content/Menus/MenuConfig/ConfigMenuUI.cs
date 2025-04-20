using UnityEngine;
using UnityEngine.InputSystem;

public class ConfigMenuUI : MonoBehaviour
{
    private GameInput _playerInput;
    public bool canClose;

    void OnEnable()
    {
        _playerInput = new GameInput();
        _playerInput.Enable();
        
        canClose = true;
        _playerInput.Menu.Cancel.performed += TryCloseConfigMenu;
        _playerInput.Menu.CloseMenu.performed += TryCloseConfigMenu;
    }

    void OnDisable()
    {
        _playerInput.Menu.Cancel.performed -= TryCloseConfigMenu;
        _playerInput.Menu.CloseMenu.performed -= TryCloseConfigMenu;
        _playerInput.Disable();
    }
    
    public void TryCloseConfigMenu(InputAction.CallbackContext context) => TryCloseConfigMenu();
    public void TryCloseConfigMenu()
    {
        if (canClose == false)
            return;

        if (EventSystemManager.instance.isDropdownButtonOpen)
        {
            EventSystemManager.instance.closeDropdownButtonAction.Invoke();
            return;
        }

        gameObject.SetActive(false);
        AudioManager.instance.coreSounds.PlayCloseMenuSound();
    }
}