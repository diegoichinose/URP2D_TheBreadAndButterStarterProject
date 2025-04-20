using UnityEngine;
using UnityEngine.EventSystems;

public class OnSelectEnableTarget : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] private GameObject target;

    void OnEnable()
    {
        target.SetActive(false);
    }
    
    public void OnSelect(BaseEventData eventData) => target.SetActive(true);
    public void OnDeselect(BaseEventData eventData) => target.SetActive(false);
}