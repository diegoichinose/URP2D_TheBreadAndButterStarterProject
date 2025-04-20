using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class OnStartSetMainCameraToCanvas : MonoBehaviour
{
    void Start()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
    }
}