using UnityEngine;
using UnityEngine.UI;

public class TimeLimit : MonoBehaviour
{
    public uint maxTime;
    public static float time = 0.0f;

    private Slider timeLeftBar;

    void Start()
    {
        timeLeftBar = GetComponent<Slider>();
        timeLeftBar.maxValue = maxTime;
        time = maxTime;
    }

    void Update()
    {
        if (0 < time && GameManager.Instance.isGamePlaying)
        {
            time -= Time.deltaTime;
        }

        time = Mathf.Clamp(time, 0, maxTime);

        if (time <= 0)
        {
            GameManager.Instance.isTimeOver = true;
        }

        timeLeftBar.value = time;
    }

    public static void Init()
    {
        time = 180;
    }
}
