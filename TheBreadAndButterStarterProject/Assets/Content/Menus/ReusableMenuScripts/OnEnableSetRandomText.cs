using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[RequireComponent(typeof(Text))]
public class OnEnableSetRandomText : MonoBehaviour
{
    [SerializeField] private List<string> textToRandomize;

    void OnEnable()
    {
        GetComponent<Text>().text = textToRandomize.GetRandom();
    }
}