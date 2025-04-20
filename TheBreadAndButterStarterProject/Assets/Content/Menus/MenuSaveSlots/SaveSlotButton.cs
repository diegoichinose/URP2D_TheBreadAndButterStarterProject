using UnityEngine;
using UnityEngine.EventSystems;

public class SaveSlotButton : MonoBehaviour, ISelectHandler
{
    public void OnSelect(BaseEventData eventData)
    {
        AudioManager.instance.coreSounds.PlayMenuNavigationSound();
    }
}
