using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int hp;
    public float sreenBloodMaxTime;
    public bool isPlayerAlive = true;
    public GameObject sreenBlood;

    [SerializeField]
    private Slider hpBar;
    [SerializeField]
    private int ZombieDamage;
    [SerializeField]
    private int GhoulDamage;

    private GameObject player;
    private int maxHp;
    private float sreenBloodTime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        hpBar.minValue = 0f;
        hpBar.maxValue = hp;

        maxHp = hp;
        sreenBloodTime = sreenBloodMaxTime;
    }
    
    public void InitPlayer()
    {
        hp = maxHp;
        hpBar.value = maxHp;
    }

    void Update()
    {
        this.transform.position = new Vector3(player.transform.position.x, 0f, player.transform.position.z);
        this.transform.rotation = Quaternion.identity;

        hpBar.value = hp;

        if (hp <= 0)
        {
            GameManager.Instance.isGamePlaying = false;
            isPlayerAlive = false;
        }

        if(sreenBlood.activeSelf)
        {
            sreenBloodTime -= Time.deltaTime;

            if(sreenBloodTime <= 0)
            {
                sreenBlood.SetActive(false);
            }
        }
    }

    void OnTriggerEnter(Collider hit)
    {
        if (hit.tag == "ZombieAttack")
        {
            if (0 < hp)
                hp -= ZombieDamage;

            sreenBlood.SetActive(true);
            sreenBloodTime = sreenBloodMaxTime;
        }

        if (hit.tag == "GhoulAttack")
        {
            if (0 < hp)
                hp -= GhoulDamage;

            sreenBlood.SetActive(true);
            sreenBloodTime = sreenBloodMaxTime;
        }
    }
}
