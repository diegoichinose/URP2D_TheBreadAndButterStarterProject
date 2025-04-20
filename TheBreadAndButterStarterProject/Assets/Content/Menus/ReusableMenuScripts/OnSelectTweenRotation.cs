using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnSelectTweenRotation : MonoBehaviour, ISelectHandler
{
    [SerializeField] private Transform _target;
    [SerializeField] private Quaternion originalRotation;
    [SerializeField] private Vector2 targetRotation;
    [SerializeField] private float duration;
    [SerializeField] private Ease ease;

    void Start() => originalRotation = transform.rotation;

    public void OnSelect(BaseEventData eventData)
    {
        _target.DOKill();
        _target.rotation = originalRotation;
        _target.DORotate(targetRotation, duration, RotateMode.FastBeyond360).SetEase(ease).SetUpdate(true);
    }

    void OnDisable()
    {
        _target.DOKill();
        _target.rotation = originalRotation;
    }
}