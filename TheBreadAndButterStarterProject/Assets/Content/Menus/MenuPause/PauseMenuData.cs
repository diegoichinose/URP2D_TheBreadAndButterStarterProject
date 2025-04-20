using UnityEngine;

[CreateAssetMenu(menuName = "My Scriptable Objects/Menu Data/Pause Menu Data")]
public class PauseMenuData : BaseMenuData
{
    private bool wasPlayerAllowedToMoveWhilePaused;

    public override void OnOpen()
    {
        base.OnOpen();
        AudioManager.instance.coreSounds.PlayOpenMenuSound();
        EventSystemManager.instance.SwitchToDefaultEventSystem();
        GameEventsManager.instance.OnPauseMenuOpen?.Invoke();
    }

    public override void OnClose()
    {
        base.OnClose();
        AudioManager.instance.coreSounds.PlayCloseMenuSound();
        EventSystemManager.instance.SwitchToPreviousEventSystem();
        GameEventsManager.instance.OnPauseMenuClose?.Invoke();
    }
}