using UnityEngine;

public class DestroySelfFunction : MonoBehaviour
{
    // USED BY ANIMATION TRIGGERS (eg. DESTROY SELF AFTER VFX ANIMATION COMPLETES)
    [SerializeField] private GameObject destroyTarget;
    public void DestroySelf() => Destroy(destroyTarget);
}