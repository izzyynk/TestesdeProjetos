using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveWithMouseDrag : MonoBehaviour
{
    private Camera mainCamera;
    private float CameraZDistance;
    //public GameObject currentObject;
    private bool isDragging = false;
    private Vector3 _initialPosition;
    private bool _connected;

    private const string _portTag = "Port";
    private  const float _dragResponseThreshold = 2;
    void Start()
    {
        mainCamera = Camera.main;
        CameraZDistance = mainCamera.WorldToScreenPoint(transform.position).z;
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
        Vector2 mousePosition = context.ReadValue<Vector2>();
        Vector3 ScreenPosition = new Vector3(mousePosition.x, mousePosition.y, CameraZDistance);
        Vector3 NewWorldPosition = mainCamera.ScreenToWorldPoint(ScreenPosition);
        if (isDragging)
        {
            _connected = true;
            transform.position = NewWorldPosition;
        }
        else if(Vector3.Distance(transform.position, NewWorldPosition)> _dragResponseThreshold) 
        {
            _connected = false;
        }


    }
       
    private void OnMouseUp()
    {
        if(_connected)
        {
            ResetPosition();
            transform.hasChanged = false;
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void SetInitialPosition(Vector3 NewPosition)
    {
        _initialPosition = NewPosition;
        transform.position = _initialPosition;
    }

    private void ResetPosition()
    {
        transform.position = _initialPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        _connected = true;
        transform.position = other.transform.position;
    }
}
