using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{

    public MoveWithMouseDrag draggableObject;

    public void OnMouseDragEvent(InputAction.CallbackContext context)
    {
        if (draggableObject != null)
        {
            draggableObject.OnMouseDragScale(context);

        }
    }

    public void OnClickEvent(InputAction.CallbackContext context)
    {
        if (draggableObject != null)
        {
            draggableObject.OnClick(context);
        }
    }
    
}
