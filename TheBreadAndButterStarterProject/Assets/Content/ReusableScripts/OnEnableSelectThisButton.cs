using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Selectable))]
public class OnEnableSelectThisButton : MonoBehaviour
{
    void OnEnable() 
    {
        StartCoroutine(SelectThisAfterTime());
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator SelectThisAfterTime()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        GetComponent<Selectable>().Select();
    }
}