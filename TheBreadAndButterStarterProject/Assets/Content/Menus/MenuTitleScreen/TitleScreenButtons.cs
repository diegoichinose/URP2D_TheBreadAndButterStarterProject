using UnityEngine;
using UnityEngine.InputSystem;

public class TitleScreenButtons : MonoBehaviour
{
    [SerializeField] private GameObject saveSlotMenu;
    [SerializeField] private GameObject configMenu;
    [SerializeField] private GameObject reportBugMenu;
    [SerializeField] private GameObject beforeYouQuitPopup;
    private GameInput _playerInput;

    void Start()
    {
        _playerInput = new GameInput();
        _playerInput.Enable();
    }

    void OnDestroy()
    {
        UnsubscribeCloseSaveSlotMenuFromInput();
        _playerInput.Disable();
    }

    public void PlayButton()
    {
        saveSlotMenu.SetActive(true);
        AudioManager.instance.coreSounds.PlayOpenMenuSound();

        SubscribeCloseSaveSlotMenuFromInput();
    }

    public void CloseSaveSlotMenu(InputAction.CallbackContext context) => CloseSaveSlotMenuButton();
    public void CloseSaveSlotMenuButton()
    {
        AudioManager.instance.coreSounds.PlayCloseMenuSound();
        saveSlotMenu.SetActive(false);
        UnsubscribeCloseSaveSlotMenuFromInput();
    }

    public void ConfigButton()
    {
        AudioManager.instance.coreSounds.PlayOpenMenuSound();
        configMenu.SetActive(true);
    }

    public void ReportBugButton()
    {
        Application.OpenURL(Constants.REPORT_BUG_URL);
    }

    public void ReportBugReturnButton()
    {
        AudioManager.instance.coreSounds.PlayCloseMenuSound();
        reportBugMenu.SetActive(false);
    }

    public void GiveFeedbackButton()
    {
        Application.OpenURL(Constants.GIVE_FEEDBACK_URL);
    }

    public void WishlistButton()
    {
        Application.OpenURL(Constants.STEAM_PAGE_URL);
    }

    public void DiscordButton()
    {
        Application.OpenURL(Constants.DISCORD_URL);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void OpenBeforeYouQuitPopUp()
    {
        AudioManager.instance.coreSounds.PlayOpenMenuSound();
        beforeYouQuitPopup.SetActive(true);
        _playerInput.Menu.CloseMenu.performed += CloseBeforeYouQuitPopUp;
    }

    public void CloseBeforeYouQuitPopUp(InputAction.CallbackContext context) => CloseBeforeYouQuitPopUp();
    public void CloseBeforeYouQuitPopUp()
    {
        beforeYouQuitPopup.SetActive(false);
        _playerInput.Menu.CloseMenu.performed -= CloseBeforeYouQuitPopUp;
    }

    public void SubscribeCloseSaveSlotMenuFromInput()
    {
        _playerInput.Menu.CloseMenu.performed += CloseSaveSlotMenu;
    }

    public void UnsubscribeCloseSaveSlotMenuFromInput()
    {
        _playerInput.Menu.CloseMenu.performed -= CloseSaveSlotMenu;
    }
}