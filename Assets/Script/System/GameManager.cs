using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public bool isGamePlaying = false;
    public bool isTimeOver = false;

    [SerializeField]
    private Text gameText;
    [SerializeField]
    private Player player;
    [SerializeField]
    private GameObject spawnPoint;

    private GameObject createdSpawnPoint;

    void Start()
    {
        gameText.gameObject.SetActive(false);
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        if (player.hp <= 0)
        {
            gameText.text = "GameOver!";
            gameText.gameObject.SetActive(true);
        }

        if (TimeLimit.time <= 0)
        {
            gameText.text = "Time Over!";
            gameText.gameObject.SetActive(true);
        }
    }

    public void EnanledGameOverText(bool f)
    {
        gameText.gameObject.SetActive(f);
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
        TimeLimit.Init();
    }
}
