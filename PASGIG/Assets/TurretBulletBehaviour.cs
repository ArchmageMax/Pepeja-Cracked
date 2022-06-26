using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBulletBehaviour : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;
    public float maxLifetime = 2.0f;
    // Update is called once per frame
    void Start ()
    {
        rb.AddForce(transform.up * speed, ForceMode2D.Impulse);
        Debug.Log(rb.velocity.magnitude);
    }

    private void Update()
    {
        maxLifetime -= Time.deltaTime;
        if (maxLifetime <= 0) Explode();
    }

    private void Explode () 
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enemy has hit you!");
            Explode();
        }

        if(collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Friendly fire by enemy!");
            Explode();
        }
    }
}
