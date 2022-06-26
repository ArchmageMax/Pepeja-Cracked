using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTurning : MonoBehaviour
{
public Transform player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            Vector3 direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle -= 90;
            transform.rotation = Quaternion.Euler(0,0, angle);
        }
    }
}
