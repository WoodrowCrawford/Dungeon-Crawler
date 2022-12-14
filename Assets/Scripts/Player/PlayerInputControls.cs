using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerInputControls : MonoBehaviour
{
    //the rigidbody for the player
    private Rigidbody2D rb;

    //the animatior for the player
    public Animator animator;

    //the input actions 
    private PlayerInputActions inputActions;

    private PlayerCombatBehavior _combatBehavior;

    //speed for movement
    [SerializeField]
    private float _speed = 3f;

    //the current animation state for the player
    private string currentState;

    //Sets the animation states to strings
    const string WARRIOR_IDLE = "PlayerWarrior_Idle";
    const string WARRIOR_RUN = "PlayerWarrior_Move";
    const string WARRIOR_ATTACK = "HeroWarrior_Attack_part1";
   

    
    //Gets the components
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        _combatBehavior = GetComponent<PlayerCombatBehavior>();

   
        inputActions = new PlayerInputActions();
        inputActions.Player.Enable();

        //Subscribes to the events
        inputActions.Player.Move.performed += Move;
        inputActions.Player.Attack.performed += InputAttack;
    }

    

    private void Start()
    {
        ChangeAnimationState(WARRIOR_IDLE);
    }

    private void FixedUpdate()
    {
        Vector2 inputVector = inputActions.Player.Move.ReadValue<Vector2>();
        rb.velocity = inputVector * _speed;

        //Checks to see if the player is moving or not
        if(inputVector.x == 0f && inputVector.y == 0f)
        {
            animator.SetBool("isMoving", false);
            ChangeAnimationState(WARRIOR_IDLE);
        }
        else
        {
            animator.SetBool("isMoving", true);
            ChangeAnimationState(WARRIOR_RUN);
        }


        //if the vector is greater than 0 (moving forward)
        if (inputVector.x > 0f)
        {
            //make the character face right
            transform.localScale = new Vector2(5f, 5f);
        }
        //if the vector is less than 0 (moving backwards)
        else if(inputVector.x < 0f)
        {
            //make the character face left
            transform.localScale = new Vector2(-5f, 5f);
        }
    }


    public void ChangeAnimationState(string newState)
    {
        //stop the same animation from interrupting itself
        if (currentState == newState)
        {
            return;
        }

        //play the animation
        animator.Play(newState);

        //reassign the current state
        currentState = newState;
    }


    //How the player moves
    public void Move(InputAction.CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();
    }


    public void InputAttack(InputAction.CallbackContext context)
    {
        //Just plays the animation 
        animator.SetTrigger("Attack");
    }


}