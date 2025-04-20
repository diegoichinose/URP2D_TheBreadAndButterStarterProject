using DG.Tweening;
using UnityEngine;

public class PauseMenuButtons : MonoBehaviour
{
    [SerializeField] private ConfigMenuUI _configMenuUI;
    [SerializeField] private GameObject configMenu;
    
    public void ResumeButton()
    {
        UserInterfaceManager.instance.pauseMenu.Close();
    }

    public void ConfigButton()
    {
        AudioManager.instance.coreSounds.PlayOpenMenuSound();
        configMenu.SetActive(true);
    }

    public void SaveAndQuitButton() 
    {
        
    }

    public void WishlistButton()
    {
        Application.OpenURL(Constants.STEAM_PAGE_URL);
    }
}