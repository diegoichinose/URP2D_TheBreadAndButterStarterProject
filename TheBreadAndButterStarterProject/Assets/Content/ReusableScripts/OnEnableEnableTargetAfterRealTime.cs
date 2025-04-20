using System.Collections;
using UnityEngine;

public class OnEnableEnableTargetAfterRealTime : MonoBehaviour
{    
    [SerializeField] private GameObject target;
    [SerializeField] private float timerInSeconds;

    void OnEnable()
    {
        target.SetActive(false);
        StartCoroutine(EnableTargetAfterTime());
    }

	private IEnumerator EnableTargetAfterTime() 
	{
		yield return new WaitForSecondsRealtime(timerInSeconds); 
        target.SetActive(true);
    }
}
