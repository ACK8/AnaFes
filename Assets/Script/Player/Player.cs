using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int hp;
    public bool isPlayerAlive = true;

    [SerializeField]
    private Slider hpBar;
    [SerializeField]
    private int ZombieDamage;
    [SerializeField]
    private int GhoulDamage;

    private GameObject player;
    private int maxHp;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        hpBar.minValue = 0f;
        hpBar.maxValue = hp;

        maxHp = hp;
    }
    
    public void InitPlayer()
    {
        hp = maxHp;
    }

    void Update()
    {
        this.transform.position = new Vector3(player.transform.position.x, 0f, player.transform.position.z);
        this.transform.rotation = Quaternion.identity;

        if (isPlayerAlive)
            hpBar.value = hp;

        if (hp <= 0)
        {
            GameManager.Instance.isGamePlaying = false;
            isPlayerAlive = false;
        }
    }

    void OnTriggerEnter(Collider hit)
    {
        if (hit.tag == "ZombieAttack")
        {
            if (0 < hp)
                hp -= ZombieDamage;
        }

        if (hit.tag == "GhoulAttack")
        {
            if (0 < hp)
                hp -= GhoulDamage;
        }
    }
}
