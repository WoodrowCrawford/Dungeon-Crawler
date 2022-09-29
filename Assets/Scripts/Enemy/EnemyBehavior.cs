using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug;


public class EnemyBehavior : MonoBehaviour
{
    public enum EnemyState
    {
        IDLE,
        WANDER,
        PATROL,
        CHASE
    }

    [Header("Enemy Name")]
    [SerializeField] private string _name;

    SpriteRenderer _enemySprite;


    [Header("EnemyStats")]
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _currentHealth;
    [SerializeField] private float _speed;
    [SerializeField] private int _attackDamage;


    [Header("Current Enemy State")]
    [SerializeField] private EnemyState _currentState;
  

    [Header("Chasing State Variables")]
    [SerializeField] private Transform _target;
    [SerializeField] private float _dectectionRange;


    [Header("Patrol State Variables")]
    [SerializeField] private Transform[] _moveSpots;
    [SerializeField] private int _randomSpot;
    [SerializeField] private float _waitTime;
    [SerializeField] private float _startWaitTime;

    [Header("Wander State Variables")]
    [SerializeField] private Transform _moveSpot;
    [SerializeField] private float _minX;
    [SerializeField] private float _minY;
    [SerializeField] private float _maxX;
    [SerializeField] private float _maxY;


    private void Awake()
    {
        _enemySprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
        _waitTime = _startWaitTime;
    }


    private void Update()
    {
        switch (_currentState)
        {
            //The idle state for the enemy
            case EnemyState.IDLE:
                
                {
                    //If the player is in range of the enemy
                    if(Vector2.Distance(transform.position, _target.position) < _dectectionRange)
                    {
                        Debug.Log("Player is in range");

                        //Change to chase state
                        _currentState = EnemyState.CHASE;
                    }                 
                    break;
                }

                //The enemy wanders randomly in this state
            case EnemyState.WANDER:
                {
                    transform.position = Vector2.MoveTowards(transform.position, _moveSpot.position, _speed * Time.deltaTime);


                    //If the player is in range of the enemy
                    if (Vector2.Distance(transform.position, _target.position) < _dectectionRange)
                    {
                        Debug.Log("Player is in range");

                        //Change to chase state
                        _currentState = EnemyState.CHASE;

                        //Change enemy color
                        _enemySprite.color = Color.red;
                    }






                    //Debug purposes
                    if (Keyboard.current.digit1Key.wasPressedThisFrame)
                    {
                        _currentState = EnemyState.PATROL;
                    }




                    if (Vector2.Distance(transform.position, _moveSpot.position) < 0.2f)
                    {
                        if (_waitTime <= 0)
                        {
                            _moveSpot.position = new Vector2(UnityEngine.Random.Range(_minX, _maxX), UnityEngine.Random.Range(_minY, _maxY));
                            _waitTime = _startWaitTime;
                        }
                        else
                        {
                            _waitTime -= Time.deltaTime;
                        }


                    }





                    break;
                }

               
                //This is when the enemy wants to patrol an area 
            case EnemyState.PATROL:
                {
                   
                    transform.position = Vector2.MoveTowards(transform.position, _moveSpots[_randomSpot].position, _speed * Time.deltaTime);


                    //If the player is in range of the enemy
                    if (Vector2.Distance(transform.position, _target.position) < _dectectionRange)
                    {
                        Debug.Log("Player is in range");

                        //Change to chase state
                        _currentState = EnemyState.CHASE;
                    }



                    if (Vector2.Distance(transform.position, _moveSpots[_randomSpot].position) < 0.2f)
                    {
                        if (_waitTime <= 0)
                        {
                            _randomSpot = UnityEngine.Random.Range(0, _moveSpots.Length);
                            _waitTime = _startWaitTime;
                        }
                        else
                        {
                            _waitTime -= Time.deltaTime;
                        }


                    }

                    //Debug purposes
                    if (Keyboard.current.digit1Key.wasPressedThisFrame)
                    {
                        _currentState = EnemyState.CHASE;
                    }


                    break;
                }

                //This is when the enemy wants to chase the player
            case EnemyState.CHASE:
                {

                   
                    //Stop moving towards the player if it reaches a certain distance
                    if (Vector2.Distance(transform.position, _target.position) > 1.2f)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
                    }


                    if (Vector2.Distance(transform.position, _target.position) > _dectectionRange)
                    {
                        Debug.Log("Player is not in range");
                        _currentState = EnemyState.PATROL;
                    }

                    //Debug purposes
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


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _dectectionRange);
        Gizmos.color = Color.red;
    }


   
}
