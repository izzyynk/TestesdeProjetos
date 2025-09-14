using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMove : MonoBehaviour
{
    public float mouseSensitivity = 20f;
    public float joystickSensitivity = 20f;
    public Transform playerBody;
    float xRotation = 0f;
    private Vector2 cameraValue;

    private PlayerInput playerInput;
    private InputAction cameraAction;

    public PlayerMoviment playerMoviment;
    public float headBobFrequency = 1.5f;
    public float headBobAmplitude = 0.05f;
    private Vector3 initialCameraPosition;

    void Awake()
    {
        playerInput = GetComponentInParent<PlayerInput>();
    }

    private void OnEnable()
    {
        cameraAction = playerInput.actions["Look"];

        cameraAction.performed += OnCameraMovePerformed;
        cameraAction.canceled += OnCameraMovePerformed;
    }

    private void OnDisable()
    {
        cameraAction.performed -= OnCameraMovePerformed;
        cameraAction.canceled -= OnCameraMovePerformed;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        initialCameraPosition = transform.localPosition;
    }

    void Update()
    {
        if (Application.isMobilePlatform)
        {
            float joystickX = cameraValue.x * joystickSensitivity * Time.deltaTime;
            float joystickY = cameraValue.y * joystickSensitivity * Time.deltaTime;
            xRotation -= joystickY;
            xRotation = Mathf.Clamp(xRotation, -45f, 45f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * joystickX);
        }
        else
        {
            float mouseX = cameraValue.x * mouseSensitivity * Time.deltaTime;
            float mouseY = cameraValue.y * mouseSensitivity * Time.deltaTime;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -75f, 75f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }

        if (playerMoviment.IsMoving())
        {
            float bobbingAmount = Mathf.Sin(Time.time * headBobFrequency) * headBobAmplitude;
            Vector3 newPosition = transform.localPosition;
            newPosition.y = initialCameraPosition.y + bobbingAmount;
            transform.localPosition = newPosition;
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, initialCameraPosition, Time.deltaTime * 5f);
        }

    }

    public void OnCameraMovePerformed(InputAction.CallbackContext context)
    {
        cameraValue = context.ReadValue<Vector2>();
    }
}