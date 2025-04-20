using UnityEngine;

public class OnEnableDisableTargetMutuallyExclusive : MonoBehaviour
{
    [SerializeField] private GameObject target;

    void OnEnable()
    {
        target.SetActive(false);
    }
    
    void OnDisable()
    {
        target.SetActive(true);
    }
}