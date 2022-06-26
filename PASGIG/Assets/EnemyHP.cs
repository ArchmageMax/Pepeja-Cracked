using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public PlayerScore playerScore;

    //Enemy HP bar is currently jittery when enemy rotates, temporarily disabled
    //public EnemyHPBar healthBar;
    
    void Awake()
    {
        playerScore = GameObject.FindWithTag("Player").GetComponent<PlayerScore>();
    }

    void Start()
    {
        currentHealth = maxHealth;
        //healthBar.SetHealth(currentHealth, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        //healthBar.SetHealth(currentHealth, maxHealth);

        if(currentHealth <= 0)
        {
            EnemyDestroyed();
        }
    }

    void EnemyDestroyed()
    {
        playerScore.AddScore(maxHealth);
        Destroy(gameObject);
    }
}
