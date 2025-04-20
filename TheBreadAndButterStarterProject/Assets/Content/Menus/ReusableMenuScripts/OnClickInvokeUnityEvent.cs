using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OnClickInvokeUnityEvent : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private UnityEvent onClickEvent;

    public void OnPointerClick(PointerEventData eventData)
    {
        onClickEvent?.Invoke();
    }
}