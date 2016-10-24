using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int hp;
    public bool isPlayerAlive = true;

    [SerializeField]
    private Slider hpBar;
    [SerializeField]
    private Text gameOverText;
    [SerializeField]
    private int ZombieDamage;
    [SerializeField]
    private int GhoulDamage;

    private GameObject player;

    void Start()
    {
        gameOverText.gameObject.SetActive(false);

        player = GameObject.FindGameObjectWithTag("Player");

        hpBar.minValue = 0f;
        hpBar.maxValue = hp;
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
            gameOverText.gameObject.SetActive(true);
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
