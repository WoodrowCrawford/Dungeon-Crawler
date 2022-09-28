using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    Transform pathPoint1;

    [SerializeField]
    Transform pathPoint2;

    [SerializeField]
    private string _name;

    [SerializeField]
    private int _maxHealth = 100;

    [SerializeField]
    private int _currentHealth;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _attackDamage;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }


    public void TakeDamage(int damage)
    {
        Debug.Log("I have been hit! ow");
        _currentHealth -= damage;

        //Play hurt animation

        if(_currentHealth <= 0)
        {
            Die();
        }
       
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}
