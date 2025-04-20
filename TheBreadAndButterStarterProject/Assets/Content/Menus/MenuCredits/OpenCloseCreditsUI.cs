using UnityEngine;
using UnityEngine.InputSystem;

public class OpenCloseCreditsUI : MonoBehaviour
{
    [SerializeField] private GameObject creditsUI;
    private GameInput _input;

    void OnDestroy()
    {
        if (_input != null)
            _input.Disable();
    }

    public void OpenCreditsUI()
    {
        creditsUI.SetActive(true);
        AudioManager.instance.coreSounds.PlayOpenMenuSound();

        _input = new GameInput();
        _input.Enable();
        _input.Menu.CloseMenu.performed += CloseFromInput;
    }

    private void CloseFromInput(InputAction.CallbackContext context) => CloseCreditsUI();
    public void CloseCreditsUI()
    {
        creditsUI.SetActive(false);
        AudioManager.instance.coreSounds.PlayCloseMenuSound();

        _input.Menu.CloseMenu.performed -= CloseFromInput;
        _input.Disable();
        _input = null;
    }
}