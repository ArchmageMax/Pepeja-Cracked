using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCollision : MonoBehaviour
{
    public EnemyHP enemyHP;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            SelfDestruct();
        }

        if(collision.gameObject.CompareTag("Bullet"))
        {
            DoDamageToEnemy(50);
        }
    }

    void SelfDestruct()
    {
        Destroy(gameObject);
    }

    void DoDamageToEnemy(int damagetaken)
    {
        if(enemyHP!=null)
        {
        Debug.Log("Enemy took damage!");
        enemyHP.TakeDamage(damagetaken);
        }
    }
}
