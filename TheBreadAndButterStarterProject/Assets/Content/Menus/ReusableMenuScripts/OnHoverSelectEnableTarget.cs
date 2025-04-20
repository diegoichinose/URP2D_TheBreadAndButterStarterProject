using UnityEngine;
using UnityEngine.EventSystems;

public class OnHoverSelectEnableTarget : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    [SerializeField] private GameObject target;

    void Start()
    {
        target.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData) => target.SetActive(true);
    public void OnPointerExit(PointerEventData eventData) => target.SetActive(false);
    public void OnSelect(BaseEventData eventData) => target.SetActive(true);
    public void OnDeselect(BaseEventData eventData) => target.SetActive(false);
}