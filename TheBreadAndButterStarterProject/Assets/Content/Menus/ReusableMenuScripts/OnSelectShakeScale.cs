using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class OnSelectShakeScale : MonoBehaviour, ISelectHandler
{
    [SerializeField] private float duration = 0.3f;
    [SerializeField] private int strength = 1;
    [SerializeField] private int vibrato = 10;
    [SerializeField] private int randomness = 90;
    [SerializeField] private bool fadeOut = true;
    [SerializeField] private bool ignoreTimeScale = true;
    private Vector3 originalScale;
    private Tweener tween;

    void OnEnable()
    {
        originalScale = transform.localScale;
    }

    void OnDisable()
    {
        transform.localScale = originalScale;
        transform.DOKill();
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (tween == null || !tween.IsActive())
        {
            transform.localScale = originalScale;
            tween = transform.DOShakeScale(duration, strength, vibrato, randomness, fadeOut).SetUpdate(ignoreTimeScale);
        }
    }
}