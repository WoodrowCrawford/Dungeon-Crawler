using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
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
    private int _MaxHealth;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _attackDamage;


    public void TakeDamage()
    {
        Debug.Log("I have been hit! ow");
    }
}
