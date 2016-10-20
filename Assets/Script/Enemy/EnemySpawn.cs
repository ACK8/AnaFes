using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject zombie;
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


    void Start()
    {

        //StartCoroutine(Spawn(spawnWait[waveNum]));

        //spawnWaitOld = spawnWait[waveNum];
    }

    void Update()
    {
        Debug.Log(Time.realtimeSinceStartup);

        if (Time.realtimeSinceStartup > waveSwitchingTime[waveNum])
        {
            waveNum += 1;
        }

        if (Time.realtimeSinceStartup > spawnWait[waveNum] + spawnWaitOld)
        {
            Instantiate(zombie, spawnPoint[Random.Range(0, spawnPoint.Length)].transform.position, Quaternion.identity);
            spawnWaitOld += spawnWait[waveNum];
        }

    }

    private IEnumerator Spawn(float second)
    {
        while (true)
        {
            yield return new WaitForSeconds(second);
            Instantiate(zombie, spawnPoint[Random.Range(0, spawnPoint.Length)].transform.position, Quaternion.identity);
        }
    }
}
