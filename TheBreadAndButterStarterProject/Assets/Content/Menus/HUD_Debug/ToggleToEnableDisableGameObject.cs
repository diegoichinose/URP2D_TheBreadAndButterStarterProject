using System.Collections.Generic;
using UnityEngine;

public class ToggleToEnableDisableGameObject : MonoBehaviour
{
    [SerializeField] private List<GameObject> enableDisableThese;

    public void OnToggleUpdate() 
    {
        enableDisableThese.ForEach(x => x.SetActive(!x.activeSelf));
    }
}