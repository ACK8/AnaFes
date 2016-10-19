using UnityEngine;
using System.Collections;

public class NumberBulletGUI : MonoBehaviour
{
    private TextMesh textMesh;
    public int numBullet = 1;

	void Start ()
    {
        textMesh = GetComponent<TextMesh>();
    }
	
	void Update ()
    {
        if (numBullet <= 0) numBullet = 0;

        textMesh.text = string.Format("{0}", numBullet);
    }
}
