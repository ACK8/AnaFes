using UnityEngine;
using System.Collections;

public class DebugTool : MonoBehaviour
{
    public GameObject menuObject;

    private Menu menuCommpont;

	void Start ()
    {
        menuCommpont = menuObject.GetComponent<Menu>();
    }

    void Update()
    {
        //ゲームスタート
        if (Input.GetKeyDown(KeyCode.F1))
        {
            menuCommpont.GameStart();
        }

        //リスタート
        if (Input.GetKeyDown(KeyCode.F2))
        {
            menuCommpont.Restart();
        }
    }
}
