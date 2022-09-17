using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem;
public class PlayerMovementBehavior : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private PlayerInputActions inputActions;

    [SerializeField]
    private float _speed = 3f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        inputActions = new PlayerInputActions();
        inputActions.Player.Enable();

        //Subscribes to the move event
        inputActions.Player.Move.performed += Move;
    }


 
    private void FixedUpdate()
    {
        Vector2 inputVector = inputActions.Player.Move.ReadValue<Vector2>();
        rb.velocity = inputVector * _speed;


        
       
    }

    //How the player moves
    public void Move(InputAction.CallbackContext context)
    {

       // Debug.Log(context.ReadValue<Vector2>());
        Vector2 inputVector = context.ReadValue<Vector2>();
        

        // rb.AddForce(new Vector2(inputVector.x, inputVector.y) * speed, ForceMode2D.Force);
    }

}