using UnityEngine;
using System.Collections;

public class Cutlery : MonoBehaviour
{
    BoxCollider boxCollider;

	void Start ()
    {
        boxCollider = GetComponent<BoxCollider>();
	}
	
	void Update ()
    {
	
	}

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Zombie")
        {
            collision.gameObject.GetComponent<ChildeColliderTrigger>().HitCutlery(collision, 1);
        }

        if (collision.gameObject.tag == "ZombieHead")
        {
            collision.gameObject.GetComponent<ChildeColliderTrigger>().HitCutlery(collision, 3);
        }

        if (collision.gameObject.tag == "Ghoul")
        {
            collision.gameObject.GetComponent<ChildeColliderTrigger>().HitCutlery(collision, 1);
        }

        if (collision.gameObject.tag == "GhoulHead")
        {
            collision.gameObject.GetComponent<ChildeColliderTrigger>().HitCutlery(collision, 3);
        }
    }

    public void Attack()
    {
        boxCollider.enabled = true;
    }

    public void NoAttack()
    {
        boxCollider.enabled = false;
    }
}
