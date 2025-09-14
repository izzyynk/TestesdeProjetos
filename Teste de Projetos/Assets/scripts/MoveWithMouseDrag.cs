using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveWithMouseDrag : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction mouseDragAction;
    private Camera mainCamera;
    private float CameraZDistance;
    
    private bool isDragging = false;
    

    void Start()
    {
        mainCamera = Camera.main;
        CameraZDistance = mainCamera.WorldToScreenPoint(transform.position).z;
    }

    private void OnMouseUp()
    {
        transform.hasChanged = false;
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.transform == transform)
        {
            if (context.performed)
            {
                isDragging = true;
            }
            else if (context.canceled)
            {
                isDragging = false;
            }
        }
    }

    public void OnMouseDragScale(InputAction.CallbackContext context)
    {
        if (isDragging)
        {
            Vector2 mousePosition = context.ReadValue<Vector2>();
            Vector3 ScreenPosition = new Vector3(mousePosition.x, mousePosition.y, CameraZDistance);
            Vector3 NewWorldPosition = mainCamera.ScreenToWorldPoint(ScreenPosition);
            transform.position = NewWorldPosition;
        }


    }
       

}
