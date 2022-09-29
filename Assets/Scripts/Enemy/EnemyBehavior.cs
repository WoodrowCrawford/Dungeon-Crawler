using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyBehavior : MonoBehaviour
{
    //The enemy behavior states
    private enum EnemyStates
    {
        Idle,
        Patroling,
        Chasing,
        Attacking
    }


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
    private EnemyStates _currentState;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }


    private void Update()
    {
        Debug.Log(_currentState.ToString());
        ChangeEnemyState();


        //Check to see if player is in range of enemy
       

        //If so then change enemy state to chasing

        //If player is closer to enemy then switch to attack
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

    public void ChangeEnemyState()
    {

        if(Keyboard.current.uKey.wasPressedThisFrame)
        {
            _currentState = EnemyStates.Attacking;
        }
    }


    public void Die()
    {
        gameObject.SetActive(false);
       
    }
}
