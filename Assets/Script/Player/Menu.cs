using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    SteamVR_TrackedObject trackedObjectLeft;
    SteamVR_TrackedObject trackedObjectRight;

    public GameObject leftTracked;
    public GameObject rightTracked;
    public Transform head;
    public GameObject menuObject;
    public GameObject pawon;
    public GameObject NowPawon;
    
    private Fire fireLeft;
    private Fire fireRight;
    private GameObject currentMenu = null;

    void Start()
    {
        trackedObjectLeft = leftTracked.GetComponent<SteamVR_TrackedObject>();
        trackedObjectRight = rightTracked.GetComponent<SteamVR_TrackedObject>();
        fireLeft = leftTracked.GetComponent<Fire>();
        fireRight = rightTracked.GetComponent<Fire>();

        NowPawon = Instantiate(pawon);
    }

    void Update()
    {
        var deviceLeft = SteamVR_Controller.Input((int)trackedObjectLeft.index);
        var deviceRight = SteamVR_Controller.Input((int)trackedObjectRight.index);

        bool isLeft = deviceLeft.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu);
        bool isRight = deviceRight.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu);

        //メニューボタン
        if (isLeft || isRight)
        {
            if (currentMenu == null)
            {
                Vector3 d = head.forward;
                d.y = 0.0f;
                d.Normalize();

                currentMenu = Instantiate(menuObject, head.position + d * 1.5f, head.rotation) as GameObject;
                currentMenu.transform.LookAt(head);
            }
            else
            {
                Destroy(currentMenu);
            }
        }

        if (fireLeft.isRestart || fireRight.isRestart)
        {
            EnemyManager.Instance.DestroyEnemys();

            /*
            currentMenu.SetActive(false);
            

            fireLeft.isRestart = false;
            fireRight.isRestart = false;

            Destroy(NowPawon);

            GameObject[] zombieList = GameObject.FindGameObjectsWithTag("ZombieAttack");
            GameObject[] ghoulList = GameObject.FindGameObjectsWithTag("GhoulAttack");

            for (int i = 0; i < zombieList.Length; i++)
            {
                Debug.Log(zombieList[i].tag);
                Destroy(zombieList[i]);
            }

            for (int i = 0; i < ghoulList.Length; i++)
            {
                Destroy(ghoulList[i]);
            }
            */
        }
    }
}
