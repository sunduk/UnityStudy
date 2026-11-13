
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMouseLook : MonoBehaviour
{
    [SerializeField] private Transform playerCamera;
    [SerializeField] private float mouseSensitivity = 0.15f;

    private float pitch = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Mouse.current == null)
            return;

        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        float mouseX = mouseDelta.x * mouseSensitivity;
        float mouseY = mouseDelta.y * mouseSensitivity;

        // 좌우 회전 (Player)
        transform.Rotate(Vector3.up * mouseX);

        // 상하 회전 (Camera)
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -80f, 80f);

        playerCamera.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }
}
