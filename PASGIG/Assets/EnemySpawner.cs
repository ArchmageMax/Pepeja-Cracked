using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject kamikazePrefab;
    [SerializeField]
    private GameObject turretPrefab;

    [SerializeField]
    private float kamikazeInterval = 3.5f;
    [SerializeField]
    private float turretInterval = 10f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy(kamikazeInterval, kamikazePrefab));
        StartCoroutine(spawnEnemy(turretInterval, turretPrefab));
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-5f, 5f), Random.Range(-6f, 6f), 0), Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));
    }
}
