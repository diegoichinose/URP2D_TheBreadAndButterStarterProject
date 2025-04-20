using System.Collections;
using UnityEngine;

public class OnEnableDestroySelfAfterRealTime : MonoBehaviour
{
    [SerializeField] private float selfDestructionTimer;

    void OnEnable()
    {
        StartCoroutine(DestroySelfAfterTime());
    }

	private IEnumerator DestroySelfAfterTime() 
	{
		yield return new WaitForSecondsRealtime(selfDestructionTimer); 
        Destroy(gameObject);
    }
}