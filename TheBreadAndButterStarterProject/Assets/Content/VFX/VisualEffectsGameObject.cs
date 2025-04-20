using System;
using System.Collections;
using UnityEngine;

public class VisualEffectsGameObject : MonoBehaviour
{
    protected SerializableGuid instanceId;
    protected Action<SerializableGuid> OnDestroyCallback; 

    void OnDisable()
    {
        StopAllCoroutines();
    }

    // CALLBACK ATTACHED BY THE VisualEffectsManager TO REMOVE THIS VFX INSTANCE FROM THE POOL
    void OnDestroy()
    { 
        OnDestroyCallback?.Invoke(instanceId);
    }

    // CALLED WHEN INSTANTIATED BY VisualEffectsManager
    public SerializableGuid Setup(Action<SerializableGuid> OnDestroyCallback)
    {
        this.OnDestroyCallback = OnDestroyCallback;
        StartCoroutine(DisableSelftAfterTime(timer: 10));

        instanceId = Guid.NewGuid();
        return instanceId;
    }

    // CALLED WHEN VFX ANIMATION ENDS
    public void DisableSelf() => gameObject.SetActive(false);

    private IEnumerator DisableSelftAfterTime(float timer)
    {
        yield return new WaitForSeconds(timer);
        DisableSelf();
    }
}