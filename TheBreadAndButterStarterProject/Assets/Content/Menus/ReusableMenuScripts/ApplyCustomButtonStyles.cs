using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ApplyCustomButtonStyles : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    public Color selectedImageColor;
    public Color selectedTextColor;
    public Color deselectedImageColor;
    public Color deselectedTextColor;
    [SerializeField] private List<Image> images;
    [SerializeField] private List<TMP_Text> texts;
    [SerializeField] private bool onSelectPlaySound;
    [SerializeField] private bool onHoverSelectButton;
    [SerializeField] private bool onHoverSetHandPointingCursor;
    private Button _thisButton;
    private bool isHandCursorSet;

    public void OnSelect(BaseEventData eventData) => ApplySelectedColors();
    public void OnDeselect(BaseEventData eventData) => ApplyDeselectedColors();
    public void OnPointerExit(PointerEventData eventData) => ResetHandPointingCursor();
    public void OnPointerEnter(PointerEventData eventData) 
    {
        if (onHoverSelectButton)
            _thisButton.Select();

        if (onHoverSetHandPointingCursor)
            SetHandPointingCursor();
    }

    void Awake()
    {
        _thisButton = GetComponent<Button>();
    }

    void OnDisable()
    {
        ApplyDeselectedColors();
        
        if (isHandCursorSet)
            ResetHandPointingCursor();
    }

    private void ApplySelectedColors()
    {
        if (onSelectPlaySound)
            AudioManager.instance.coreSounds.PlayMenuNavigationSound();
        
        images.ForEach(image => image.color = selectedImageColor);
        texts.ForEach(text => text.color = selectedTextColor);
    }

    private void ApplyDeselectedColors()
    {
        images.ForEach(image => image.color = deselectedImageColor);
        texts.ForEach(text => text.color = deselectedTextColor);
    }

    private void SetHandPointingCursor()
    {
        CursorManager.instance.SetHandPointingCursor();
        isHandCursorSet = true;
    }

    private void ResetHandPointingCursor()
    {
        CursorManager.instance.ResetHandPoitingCursor();
        isHandCursorSet = false;
    }
}