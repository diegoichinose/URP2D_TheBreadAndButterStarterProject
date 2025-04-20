using UnityEngine;

public class ApplyMagnetTargetOnCollision : MonoBehaviour
{
    public string filterByTag;

    void OnTriggerStay2D(Collider2D other)
    {
        ApplyMagnet(other.gameObject);
    }

    private void ApplyMagnet(GameObject other)
    {
        if(!other.CompareTag(filterByTag))
            return;

        if(!other.TryGetComponent<MagnetMovement>(out MagnetMovement _magnet))
            return;

        _magnet.SetMagnetTarget(transform.parent.position);
    }
}
