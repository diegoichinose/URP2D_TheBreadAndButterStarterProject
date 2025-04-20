using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LongClickButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, ISelectHandler, IDeselectHandler
{
    [SerializeField] private GameObject progressBar;
    [SerializeField] private Image progressBarFill;
    [SerializeField] private Selectable selectable;

    public float requiredHoldTime;
    public bool canInteract;
    public bool shakeOnHold = true;
    public bool scaleUpOnHold = true;
    public UnityEvent onLongClick;
    
    private GameInput _input;
    private Tweener shakeAnimation;
    private Vector3 originalScale;
    private bool isSelected;
    private bool isHoldingInteract;
    private float holdDuration;

    public void OnSelect(BaseEventData eventData) 
    {
        progressBar.SetActive(true);
        isSelected = true;
    }

    public void OnDeselect(BaseEventData eventData) 
    {
        progressBar.SetActive(false);

        if (gameObject.activeSelf)
            isSelected = false; 
    }

    private bool DidYouHoldLongEnough() => holdDuration >= requiredHoldTime;
    public void OnPointerUp(PointerEventData eventData) => Reset();
    public void OnPointerDown(PointerEventData eventData) => OnInteract();
    private void OnInteractRelease(InputAction.CallbackContext input)  => Reset();  
    private void OnInteract(InputAction.CallbackContext input) => OnInteract();
    private void OnInteract() 
    {
        if (canInteract == false)
            return;

        isHoldingInteract = true;
        AudioManager.instance.coreSounds.PlayMenuNavigationSound();
        AudioManager.instance.coreSounds.PlayLongClickButtonHoldSound();
    } 

    void Awake()
    {
        _input = new GameInput();
        originalScale = transform.localScale;
    }

    void OnEnable()
    {
        _input.Enable();
        _input.Menu.Confirm.performed += OnInteract;
        _input.Menu.Confirm.canceled += OnInteractRelease;
        canInteract = true;

        if (isSelected)
            selectable.Select();
    }

    void OnDisable()
    {
        _input.Disable();
        _input.Menu.Confirm.performed -= OnInteract;
        _input.Menu.Confirm.canceled -= OnInteractRelease;
        isHoldingInteract = false;

        progressBarFill.DOKill();
        transform.DOKill();
    }

    private void Update()
    {
        if (canInteract == false)
            return;

        if (!progressBar.activeSelf)
            return;

        if (!isHoldingInteract)
            return;
            
        holdDuration += Time.unscaledDeltaTime;

        if(DidYouHoldLongEnough())
        {
            onLongClick.Invoke();
            Complete();
            return;
        }

        if (shakeOnHold) PlayShakeAnimation();
        if (scaleUpOnHold) PlayIncraseScaleAnimation();
        
        SetProgressBarFill(holdDuration / requiredHoldTime);
    }

    private void Reset()
    {
        isHoldingInteract = false;
        holdDuration = 0;
        SetProgressBarFill(0);
        transform.DOScale(originalScale, 0.2f).SetUpdate(true);
        
        AudioManager.instance.coreSounds.StopLongClickButtonHoldSound(); 
    }

    private void Complete()
    {
        Reset();
        AudioManager.instance.coreSounds.PlaySuccessSound();
    }
    
    private void SetProgressBarFill(float percentage, float delay = 0) 
    {
        if (progressBarFill == null)
            return;
            
        DOTween.To(() => progressBarFill.fillAmount, x => progressBarFill.fillAmount = x, percentage, 0.2f).SetDelay(delay).SetUpdate(true);
    }

    private void PlayShakeAnimation()
    {
        if (shakeAnimation == null || !shakeAnimation.IsActive())
            shakeAnimation = transform.DOShakePosition(0.3f, 5f, 25, 90, false).SetUpdate(true);
    }

    private void PlayIncraseScaleAnimation()
    {
        float sizeToIncrease = gameObject.transform.localScale.x + (holdDuration / requiredHoldTime * 0.01f);
        transform.DOScale(sizeToIncrease, 0.1f).SetUpdate(true);
    }
}