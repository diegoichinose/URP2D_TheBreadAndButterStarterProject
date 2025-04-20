using UnityEngine;

public class EnableDisableGameObject : MonoBehaviour
{
    public GameObject target;

    public void Enable() => target.SetActive(true);
    public void Disable() => target.SetActive(false);
}
