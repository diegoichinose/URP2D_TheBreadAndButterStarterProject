using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class OnEnablePlaySound : MonoBehaviour
{
    [SerializeField] private float delayInSeconds;
    [SerializeField] private UnityEvent AudioManagerMethodToPlay;

    void OnEnable()
    {
        StartCoroutine(PlaySoundAfterDelay());
    }

    void OnDisable()
    {   
        StopAllCoroutines();
    }

	private IEnumerator PlaySoundAfterDelay() 
	{
		yield return new WaitForSecondsRealtime(delayInSeconds);
        AudioManagerMethodToPlay?.Invoke();
    }
}
