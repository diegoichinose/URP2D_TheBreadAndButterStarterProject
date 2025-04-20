using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnHoverSelectThisButton : MonoBehaviour, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData) => GetComponent<Selectable>().Select();
}