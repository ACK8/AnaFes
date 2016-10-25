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

    private Fire fireLeft;
    private Fire fireRight;
    private GameObject currentMenu = null;

    void Start()
    {
        trackedObjectLeft = leftTracked.GetComponent<SteamVR_TrackedObject>();
        trackedObjectRight = rightTracked.GetComponent<SteamVR_TrackedObject>();
        fireLeft = leftTracked.GetComponent<Fire>();
        fireRight = rightTracked.GetComponent<Fire>();
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
                Destroy(currentMenu.gameObject);
            }
        }

        //リスタート
        if (fireLeft.isRestart || fireRight.isRestart)
        {
            GameManager.Instance.EnanledGameOverText(false);
            GameManager.Instance.InitGame();
            EnemyManager.Instance.DestroyEnemys();

            fireLeft.isRestart = false;
            fireRight.isRestart = false;

            GameManager.Instance.isGamePlaying = false;

            Destroy(currentMenu.gameObject);
        }

        if (GameManager.Instance.isGamePlaying)
        {
            if (currentMenu != null)
            {
                currentMenu.transform.FindChild("GameStart").gameObject.SetActive(false);
            }
        }

        //スタート
        if (fireLeft.isGameStart || fireRight.isGameStart)
        {
            Destroy(currentMenu.gameObject);

            fireLeft.isGameStart = false;
            fireRight.isGameStart = false;
        }
    }
}
