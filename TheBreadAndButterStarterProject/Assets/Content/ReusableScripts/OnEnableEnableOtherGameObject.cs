using System.Collections;
using UnityEngine;

public class OnEnableEnableOtherGameObject : MonoBehaviour
{
    [SerializeField] private GameObject gameObjectToEnable;
    [SerializeField] private float delayInSeconds;

    void OnEnable()
    {
        gameObjectToEnable.SetActive(false);   
        StartCoroutine(EnableGameObjectAfterDelay());
    }

    void OnDisable()
    {
        gameObjectToEnable.SetActive(false); 
    }

	private IEnumerator EnableGameObjectAfterDelay() 
	{
		yield return new WaitForSecondsRealtime(delayInSeconds);
        gameObjectToEnable.SetActive(true);
    }
}