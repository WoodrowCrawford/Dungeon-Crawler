using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class EnemyBehavior : MonoBehaviour
{
    public enum EnemyState
    {
        IDLE,
        WANDER,
        PATROL,
        CHASE
    }

    [SerializeField]
    private Rigidbody2D _rb2d;

    [SerializeField]
    [Header("Enemy Name")]
    private string _name;


    [Header("EnemyStats")]
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _currentHealth;
    [SerializeField] private float _speed;
    [SerializeField] private int _attackDamage;


    [Header("Current Enemy State")]
    [SerializeField]
    private EnemyState _currentState;



    [Header("Chasing State Variables")]
    [SerializeField] private Transform[] _moveSpots;
    



    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
       
    }


    private void Update()
    {
        //Tests to see what the current state is
        Debug.Log(_currentState.ToString());

        //A state machine used to switch between AI states
        switch (_currentState)
        {
            case EnemyState.IDLE:
                {
                   if(Keyboard.current.digit1Key.wasPressedThisFrame)
                    {
                        _currentState = EnemyState.WANDER;
                    }

                    break;
                }

            case EnemyState.WANDER:
                {

                    if (Keyboard.current.digit1Key.wasPressedThisFrame)
                    {
                        _currentState = EnemyState.PATROL;
                    }



                    break;
                }


            case EnemyState.PATROL:
                {

                    if (Keyboard.current.digit1Key.wasPressedThisFrame)
                    {
                        _currentState = EnemyState.CHASE;
                    }



                    break;
                }

            case EnemyState.CHASE:
                {
                    if (Keyboard.current.digit1Key.wasPressedThisFrame)
                    {
                        _currentState = EnemyState.IDLE;
                    }

                    break;
                }
        }
    }

 
    public void TakeDamage(int damage)
    {
        Debug.Log("Player has hit " + _name + " dealing " + damage + " damage!");
        _currentHealth -= damage;

        //Play hurt animation

        if(_currentHealth <= 0)
        {
            Die();
        }
    }

    public void Attack(int enemyDamage)
    {
        enemyDamage = _attackDamage;
    }

   

    public void Die()
    {
        gameObject.SetActive(false);
       
    }

  
}
