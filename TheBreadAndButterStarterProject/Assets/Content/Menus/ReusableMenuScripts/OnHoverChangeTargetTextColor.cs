using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnHoverChangeTargetTextColor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
     [SerializeField] private Color onPointerEnterColor;
     [SerializeField] private Color onPointerExitColor;
     [SerializeField] private List<TMP_Text> colorChangeTargets;
    
    public void OnPointerEnter(PointerEventData eventData) => colorChangeTargets.ForEach(x => x.color = onPointerEnterColor);
    public void OnPointerExit(PointerEventData eventData) => colorChangeTargets.ForEach(x => x.color = onPointerExitColor);
    void OnEnable() => colorChangeTargets.ForEach(x => x.color = onPointerExitColor);
}