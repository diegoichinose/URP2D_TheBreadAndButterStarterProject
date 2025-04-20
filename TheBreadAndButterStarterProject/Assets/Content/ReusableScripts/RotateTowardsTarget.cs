using UnityEngine;

public class RotateTowardsTarget : MonoBehaviour
{
    public string targetTag;

    void OnTriggerStay2D(Collider2D other)
	{
		if (!other.CompareTag(targetTag))
            return;

        Rotate(other.transform.position);
	}

    public void Rotate(Vector3 targetPosition)
    {
        Vector3 aimDirection = (targetPosition - transform.position).normalized; 
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg; 
        transform.eulerAngles = new Vector3(0, 0, angle);
    } 
}
