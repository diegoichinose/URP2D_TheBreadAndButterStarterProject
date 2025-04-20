using System.Collections;
using UnityEngine;

public class OnEnableDisableSelfAfterTime : MonoBehaviour
{
    [SerializeField] private float selfDisableTimer;

    void OnEnable()
    {
        StartCoroutine(DestroySelfAfterTime());
    }

	private IEnumerator DestroySelfAfterTime()
	{
		yield return new WaitForSecondsRealtime(selfDisableTimer); 
        gameObject.SetActive(false);
    }
}