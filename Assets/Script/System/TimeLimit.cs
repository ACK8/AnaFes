using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeLimit : MonoBehaviour
{
    public uint maxTime;
    public static float time = 0;

    private Slider timeLeftBar;

    void Start()
    {
        timeLeftBar = GetComponent<Slider>();
        timeLeftBar.maxValue = maxTime;
        time = maxTime;
    }

    void Update()
    {
        if (0 < time)
        {
            time -= Time.deltaTime;
        }

        time = Mathf.Clamp(time, 0, maxTime);

        timeLeftBar.value = time;
    }
}
