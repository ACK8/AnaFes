using UnityEngine;
using System.Collections;

public class Cutlery : MonoBehaviour
{
	void Start ()
    {
	
	}
	
	void Update ()
    {
	
	}

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Axe");

        if(collision.gameObject.tag == "Zombie")
        {
            collision.gameObject.GetComponent<ChildeColliderTrigger>().HitCutlery(collision);
        }

        if (collision.gameObject.tag == "Ghoul")
        {
            collision.gameObject.GetComponent<ChildeColliderTrigger>().HitCutlery(collision);
        }
    }
}
