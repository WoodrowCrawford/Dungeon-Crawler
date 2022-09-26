using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombatBehavior : MonoBehaviour
{
    [SerializeField]
    private Transform _attackPoint;

    [SerializeField]
    private float _attackRange; 

    [SerializeField]
    private float _damage;

    [SerializeField]
    private LayerMask _enemyLayer;


    public void Attack()
    {
      
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enemyLayer);

        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("we hit " + enemy.name + " dealing " + _damage + " damage!");
            enemy.GetComponent<EnemyBehavior>().TakeDamage();
           
        }
           
    }


    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);

    }
}
