using UnityEngine;
using UnityEngine.UI;

public class GameText : MonoBehaviour
{
    [SerializeField]
    private Player player;

    private Text gameText;
    private AudioSource seSource;
    private bool _playingSound = false;

    void Start()
    {
        seSource = GetComponent<AudioSource>();
        gameText = GetComponent<Text>();
        gameText.text = "";
    }

    void Update()
    {
        if (player.hp <= 0f)
        {
            gameText.text = "Game Over!";
            gameText.color = Color.red;

            AudioManager.Instance.StopBGM();

            if (!_playingSound)
                AudioManager.Instance.PlaySE("GameOver", seSource);

            _playingSound = true;
        }

        if (GameManager.Instance.isTimeOver)
        {
            gameText.text = "Game Clear!";
            gameText.color = Color.green;

            AudioManager.Instance.StopBGM();

            if (!_playingSound)
                AudioManager.Instance.PlaySE("GameClear", seSource, 0.8f);

            _playingSound = true;
        }
    }

    public bool playingSound
    {
        get { return _playingSound; }
        set { _playingSound = value; }
    }
}
