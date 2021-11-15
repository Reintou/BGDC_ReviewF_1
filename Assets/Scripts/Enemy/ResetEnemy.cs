using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetEnemy : MonoBehaviour
{
    public EnemySpawner[] enemySpawner = new EnemySpawner[0];
    
    public void StartReset()
    {
        for(int i = 0; i < enemySpawner.Length; i++)
        {
            enemySpawner[i].ResetSpawn();
        }
    }
}
