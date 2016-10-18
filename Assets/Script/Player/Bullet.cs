using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float survivalDistance = 10.0f;
    public float speed = 1.0f;
    public Vector3 originPos;
    public Vector3 direction;
    public GameObject hitEffect;

    void Start ()
    {
        originPos = transform.position;
    }
	
	void Update ()
    {
        transform.position += direction * speed * Time.deltaTime;

        if(survivalDistance < Vector3.Distance(transform.position, originPos))
        {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        print(col.transform.position);
        Instantiate(hitEffect, transform.position, transform.rotation);

        Destroy(this.gameObject);
    }
}
