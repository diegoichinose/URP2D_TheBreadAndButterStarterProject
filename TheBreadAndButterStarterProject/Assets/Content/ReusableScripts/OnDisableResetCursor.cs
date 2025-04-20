using UnityEngine;

public class OnDisableResetCursor : MonoBehaviour
{
    void OnDisable() => Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
}