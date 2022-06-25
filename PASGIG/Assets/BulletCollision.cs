using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("We hit an enemy!");
        
        if(collision.gameObject.TryGetComponent<EnemyHP>(out EnemyHP enemyComponent))
        {
            enemyComponent.TakeDamage(50);
        }

        Destroy(gameObject);
    }
}
