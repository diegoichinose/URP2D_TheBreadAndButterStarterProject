using UnityEngine;

public class OnStartDisableSelf : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(false);
    }
}