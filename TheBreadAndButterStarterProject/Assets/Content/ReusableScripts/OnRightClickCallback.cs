using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OnRightClickCallback : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent callback;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
            callback.Invoke();
    }
}