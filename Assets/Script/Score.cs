using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private Text textMesh;
    public static uint numScore = 0; 

	void Start ()
    {
        textMesh = GetComponent<Text>();
    }

    void Update ()
    {
        textMesh.text = "Score: " + numScore;
    }
}
