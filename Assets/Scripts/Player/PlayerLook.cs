using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField, Range(0.3f, 4f)] private float _mouseSensivity;
    private Camera _camera;
    private Transform _transform;
    private float _xRotation;

    private void Awake()
    {
        _camera = Camera.main;
        Cursor.visible = false;
    }

    public void ProcessLook(Vector2 input)
    {
        input *= _mouseSensivity / 10f;
        Vector3 angles = transform.rotation.eulerAngles;
        
        transform.rotation = Quaternion.Euler(angles.x, angles.y + input.x, angles.z);
        _camera.transform.rotation = Quaternion.Euler(_camera.transform.rotation.eulerAngles + new Vector3(-input.y, 0f, 0f));

    }
}
