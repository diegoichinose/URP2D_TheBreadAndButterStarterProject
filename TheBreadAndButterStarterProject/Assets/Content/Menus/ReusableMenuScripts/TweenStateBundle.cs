using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class TweenStateBundle : MonoBehaviour
{
    [SerializeField] private List<TransformTweenStateList> tweenStates;

    void OnDisable()
    {
        tweenStates.ForEach(x => x.DOKill());
    }

    public void TransitionTo(TweenStateLabel state)
    {
        var tweenState = tweenStates.FirstOrDefault(x => x.stateLabel == state);

        if (tweenState == null)
            return;

        tweenState.Play();
    }
}

[Serializable]
public class TransformTweenStateList
{
    public TweenStateLabel stateLabel;
    [SerializeField] private List<TransformTweenState> list;
    public void DOKill() => list.ForEach(x => x.DOKill());
    public void Play() => list.ForEach(x => x.Play());
}

[Serializable]
public class TransformTweenState
{
    [SerializeField] private Transform transform;
    [SerializeField] private Vector3 localPosition;
    [SerializeField] private Vector3 rotation;
    [SerializeField] private Ease ease;
    [SerializeField] private float duration;
    public void DOKill() => transform.DOKill();
    public void Play()
    {   
        DOKill();
        transform.DOLocalMove(localPosition, duration).SetEase(ease).SetUpdate(true);
        transform.DOLocalRotate(rotation, duration, RotateMode.FastBeyond360).SetEase(Ease.OutElastic).SetUpdate(true);
    }
}

public enum TweenStateLabel
{
    None,
    Center,
    Left,
    Right,
    Up,
    Down,
    OnSelect,
    OnDeselect
}