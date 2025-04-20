using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public enum NavigationType
{
    HorizontalShoulders,
    Horizontal,
    Vertical,
    Matrix
}

public class MyButtonGroup : MonoBehaviour
{
    [SerializeField] private bool selectFirstButtonOnStart;
    [SerializeField] private bool selectFirstActiveButtonOnStart;
    [SerializeField] private List<MyButton> buttons;
    [SerializeField] private List<GameObject> onAnyButtonSelectDisableTheseGameObjects;
    [SerializeField] private NavigationType navigationType;
    [SerializeField] private bool muteSound;
    private List<MyButton> activeButtons => buttons.Where(x => x.gameObject.activeInHierarchy).ToList();
    private int currentlySelectedButtonIndex = 0;
    private GameInput _input;

    void Start()
    {
        var orderIndex = 0;
        activeButtons.ForEach(button => 
        {
            button.OnSelect += OnButtonSelect;
            button.orderIndex = orderIndex;
            orderIndex++;
        });

        buttons = buttons.OrderBy(x => x.orderIndex).ToList();
        if (selectFirstButtonOnStart)
            buttons[0].Select();
        else if (selectFirstActiveButtonOnStart)
            activeButtons[0].Select();
    }

    void OnEnable()
    {
        _input = new GameInput();
        _input.Enable();

        if (navigationType == NavigationType.HorizontalShoulders)
        {
            _input.Menu.NavigateTabLeft.performed += SelectPreviousButtonFromInput; // NAVIGATE WITH Q, L1
            _input.Menu.NavigateTabRight.performed += SelectNextButtonFromInput; // NAVIGATE WITH R, R1
        }
        else if (navigationType == NavigationType.Horizontal)
        {
            _input.Menu.NavigateLeft.performed += SelectPreviousButtonFromInput;
            _input.Menu.NavigateRight.performed += SelectNextButtonFromInput;
        }
        else if (navigationType == NavigationType.Vertical)
        {
            _input.Menu.NavigateUp.performed += SelectPreviousButtonFromInput;
            _input.Menu.NavigateDown.performed += SelectNextButtonFromInput;
        }
        
        buttons[currentlySelectedButtonIndex].Select();
    }

    void OnDisable()
    {
        _input.Disable();
        if (navigationType == NavigationType.HorizontalShoulders)
        {
            _input.Menu.NavigateTabLeft.performed -= SelectPreviousButtonFromInput;
            _input.Menu.NavigateTabRight.performed -= SelectNextButtonFromInput;
        }
        else if (navigationType == NavigationType.Horizontal)
        {
            _input.Menu.NavigateLeft.performed -= SelectPreviousButtonFromInput;
            _input.Menu.NavigateRight.performed -= SelectNextButtonFromInput;
        }
        else if (navigationType == NavigationType.Vertical)
        {
            _input.Menu.NavigateUp.performed -= SelectPreviousButtonFromInput;
            _input.Menu.NavigateDown.performed -= SelectNextButtonFromInput;
        }
    }

    void OnDestroy()
    {
        buttons.ForEach(button => button.OnSelect -= OnButtonSelect);
    }
        
    private void OnButtonSelect(MyButton button) 
    {
        currentlySelectedButtonIndex = button.orderIndex;

        if (muteSound == false)
            AudioManager.instance.coreSounds.PlayClickSound();

        DeselectAllButtonsExcept(thisButton: button);
        onAnyButtonSelectDisableTheseGameObjects.ForEach(x => x.SetActive(false));
    }
    
    private void DeselectAllButtonsExcept(MyButton thisButton) 
    {
        buttons.Where(x => x != thisButton).ToList().ForEach(button => button.Deselect());
    }
    
    private void SelectPreviousButtonFromInput(InputAction.CallbackContext context)
    {
        if (currentlySelectedButtonIndex <= 0)
            return;

        currentlySelectedButtonIndex--;
        activeButtons[currentlySelectedButtonIndex].Select();

        if (muteSound == false)
            AudioManager.instance.coreSounds.PlayMenuNavigationSound();
    }

    private void SelectNextButtonFromInput(InputAction.CallbackContext context)
    {
        if (currentlySelectedButtonIndex >= activeButtons.Count - 1)
            return;

        currentlySelectedButtonIndex++;
        activeButtons[currentlySelectedButtonIndex].Select();

        if (muteSound == false)
            AudioManager.instance.coreSounds.PlayMenuNavigationSound();
    }
    
    public void OverrideColor(Color color)
    {
        buttons.ForEach(button => button.OverrideColor(color));
    }
}