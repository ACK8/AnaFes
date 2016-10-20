using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject zombie;
    [SerializeField]
    private Transform[] spawnPoint;
    [SerializeField]
    private int waveNum;
	[SerializeField]
	private float spawnWait;


    void Start()
    {
		StartCoroutine(Spawn(spawnWait));
    }

    void Update()
    {

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
