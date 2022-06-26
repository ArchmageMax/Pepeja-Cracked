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
            DoDamageToPlayer(10);
        }

        if(collision.gameObject.CompareTag("Missile"))
        {
            DoDamageToPlayer(2);
        }
    }

    void DoDamageToPlayer(int damagetaken)
    {
        Debug.Log("Took Damage!");
        playerHP.TakeDamage(damagetaken);
    }
}
