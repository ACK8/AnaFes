using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour
{
    private TextMesh textMesh;
    public static uint numScore = 0; 

	void Start ()
    {
        textMesh = GetComponent<TextMesh>();
    }

    void Update ()
    {
        textMesh.text = string.Format("{0}", numScore);
    }
}
