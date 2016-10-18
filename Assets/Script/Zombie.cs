using UnityEngine;
using System.Collections;

public class Zombie : MonoBehaviour
{
    [SerializeField]
    private int hp = 10;
    [SerializeField]
    private float attackDist;
    [SerializeField]
    private int damageValue;

    private GameObject target;
    private NavMeshAgent navMesh;
    private Animator anim;
	private bool isAlive = true;

    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        target = GameObject.FindGameObjectWithTag("Player");

        navMesh.speed = Random.Range(1f, 2f);
    }

    void Update()
	{
		float distance = Vector3.Distance (this.transform.position, target.transform.position);

		if (target != null && isAlive)
		{
			navMesh.SetDestination(target.transform.position);
		}

		float speed = Mathf.Clamp(navMesh.velocity.sqrMagnitude, 0f, 1f);

		//Idle or Run
		anim.SetFloat("Speed", speed);

		//Attack
		if (distance < attackDist) 
		{
			anim.SetTrigger ("Attack");
		} 

		if (!isAlive) 
		{
			Destroy (this.gameObject, 1.5f);
		}
    }

    //子のChildeColliderTriggerから呼ばれる
    public void RelayOnTriggerEnter(Collision col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            if (hp <= 0)
            {
                //Death
				anim.SetTrigger("Death");
				isAlive = false;
				navMesh.Stop ();
            }
            else
            {
                //Hit
				anim.SetTrigger("Hit");
                hp -= damageValue;
            }
        }
    }


}
