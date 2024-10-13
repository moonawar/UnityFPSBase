using Cinemachine;
using UnityEngine;

public class FPCamera : MonoBehaviour
{
    public Transform playerBody;
    private float xRotation = 0f;
    private CinemachineVirtualCamera vcam;

    [SerializeField] private RangeFloat clamp = new(-75f, 75f);

    private void Awake() {
        vcam = GetComponent<CinemachineVirtualCamera>();
    }

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate() {
        Vector2 mouseInput = InputContainer.Instance.Controls.Gameplay.Look.ReadValue<Vector2>();

        float mouseX = mouseInput.x * Time.deltaTime * Setting.Instance.MouseSensitivity;
        float mouseY = mouseInput.y * Time.deltaTime * Setting.Instance.MouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, clamp.min, clamp.max);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}