using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject bloodEffect;
    [SerializeField]
    private int hp = 10;
    [SerializeField]
    private int damageValue;
    [SerializeField]
    private float attackDist;
    [SerializeField]
    private float trackingDist;

    private GameObject target;
    private NavMeshAgent navMesh;
    private Animator anim;
    private float moveSpeed;
    private bool isAlive = true;

    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        target = GameObject.FindGameObjectWithTag("Player");

        moveSpeed = Random.Range(1f, 1.4f);
        navMesh.speed = moveSpeed;
    }

    void Update()
    {
        float distance = Vector3.Distance(
            new Vector3(this.transform.position.x, 0f, this.transform.position.z),
            new Vector3(target.transform.position.x, 0f, target.transform.position.z));

        if (target != null && isAlive)
        {
            if (trackingDist < distance)
            {
                navMesh.speed = moveSpeed;
                navMesh.SetDestination(target.transform.position);
            }
            else
            {
                navMesh.speed = 0f;
            }
        }

        float speed = Mathf.Clamp(navMesh.velocity.sqrMagnitude, 0f, 1f);

        //Idle or Run
        anim.SetFloat("Speed", speed);

        //Attack
        if (distance < attackDist)
        {
            anim.SetTrigger("Attack");
        }

        //Death
        if (hp <= 0 && isAlive)
        {
            anim.SetTrigger("Death");
            isAlive = false;
            navMesh.Stop();
            Score.numScore += 100;
            Destroy(this.gameObject, 2f);
        }
    }

    public void HitRaycast(RaycastHit hit)
    {
        GameObject blood = Instantiate(bloodEffect, hit.transform.position, bloodEffect.transform.rotation) as GameObject;
        blood.transform.SetParent(this.gameObject.transform);
        Destroy(blood.gameObject, 0.5f);

        if (isAlive)
        {
            //Hit
            anim.SetTrigger("Hit");
            hp -= damageValue;
        }
    }
}
