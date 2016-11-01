using UnityEngine;
using UnityEngine.UI;

public class GameText : MonoBehaviour
{
    [SerializeField]
    private Player player;

    private Text gameText;
    private AudioSource seSource;

    void Start()
    {
        gameText = GetComponent<Text>();
        gameText.text = "";
    }

    void Update()
    {
        if (player.hp <= 0)
        {
            gameText.text = "Game Over!";
            gameText.color = Color.red;

            AudioManager.Instance.PlaySE("GameOver", seSource);
        }

        if (GameManager.Instance.isTimeOver)
        {
            gameText.text = "Game Clear!";
            gameText.color = Color.green;

            AudioManager.Instance.PlaySE("GameClear", seSource);
        }
    }
}
