using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PropagateButtonColorsToChildren : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Button thisButton;
    [SerializeField] private Image childrenImage;
    [SerializeField] private TMP_Text childrenText;

    public void OnSelect(BaseEventData eventData)
    {
        if (childrenImage != null)
            childrenImage.color = thisButton.colors.selectedColor;
            
        if (childrenText != null)
            childrenText.color = thisButton.colors.selectedColor;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        if (childrenImage != null)
            childrenImage.color = thisButton.colors.normalColor;
            
        if (childrenText != null)
            childrenText.color = thisButton.colors.normalColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (childrenImage != null)
            childrenImage.color = thisButton.colors.highlightedColor;
            
        if (childrenText != null)
            childrenText.color = thisButton.colors.highlightedColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (childrenImage != null)
            childrenImage.color = thisButton.colors.normalColor;
            
        if (childrenText != null)
            childrenText.color = thisButton.colors.normalColor;
    }
}