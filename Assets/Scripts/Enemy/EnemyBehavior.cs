using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;


public class EnemyBehavior : MonoBehaviour
{
    //Enemy AI states
    private enum AIState
    {
        Idle,
        Wander,
        Patrol,
        Chase,
        Attack
    }


    [SerializeField]
    private Rigidbody2D _rb2d;

    [SerializeField]
    private string _name;

    [SerializeField]
    private int _maxHealth = 100;

    [SerializeField]
    private int _currentHealth;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private int _attackDamage;

    [SerializeField]
    private AIState _currentState;

    [SerializeField]
    private Transform[] _moveSpots;

    private int _randomSpot;



    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
        _currentState = AIState.Idle;
        
        _randomSpot = UnityEngine.Random.Range(0, _moveSpots.Length);
    }


    private void Update()
    {
        //Get distance to player. if close then do 
        
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

    public void Patrol()
    {
        _currentState = AIState.Patrol;
        Debug.Log(_currentState.ToString());

        //Patrol code goes here
        transform.position = Vector2.MoveTowards(transform.position, _moveSpots[_randomSpot].position, _speed * Time.deltaTime);

        if(Vector2.Distance(transform.position, _moveSpots[_randomSpot].position) < 0.2f)
        {
            _randomSpot = UnityEngine.Random.Range(0, _moveSpots.Length);
        }

    }


    public void Wander()
    {
        _currentState = AIState.Wander;

        //Wander code goes here
    }



}
