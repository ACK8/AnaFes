using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private Text scoreText;

    public static uint numScore = 0;

    void Start()
    {
        scoreText = GetComponent<Text>();
    }

    void Update()
    {
        if (GameManager.Instance.isGameOver || GameManager.Instance.isTimeOver)
        {
            scoreText.text = "Score: " + numScore;
        }
        else
        {
            scoreText.text = "";
        }
    }
}
