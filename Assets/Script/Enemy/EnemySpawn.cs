﻿using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemyObject;
    [SerializeField]
    private Transform[] spawnPoint;
    [SerializeField]
    private int waveNum = 0;
    [SerializeField]
    private float[] waveSwitchingTime;
    [SerializeField]
    private float[] spawnWait;
    [SerializeField]
    private float spawnWaitOld;


    private float time;

    void Start()
    {

        //StartCoroutine(Spawn(spawnWait[waveNum]));

        //spawnWaitOld = spawnWait[waveNum];
    }

    void Update()
    {
        if (!GameManager.Instance.isGamePlaying || GameManager.Instance.isTimeOver) return;

        time += Time.deltaTime;
                
        //Debug.Log(Time.realtimeSinceStartup);

        if (waveSwitchingTime.Length > waveNum)
        {
            if (time > waveSwitchingTime[waveNum])
            {
                waveNum += 1;
                Debug.Log("waveCount");
            }
        }

        if (waveNum >= waveSwitchingTime.Length)
        {
            waveNum = waveSwitchingTime.Length - 1;
        }

        if (time > spawnWait[waveNum] + spawnWaitOld)
        {
            int enemyType = Random.Range(0, enemyObject.Length);

            EnemyManager.Instance.spawnEnemyList.Add(
                Instantiate(enemyObject[enemyType],
                spawnPoint[Random.Range(0, spawnPoint.Length)].transform.position,
                Quaternion.identity) as GameObject);

            spawnWaitOld += spawnWait[waveNum];
        }

    }
}
