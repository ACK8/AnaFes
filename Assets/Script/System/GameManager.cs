using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public bool isGamePlaying = false;

    [SerializeField]
    private Text gameOverText;
    [SerializeField]
    private Player player;
    [SerializeField]
    private Fire[] fire;
    [SerializeField]
    private GameObject spawnPoint;

    private GameObject createdSpawnPoint;

    void Start()
    {
        gameOverText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (player.hp <= 0)
        {
            gameOverText.gameObject.SetActive(true);
        }
    }

    public void EnanledGameOverText(bool f)
    {
        gameOverText.gameObject.SetActive(f);
    }

    public void CreateSpawnPoint()
    {
        createdSpawnPoint = Instantiate(spawnPoint);
    }

    public void InitGame()
    {
        if (createdSpawnPoint != null)
            Destroy(createdSpawnPoint.gameObject);

        isGamePlaying = false;
        player.InitPlayer();
        Score.numScore = 0;

        for (int i = 0; i < fire.Length; i++)
        {
            fire[i].InitFire();
        }
    }
}
