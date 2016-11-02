using UnityEngine;
using System.Collections;

public class EnemyDeathSound : MonoBehaviour
{
    private AudioSource SEsource;
    
    void Start()
    {
        SEsource = GetComponent<AudioSource>();
    }

	void Update ()
    {
        if (!SEsource.isPlaying)
            Destroy(this.gameObject);
	}
}
