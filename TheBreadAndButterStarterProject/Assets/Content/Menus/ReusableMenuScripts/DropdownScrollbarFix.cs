using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

[RequireComponent(typeof(ScrollRect))]
public class DropdownScrollbarFix : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float scrollSpeed = 10f;
    private bool mouseOver = false;
    private List<Selectable> m_Selectables = new List<Selectable>();
    private ScrollRect m_ScrollRect;
    private Vector2 m_NextScrollPosition = Vector2.up;
    
    public void OnPointerEnter(PointerEventData eventData) => mouseOver = true;
    public void OnPointerExit(PointerEventData eventData) 
    {
        mouseOver = false;
        ScrollToSelected(false);
    }

    void OnEnable() 
    {
        if (m_ScrollRect) 
            m_ScrollRect.content.GetComponentsInChildren(m_Selectables);
    }


    void Awake() 
    {
        m_ScrollRect = GetComponent<ScrollRect>();
    }

    void Start() 
    {
        if (m_ScrollRect)
            m_ScrollRect.content.GetComponentsInChildren(m_Selectables);
            
        ScrollToSelected(true);
        EventSystemManager.instance.isDropdownButtonOpen = true;
        EventSystemManager.instance.closeDropdownButtonAction += CloseDropdown;
    }

    void OnDestroy()
    {
        EventSystemManager.instance.isDropdownButtonOpen = false;
        EventSystemManager.instance.closeDropdownButtonAction -= CloseDropdown;
    }

    private void CloseDropdown() 
    {
        GetComponentInParent<TMP_Dropdown>().Hide();
    }

    void Update() 
    {
        // Do nothing if we are on mobile AND we dont have a gamepad connected
        if (SystemInfo.deviceType == DeviceType.Handheld && Gamepad.all.Count <= 1)
            return;

        // Scroll via input.
        InputScroll();
        if (!mouseOver)
            m_ScrollRect.normalizedPosition = Vector2.Lerp(m_ScrollRect.normalizedPosition, m_NextScrollPosition, scrollSpeed * Time.unscaledDeltaTime);
        else 
            m_NextScrollPosition = m_ScrollRect.normalizedPosition;
    }

#nullable enable
    void InputScroll() 
    {
        if (m_Selectables.Count > 0) 
        {
            Keyboard? currentKeyboard = Keyboard.current;
            Gamepad? currentGamepad = Gamepad.current;

            if (currentKeyboard != null) 
                if (Keyboard.current.upArrowKey.isPressed || Keyboard.current.downArrowKey.isPressed)
                    ScrollToSelected(false);

            if (currentGamepad != null) 
                if (Gamepad.current.dpad.up.isPressed || Gamepad.current.dpad.down.isPressed)
                    ScrollToSelected(false);
        }
    }

#nullable disable
    void ScrollToSelected(bool quickScroll) 
    {
        int selectedIndex = -1;
        Selectable selectedElement = EventSystem.current.currentSelectedGameObject ? EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>() : null;

        if (selectedElement) 
            selectedIndex = m_Selectables.IndexOf(selectedElement);
            
        if (selectedIndex > -1) 
        {
            if (quickScroll) 
            {
                m_ScrollRect.normalizedPosition = new Vector2(0, 1 - (selectedIndex / ((float)m_Selectables.Count - 1)));
                m_NextScrollPosition = m_ScrollRect.normalizedPosition;
            }
            else 
                m_NextScrollPosition = new Vector2(0, 1 - (selectedIndex / ((float)m_Selectables.Count - 1)));
        }
    }
}