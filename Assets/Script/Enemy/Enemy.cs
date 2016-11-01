using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject bloodEffect;
    [SerializeField]
    private int hp = 10;
    [SerializeField]
    private int damageValue;    //ダメージ値
    [SerializeField]
    private float attackAnimRate;   //攻撃が有効になるアニメーション時間
    [SerializeField]
    private float attackDist;   //攻撃を始めるプレイヤーとの距離
    [SerializeField]
    private float trackingDist; //追跡をやめるプレイヤーとの距離

    private GameObject target;
    private CapsuleCollider handCollider;
    private AudioSource enemyAudioSource;
    private NavMeshAgent navMesh;
    private Animator anim;
    private float moveSpeed;
    private bool isAlive = true;
    private bool isAttack = false;
    private bool isHeadDeath = false;

    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        handCollider = GetComponent<CapsuleCollider>();
        enemyAudioSource = GetComponent<AudioSource>();
        target = GameObject.FindGameObjectWithTag("Player");

        moveSpeed = Random.Range(1f, 1.4f);
        navMesh.speed = moveSpeed;
        handCollider.enabled = false;
    }

    void Update()
    {
        if (!GameManager.Instance.isGamePlaying || GameManager.Instance.isTimeOver)
        {
            navMesh.speed = 0f;
            anim.SetFloat("Speed", 0);
            return;
        }

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

        handCollider.enabled = false;

        //Attack
        if (distance < attackDist)
        {
            anim.Update(0);
            AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

            if (stateInfo.IsName("Base Layer.Attack"))
            {
                if (attackAnimRate <= stateInfo.normalizedTime && stateInfo.normalizedTime < (attackAnimRate + 0.01))
                {
                    handCollider.enabled = true;
                }
                else
                {
                    handCollider.enabled = false;
                }
            }
        }

        //****  Animation  ****//

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
            if(isHeadDeath) Score.numScore += 500;
            else Score.numScore += 100;
            Destroy(this.gameObject, 2f);
        }
    }

    public void HitRaycast(RaycastHit hit, int damageMagnification)
    {
        GameObject blood = Instantiate(bloodEffect, hit.transform.position, bloodEffect.transform.rotation) as GameObject;
        blood.transform.SetParent(this.gameObject.transform);
        Destroy(blood.gameObject, 0.5f);

        if (isAlive)
        {
            //Hit
            anim.SetTrigger("Hit");
            hp -= damageValue * damageMagnification;

            if(damageMagnification >= 3) isHeadDeath = true;

            int rand = Random.Range(0, 3);
            if (rand == 0)
                AudioManager.Instance.PlaySE("Damage_1", enemyAudioSource);
            else if (rand == 1)
                AudioManager.Instance.PlaySE("Damage_2", enemyAudioSource);
            else
                AudioManager.Instance.PlaySE("Blood", enemyAudioSource);
        }
    }

    public void HitCutlery(Collision collision, int damageMagnification)
    {
        GameObject blood = Instantiate(bloodEffect, collision.transform.position, bloodEffect.transform.rotation) as GameObject;
        blood.transform.SetParent(this.gameObject.transform);
        Destroy(blood.gameObject, 0.5f);

        if (isAlive)
        {
            //Hit
            anim.SetTrigger("Hit");
            hp -= damageValue * damageMagnification;

            int rand = Random.Range(0, 3);
            if (rand == 0)
                AudioManager.Instance.PlaySE("Damage_1", enemyAudioSource);
            else if (rand == 1)
                AudioManager.Instance.PlaySE("Damage_2", enemyAudioSource);
            else
                AudioManager.Instance.PlaySE("Blood", enemyAudioSource);
        }
    }
}
