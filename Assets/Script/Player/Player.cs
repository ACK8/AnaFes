using UnityEngine;

public class Player : MonoBehaviour
{
    public int hp;

    [SerializeField]
    private int ZombieDamage;
    [SerializeField]
    private int GhoulDamage;

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        this.transform.position = new Vector3(player.transform.position.x, 0f, player.transform.position.z);
        this.transform.rotation = Quaternion.identity;
    }

    void OnTriggerEnter(Collider hit)
    {
        if(hit.tag == "Zombie")
        {
           print("Zombie Hit");
            hp -= ZombieDamage;
        }

        if (hit.tag == "Ghoul")
        {
            print("Ghoul Hit");
            hp -= GhoulDamage;
        }
    }
}
