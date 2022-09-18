using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem;



public class PlayerMovementBehavior : MonoBehaviour
{
    //the rigidbody for the player
    private Rigidbody2D rb;
    
    //the input actions 
    private PlayerInputActions inputActions;

    [SerializeField]
    private float _speed = 3f;

    //Gets the components
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
   
        inputActions = new PlayerInputActions();
        inputActions.Player.Enable();


        //Subscribes to the move event
        inputActions.Player.Move.performed += Move;
    }


    private void FixedUpdate()
    {
        Vector2 inputVector = inputActions.Player.Move.ReadValue<Vector2>();
        rb.velocity = inputVector * _speed;

        //if the vector is greater than 0 (moving forward)
        if (inputVector.x > 0f)
        {
            //make the character face right
            transform.localScale = new Vector2(1f, 1f);
        }
        //if the vector is less than 0 (moving backwards)
        else if(inputVector.x < 0f)
        {
            //make the character face left
            transform.localScale = new Vector2(-1f, 1f);
        }
    }

    //How the player moves
    public void Move(InputAction.CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();
    }

}