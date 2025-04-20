using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MyButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TweenStateBundle optionalSelectDeselectAnimations;
    [SerializeField] private MyButtonColorState myButtonColorState;
    [SerializeField] private List<MyButtonSpriteState> myButtonSpriteStateList;
    
    [Header("Actions")]
    [SerializeField] private List<GameObject> onSelectEnableTheseGameObjects;
    [SerializeField] private UnityEvent onSelectInvoke;
    [SerializeField] private UnityEvent onDeselectInvoke;
    [SerializeField] private bool canRepeatInvokation;

    [Header("DO NOT TOUCH - FOR INSPECTION ONLY")]
    public Action<MyButton> OnSelect;  // THIS ACTION IS SETUP AND USED BY MyButtonGroup TO DESELECT OTHER BUTTONS IN THE GROUP
    public bool isSelected;
    public bool isDisabled;
    public int orderIndex;

    public void OnPointerClick(PointerEventData eventData) => Select();
    public void Select() 
    {
        if (isDisabled)
            return;
        
        if (canRepeatInvokation == false)
        if (isSelected)
            return;

        isSelected = true;
        myButtonColorState.OnSelect();
        myButtonSpriteStateList.ForEach(x => x.UpdateImageToDeselectState());

        if (optionalSelectDeselectAnimations != null)
            optionalSelectDeselectAnimations.TransitionTo(TweenStateLabel.OnSelect);

        OnSelect?.Invoke(this); // SEND SIGNAL TO BUTTON GROUP TO DESELECT OTHERS AND DISABLE ALL CONTENT BY DEFAULT
        onSelectEnableTheseGameObjects.ForEach(x => x.SetActive(true));
        onSelectInvoke?.Invoke(); // KEEPING THIS ORDER IS IMPORTANT, AS SOME CUSTOM LOGIC MIGHT WANT TO OVERRIDE DEFAULT BUTTON BEHAVIOR
    }

    public void Deselect() 
    {
        if (canRepeatInvokation == false)
        if (isSelected == false)
            return;

        isSelected = false;
        myButtonColorState.OnDeselect();
        myButtonSpriteStateList.ForEach(x => x.UpdateImageToDeselectState());
        onSelectEnableTheseGameObjects.ForEach(x => x.SetActive(false));

        if (optionalSelectDeselectAnimations != null)
            optionalSelectDeselectAnimations.TransitionTo(TweenStateLabel.OnDeselect);

        onDeselectInvoke?.Invoke();
    }
    
    public void OverrideColor(Color color)
    {
        var colorWithLowerAlpha = color;
            colorWithLowerAlpha.a = 0.5f;

        myButtonColorState.onSelectColor = colorWithLowerAlpha;
        myButtonColorState.onDeselectColor = color;
        myButtonColorState.OnDeselect();
    }
}

[Serializable]
public class MyButtonSpriteState
{
    [SerializeField] private Image image;
    [SerializeField] private Sprite onSelectSprite;
    [SerializeField] private Sprite onDeselectSprite;

    public void UpdateImageToSelectState() 
    {
        image.sprite = onSelectSprite;
    }

    public void UpdateImageToDeselectState() 
    {
        image.sprite = onDeselectSprite;
    }
}

[Serializable]
public class MyButtonColorState
{
    public Color onSelectColor;
    public Color onDeselectColor;
    [SerializeField] private List<Image> colorChangeTheseImages;
    [SerializeField] private List<TMP_Text> colorChangeTheseTexts;

    public void OnSelect() 
    {
        colorChangeTheseImages.ForEach(x => x.color = onSelectColor);
        colorChangeTheseTexts.ForEach(x => x.color = onSelectColor);
    }

    public void OnDeselect() 
    {
        colorChangeTheseImages.ForEach(x => x.color = onDeselectColor);
        colorChangeTheseTexts.ForEach(x => x.color = onDeselectColor);
    }
}