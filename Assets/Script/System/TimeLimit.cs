using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeLimit : MonoBehaviour
{
    private Text textMesh;

    public static uint time = 0;

    void Start()
    {
        textMesh = GetComponent<Text>();
    }

    void Update()
    {
        textMesh.text = "Time: " + time;
    }
}
