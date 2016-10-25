using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : SingletonMonoBehaviour<EnemyManager>
{
    public List<GameObject> spawnEnemyList;

    public void DestroyEnemys()
    {
        foreach (GameObject enemy in spawnEnemyList)
        {
            Destroy(enemy.gameObject);
        }
        spawnEnemyList.Clear();
        spawnEnemyList = new List<GameObject>();



    }
}
