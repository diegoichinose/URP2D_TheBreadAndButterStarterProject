using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LongClickButtonNonSelectable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool canInteract;
    [SerializeField] private Image progressBarFill;
    [SerializeField] private float requiredHoldTime;
    [SerializeField] private bool shakeOnHold = true;
    [SerializeField] private bool scaleUpOnHold = true;
    [SerializeField] private bool disableDefaultInteractInputListener;
    [SerializeField] private UnityEvent onLongClick;
    
    private GameInput _input;
    private Tweener shakeAnimation;
    private Vector3 originalScale;
    private bool isHoldingInteract;
    private float holdDuration;
    private Vector2 originalLocalPosition;

    private bool DidYouHoldLongEnough() => holdDuration >= requiredHoldTime;
    public void OnPointerUp(PointerEventData eventData) => Reset();
    public void OnPointerDown(PointerEventData eventData) => OnInteract();
    public void OnInteractRelease(InputAction.CallbackContext input)  => Reset();  
    public void OnInteract(InputAction.CallbackContext input) => OnInteract();
    private void OnInteract() 
    {
        if (canInteract == false)
        {
            PlayShakeAnimationStrong();
            AudioManager.instance.coreSounds.PlayErrorSound();
            return;
        }

        isHoldingInteract = true;
        AudioManager.instance.coreSounds.PlayMenuNavigationSound();
        AudioManager.instance.coreSounds.PlayLongClickButtonHoldSound();
    } 

    void Awake()
    {
        _input = new GameInput();
        originalScale = transform.localScale;
        originalLocalPosition = transform.localPosition;
        canInteract = true;
    }

    void OnEnable()
    {
        if (disableDefaultInteractInputListener == false)
        {
            _input.Enable();
            _input.Menu.Confirm.performed += OnInteract;
            _input.Menu.Confirm.canceled += OnInteractRelease;
        }
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
        isHoldingInteract = false;
        holdDuration = 0;
        SetProgressBarFill(0);
        
        AudioManager.instance.coreSounds.StopLongClickButtonHoldSound(); 
        AudioManager.instance.coreSounds.PlaySuccessSound(); 
    }
    
    private void SetProgressBarFill(float percentage, float delay = 0) 
    {
        if (progressBarFill == null)
            return;
            
        DOTween.To(() => progressBarFill.fillAmount, x => progressBarFill.fillAmount = x, percentage, 0.2f).SetDelay(delay).SetUpdate(true);
    }

    private void PlayIncraseScaleAnimation()
    {
        float sizeToIncrease = gameObject.transform.localScale.x + (holdDuration / requiredHoldTime * 0.01f);
        transform.DOScale(sizeToIncrease, 0.1f).SetUpdate(true);
    }

    private void PlayShakeAnimation()
    {
        if (shakeAnimation == null || !shakeAnimation.IsActive())
            shakeAnimation = transform.DOShakePosition(0.3f, 5f, 25, 90, false).SetUpdate(true);
    }
    
	private void PlayShakeAnimationStrong()
    {
        transform
            .DOShakePosition(duration: 0.3f, strength: new Vector2(20f, 20f), vibrato: 50, randomness: 90, snapping: true, fadeOut: false)
            .SetUpdate(true)
            .OnComplete(() => transform.localPosition = originalLocalPosition);
    }
}