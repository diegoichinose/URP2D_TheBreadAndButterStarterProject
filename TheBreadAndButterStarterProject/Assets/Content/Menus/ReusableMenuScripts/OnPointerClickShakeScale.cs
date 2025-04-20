using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnPointerClickShakeScale : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private float duration = 0.3f;
    [SerializeField] private int strength = 1;
    [SerializeField] private int vibrato = 10;
    [SerializeField] private int randomness = 90;
    [SerializeField] private bool fadeOut = true;
    [SerializeField] private bool ignoreTimeScale = true;
    private Vector3 originalScale;

    void OnEnable()
    {
        originalScale = transform.localScale;
    }

    void OnDisable()
    {
        transform.localScale = originalScale;
        transform.DOKill();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        transform.localScale = originalScale;
        transform.DOShakeScale(duration, strength, vibrato, randomness, fadeOut)
                 .SetUpdate(ignoreTimeScale)
                 .OnComplete(() => transform.localScale = originalScale);
    }
}
