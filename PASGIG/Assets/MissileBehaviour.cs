using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBehaviour : MonoBehaviour
{
    public Transform player;
    public float speed = 5f;
    public Rigidbody2D rb;
    public float maxLifetime = 2.0f;
    private Vector2 movement;
    
    void Start ()
    {
        rb.AddForce(transform.right * speed, ForceMode2D.Impulse);
        player = GameObject.FindWithTag("Player").transform;
        Debug.Log(rb.velocity.magnitude);
    }

    // Update is called once per frame
    private void Update()
    {
        maxLifetime -= Time.deltaTime;
        if (maxLifetime <= 0) Explode();

        if(player != null)
        {
            Vector3 direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
            direction.Normalize();
            movement = direction;
        }
    }

    private void Explode () 
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("An enemy hit you!");
        
        if(collision.gameObject.CompareTag("Player"))
        {
            Explode();
        }
    }

    private void FixedUpdate() 
    {
        if(player!=null)
        {
            moveMissile(movement);
        }
    }
    
    void moveMissile(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
    }
}
