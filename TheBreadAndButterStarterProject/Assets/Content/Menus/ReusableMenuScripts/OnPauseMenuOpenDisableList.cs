using System.Collections.Generic;
using UnityEngine;

public class OnPauseMenuOpenDisableList : MonoBehaviour
{
    [SerializeField] private PauseMenuData _pauseMenuData;
    [SerializeField] private List<GameObject> list;

    void Start()
    {
        EnableThese();
        GameEventsManager.instance.OnPauseMenuOpen += DisableThese;
        GameEventsManager.instance.OnPauseMenuClose += EnableThese;
    }

    void OnDestroy()
    {
        GameEventsManager.instance.OnPauseMenuOpen -= DisableThese;
        GameEventsManager.instance.OnPauseMenuClose -= EnableThese;
    }

    private void EnableThese() => list.ForEach(x => x.SetActive(true));
    private void DisableThese() => list.ForEach(x => x.SetActive(false));
}
