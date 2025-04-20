using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    private GameInput _input;
    private Camera _camera;
    
    void OnEnable()
    {
        _camera = Camera.main;
        _input = new GameInput();
        _input.Enable();
    }

    void OnDisable()
    {
        _input.Disable();
        _camera = null;
    }

    void Update()
    {
        if (_camera == null)
            return;

        transform.position = _camera.ScreenToWorldPoint(_input.Gameplay.RightStick.ReadValue<Vector2>());
    }
}