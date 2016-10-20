using UnityEngine;
using System.Collections;

public class NumberBulletGUI : MonoBehaviour
{
    private TextMesh textMesh;
    public int numBullet = 0;
    public int numBulletMax = 0;
    public bool isRelod = false;
    public float relodTime = 0.0f;

	void Start ()
    {
        textMesh = GetComponent<TextMesh>();
        numBullet = numBulletMax;
    }
	
	void Update ()
    {
        if (numBullet <= 0) numBullet = 0;

        if(isRelod)
        {
            if (relodTime <= 0)
            {
                relodTime = 0.0f;
                isRelod = false;
            }
            else
            {
                textMesh.text = string.Format("{0}", (int)relodTime);
                textMesh.color = new Color(1.0f, 0.0f, 0.0f);
                relodTime -= Time.deltaTime;
            }
        }
        else
        {
            textMesh.text = string.Format("{0}", numBullet);
            textMesh.color = new Color(0.0f, 1.0f, 0.0f);
        }
    }
}
