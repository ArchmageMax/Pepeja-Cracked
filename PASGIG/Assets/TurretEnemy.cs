using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform muzzle1;
    public Transform muzzle2;

    float fireRate;
    float nextFire;
    // Start is called before the first frame update
    void Start()
    {
        fireRate = 1f;
        nextFire = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfTimeToFire();
    }

    void CheckIfTimeToFire()
    {
        if(Time.time > nextFire) 
        {
            Instantiate(bulletPrefab, muzzle1.position, transform.rotation);
            Instantiate(bulletPrefab, muzzle2.position, transform.rotation);
            nextFire = Time.time + fireRate;
        }
    }
}
