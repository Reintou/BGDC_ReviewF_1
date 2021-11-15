using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(enemyPrefab, transform.position, Quaternion.identity, transform);
    }

    public void ResetSpawn()
    {
        if(transform.childCount == 0f)
        {
            Instantiate(enemyPrefab, transform.position, Quaternion.identity, transform);
        }
    }
}
