
using System.Collections;
using UnityEngine;

public class ReferencesManager : MonoBehaviour
{
    public static ReferencesManager instance = null;
    
    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);
    }

    public Coroutine StartThisCoroutine(IEnumerator coroutine) => StartCoroutine(coroutine);
    public void StopThisCoroutine(Coroutine coroutine) => StopCoroutine(coroutine);
    public void DebugLog(string log) => Debug.Log(log);
}