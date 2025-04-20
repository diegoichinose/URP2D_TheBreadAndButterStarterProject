using DG.Tweening;
using UnityEngine;

public class OnEnableScaleIn : MonoBehaviour
{
    private Vector3 originalScale;
    [SerializeField] private float animationDuration;
    [SerializeField] private float delay;
    [SerializeField] private Ease ease;
    [SerializeField] private bool onCompleteDeleteScript;
    
    void Awake()
    {
        originalScale = transform.localScale;
    }

    void OnEnable()
    {
        transform.localScale = Vector3.zero; 
        transform.DOScale(originalScale, animationDuration)
                 .SetEase(ease)
                 .SetUpdate(true)
                 .SetDelay(delay)
                 .OnComplete(() => 
                 {
                    if (onCompleteDeleteScript)
                        Destroy(this);
                 });
    }
    
    void OnDisable()
    {
        transform.localScale = originalScale;
        transform.DOKill();
    }
}