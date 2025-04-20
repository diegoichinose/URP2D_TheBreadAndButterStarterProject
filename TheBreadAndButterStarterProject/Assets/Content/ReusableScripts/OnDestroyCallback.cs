using System;
using UnityEngine;

public class OnDestroyCallback : MonoBehaviour
{
    public Action<GameObject> Callback;

    void OnDestroy()
    {
        Callback?.Invoke(gameObject);
    }
}