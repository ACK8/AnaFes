using UnityEngine;
using System.Collections;

public class ZombieSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject zombie;
    [SerializeField]
    private int waveNum;



    void Start()
    {
        StartCoroutine(Spawn(2f));
    }

    void Update()
    {

    }

    private IEnumerator Spawn(float second)
    {
        while (true)
        {
            yield return new WaitForSeconds(second);
            Instantiate(zombie, transform.position, Quaternion.identity);
        }
    }
}
