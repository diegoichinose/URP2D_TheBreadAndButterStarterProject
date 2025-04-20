using System.Collections;
using DG.Tweening;
using UnityEngine;

public class OnEnableLoopPulseScaleFade : MonoBehaviour
{
    [SerializeField] private float loopCooldown;
    [SerializeField] private float scaleDuration;
    [SerializeField] private float fadeDuration;
    [SerializeField] private Ease ease;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Vector3 originalScale;
    private Color originalColor;
    private Tweener tweener;

    void Awake()
    {
        originalColor = spriteRenderer.color;
        originalScale = transform.localScale;
    }

    void OnEnable()
    {
        StartCoroutine(LoopAnimationAfterTime());
    }

    void OnDisable()
    {
        StopAllCoroutines();
        transform.DOKill();
        spriteRenderer.DOKill();
        tweener?.Kill();
    }

    private IEnumerator LoopAnimationAfterTime()
    {
        while (true)
        {
            Animate();

            yield return new WaitForSeconds(loopCooldown);

            if (PauseResumeTimeManager.instance.isPaused)
                continue;
        }
    }

    private void Animate()
    {
        spriteRenderer.color = originalColor;
        transform.localScale = Vector3.zero;
        transform.DOScale(originalScale, scaleDuration).SetEase(ease);

        tweener = DOTween.To(()=> 
                    spriteRenderer.color.a,
                    targetAlpha => spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, targetAlpha),
                    0f, 
                    fadeDuration);
    }
}