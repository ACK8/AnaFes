using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    private bool _isGamePlaying = false;
    private bool _isGameOver = false;
    private bool _isTimeOver = false;
    
    [SerializeField]
    private Player player;
    [SerializeField]
    private GameObject spawnPoint;

    private GameObject createdSpawnPoint;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    
    public void CreateSpawnPoint()
    {
        createdSpawnPoint = Instantiate(spawnPoint);
    }

    public void InitGame()
    {
        if (createdSpawnPoint != null)
            Destroy(createdSpawnPoint.gameObject);

        TimeLimit.Init();
        Score.numScore = 0;
        _isGamePlaying = false;
        _isGameOver = false;
        _isTimeOver = false;
        player.InitPlayer();
    }

    public bool isGamePlaying
    {
        get { return _isGamePlaying; }
        set { _isGamePlaying = value; }
    }

    public bool isGameOver
    {
        get { return _isGameOver; }
        set { _isGameOver = value; }
    }

    public bool isTimeOver
    {
        get { return _isTimeOver; }
        set { _isTimeOver = value; }
    }

}
