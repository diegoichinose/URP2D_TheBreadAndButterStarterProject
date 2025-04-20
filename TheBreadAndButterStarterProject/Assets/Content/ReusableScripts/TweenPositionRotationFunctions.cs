using DG.Tweening;
using UnityEngine;

public class TweenPositionRotationFunctions : MonoBehaviour
{
    [SerializeField] private Ease ease;
    [SerializeField] private float moveDuration;
    [SerializeField] private Vector2 originalLocalPosition;
    [SerializeField] private Vector2 customLocalPosition;
    [SerializeField] private float rotationDuration;
    [SerializeField] private Vector2 originalRotation;
    [SerializeField] private Vector2 customRotation;
    private Tweener rotateTweener;
    
    void OnDisable()
    {
        transform.DOKill();
    }

    public void TransitionToOriginalPositionRotation()
    {
        transform.DOKill();
        Move(originalLocalPosition);

        if (rotationDuration > 0f)
            Rotate(originalRotation);
    }

    public void TransitionToCustomPositionRotation()
    {
        transform.DOKill();
        transform.localPosition = originalLocalPosition;
        Move(customLocalPosition);

        if (rotationDuration > 0f)
            Rotate(customRotation);
    }

    private void Move(Vector2 targetLocalPosition)
    {
        if (transform.localPosition.x != targetLocalPosition.x)
            transform.DOLocalMoveX(targetLocalPosition.x, moveDuration).SetEase(ease).SetUpdate(true);

        if (transform.localPosition.y != targetLocalPosition.y)
            transform.DOLocalMoveY(targetLocalPosition.y, moveDuration).SetEase(ease).SetUpdate(true);
    }

    private void Rotate(Vector2 targetRotation)
    {
        transform.rotation = Quaternion.identity;
        transform.DORotate(targetRotation, rotationDuration, RotateMode.FastBeyond360).SetEase(Ease.OutElastic).SetUpdate(true);
    }
}