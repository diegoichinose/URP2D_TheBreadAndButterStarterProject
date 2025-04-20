using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class AutoScrollUpEffect : MonoBehaviour
{
    [SerializeField] private float originalPositionY;
    [SerializeField] private float targetPositionY;
    [SerializeField] private float duration;
    [SerializeField] private float durationWhenSpeedUp;
    private GameInput _input;

    void OnEnable()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, originalPositionY, transform.localPosition.z);
        transform.DOLocalMoveY(targetPositionY, duration).SetUpdate(true);

        _input = new GameInput();
        _input.Enable();
        _input.Menu.Confirm.performed += OnInteractInput;
        _input.Menu.Confirm.canceled += OnInteractInputRelease;
    }
    
    void OnDisable()
    {
        transform.DOKill();

        _input.Menu.Confirm.performed -= OnInteractInput;
        _input.Menu.Confirm.canceled -= OnInteractInputRelease;
        _input.Disable();
        _input = null;
    }
    
    private void ChangeScrollingSpeed(float durationToApply)
    {
        transform.DOKill();
        transform.DOLocalMoveY(targetPositionY, durationToApply).SetUpdate(true);
    }

    private void OnInteract() => ChangeScrollingSpeed(durationToApply: durationWhenSpeedUp);
    private void OnInteractInput(InputAction.CallbackContext input) => OnInteract();
    private void OnInteractInputRelease(InputAction.CallbackContext input)  => ChangeScrollingSpeed(durationToApply: duration);

}