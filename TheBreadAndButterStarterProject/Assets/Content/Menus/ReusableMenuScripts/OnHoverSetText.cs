using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnHoverSetText : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private List<TMPTextStringPair> textList;

    public void OnPointerEnter(PointerEventData eventData)
    {
        textList.ForEach(x => x.SetText());
    }
}

[Serializable]
public class TMPTextStringPair
{
    public TMP_Text textComponent;
    public string text;

    public void SetText()
    {
        textComponent.text = text;
    }
}