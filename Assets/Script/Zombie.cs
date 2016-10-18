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

    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        target = GameObject.FindGameObjectWithTag("Player");

        navMesh.speed = Random.Range(1f, 2f);
    }

    void Update()
    {
        if (target != null)
        {
            navMesh.SetDestination(target.transform.position);
        }

        Animation();
    }

    void Animation()
    {
        float speed = Mathf.Clamp(navMesh.velocity.sqrMagnitude, 0f, 1f);

        //Idle or Run
        anim.SetFloat("Speed", speed);

        //Attack
        if (Vector3.Distance(this.transform.position, target.transform.position) < attackDist)
        {
            anim.SetTrigger("Attack");
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
