using UnityEngine;

public class DeleteChildrensOnDisable : MonoBehaviour
{
    void OnDisable()
    {
        gameObject.DeleteAllChildren();
    }
}