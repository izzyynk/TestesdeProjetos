using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoviment : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 30f;


    Vector3 velocity;


    private PlayerInput playerInput;
    private InputAction moveAction;


    private Vector2 moveInput;

    //public AudioSystem audioSystem;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        moveAction = playerInput.actions["Move"];


        moveAction.performed += OnMovePerformed;
        moveAction.canceled += OnMoveCanceled;

    }

    private void OnDisable()
    {
        moveAction.performed -= OnMovePerformed;
        moveAction.canceled -= OnMoveCanceled;

    }

    void Update()
    {
        //isGrounded = Physics.CheckSphere(groundCheck.position, groundDistence, groundMask);

        if (velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;

        controller.Move(move * speed * Time.deltaTime);

        controller.Move(velocity * Time.deltaTime);

        // if (audioSystem != null)
        // {
        //     audioSystem.andando = moveInput.magnitude > 0.1f && isGrounded;
        // }
    }

    public void OnMovePerformed(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnMoveCanceled(InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;
    }

    public bool IsMoving()
    {
        return moveInput.magnitude > 0.1f;
    }

}