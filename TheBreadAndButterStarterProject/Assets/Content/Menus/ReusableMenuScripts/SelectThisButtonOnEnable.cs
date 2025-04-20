using UnityEngine;
using UnityEngine.UI;

public class SelectThisButtonOnEnable : MonoBehaviour
{
    [SerializeField] private Selectable button;
    void OnEnable() => button.Select();
}