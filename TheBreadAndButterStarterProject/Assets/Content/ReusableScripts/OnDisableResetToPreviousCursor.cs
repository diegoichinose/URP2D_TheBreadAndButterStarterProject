using UnityEngine;

public class OnDisableResetToPreviousCursor : MonoBehaviour
{
    void OnDisable() => CursorManager.instance.ResetToPreviousCursor();
}