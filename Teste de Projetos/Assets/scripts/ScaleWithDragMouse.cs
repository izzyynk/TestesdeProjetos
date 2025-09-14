using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider))]
public class ScaleWithDragMouse : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction mouseDragAction;

    public Transform WorldArchor;

    private float CameraZDistance;
    private Camera mainCamera;
    private Vector3 InitialScale;
    public bool canDrag = false;

    /*void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    void OnEnable()
    {
        mouseDragAction = playerInput.actions["Wiring"];
        mouseDragAction.performed += OnMouseDragPerformed;
    }

    void OnDisable()
    {
        mouseDragAction.performed -= OnMouseDragPerformed;
    }*/

    private void Start()
    {
        InitialScale = transform.localScale;
        mainCamera = Camera.main;
        CameraZDistance = mainCamera.WorldToScreenPoint(transform.position).z;
    } 
    
    public void EnableDrag(bool state)
    {
        canDrag = state;
    }

    private void OnMouseDrag()
    {
        if (!canDrag) return; // só deixa arrastar se tiver ativado
        //pega a posiçao do mouse no espaço 
        Vector3 MouseScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, CameraZDistance);
        Vector3 MouseWorldPosition = mainCamera.ScreenToWorldPoint(MouseScreenPosition);

        /*MUDAR O LOCAL DO CODIGO A SEGUIR PARA FORA DO ONMOUSEDRAG*/
        // muda o transforM
        //muda a escala
        float distance = Vector3.Distance(WorldArchor.position, MouseWorldPosition);
        transform.localScale = new Vector3(InitialScale.x, distance / 2, InitialScale.z);

        Vector3 middlePoint = (WorldArchor.position + MouseWorldPosition) / 2f;
        transform.position = middlePoint;

        Vector3 rotationDirection = (MouseWorldPosition - WorldArchor.position);
        transform.up = rotationDirection;
    }
}
