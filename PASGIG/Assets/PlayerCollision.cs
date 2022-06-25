using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public PlayerHP playerHP;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            DoColDamageToPlayer();
        }
    }

    void DoColDamageToPlayer()
    {
        Debug.Log("Took Damage!");
        playerHP.TakeDamage(10);
    }
}
