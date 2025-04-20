using System.Collections.Generic;
using UnityEngine;

public class OnEnableSetRandomSpriteFromList : MonoBehaviour
{
    [SerializeField] private List<Sprite> sprites;

    void OnEnable() => GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Count)];
}