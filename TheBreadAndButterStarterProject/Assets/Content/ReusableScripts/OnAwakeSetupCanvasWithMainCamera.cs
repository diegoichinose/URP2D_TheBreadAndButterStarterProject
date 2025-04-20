using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class OnAwakeSetupCanvasWithMainCamera : MonoBehaviour
{
    void Awake()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
    }
}