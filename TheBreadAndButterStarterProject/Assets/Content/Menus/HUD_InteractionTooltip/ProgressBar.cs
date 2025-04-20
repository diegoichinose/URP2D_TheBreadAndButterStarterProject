using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image progressBarIcon;
    [SerializeField] private Image progressBarFill;
    protected Tweener progressBarAnimation;
    
    void OnDisable()
    {
        progressBarFill.DOKill();
        DOTween.Kill(progressBarAnimation);
    }

    public void SetupIcon(Sprite icon)
    {
        progressBarIcon.sprite = icon;
    }
    
    public void SetFill(float percentage, float delay = 0) 
    {
        DOTween.Kill(progressBarAnimation);
        progressBarAnimation = DOTween.To(() => progressBarFill.fillAmount, x => progressBarFill.fillAmount = x, percentage, 0.2f).SetDelay(delay);
    }
}