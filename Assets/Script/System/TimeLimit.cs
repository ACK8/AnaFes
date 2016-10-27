using UnityEngine;
using UnityEngine.UI;

public class TimeLimit : MonoBehaviour
{
    [SerializeField]
    private Text timeOverText;

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

        if(time <= 0)
        {
            GameManager.Instance.isTimeOver = true;
        }

        timeLeftBar.value = time;
    }
}
