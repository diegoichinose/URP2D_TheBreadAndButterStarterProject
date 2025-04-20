using UnityEngine;
using UnityEngine.EventSystems;

public class OnHoverEnableTarget : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject target;

    void Start()
    {
        target.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData) => target.SetActive(true);
    public void OnPointerExit(PointerEventData eventData) => target.SetActive(false);
}