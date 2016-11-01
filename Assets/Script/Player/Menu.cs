using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private AudioSource seSource;

    public Transform head;
    public GameObject menuObject;
    public GameObject gameTextObj;


    public bool isGameStart = false;
    private GameObject currentMenu = null;
    private Text gameText;
    private GameText gameTextScript;

    void Start()
    {
        gameText = gameTextObj.GetComponent<Text>();
        gameTextScript = gameTextObj.GetComponent<GameText>();
    }

    void Update()
    {
    }

    public void StartingAndDelete()
    {
        if (currentMenu == null)
        {
            Vector3 d = head.forward;
            d.y = 0.0f;
            d.Normalize();

            currentMenu = Instantiate(menuObject, head.position + d * 1.5f, head.rotation) as GameObject;
            currentMenu.transform.LookAt(head);

            //ゲーム中ならGameStartを消す
            if (GameManager.Instance.isGamePlaying)
            {
                currentMenu.transform.FindChild("GameStart").gameObject.SetActive(false);
            }
        }
        else
        {
            Destroy(currentMenu.gameObject);
        }
    }

    public void Restart()
    {
        isGameStart = false;
        gameText.text = "";
        gameTextScript.playingSound = false;
        GameManager.Instance.InitGame();
        EnemyManager.Instance.DestroyEnemys();
        AudioManager.Instance.StopBGM();

        if (currentMenu != null)
        {
            Destroy(currentMenu.gameObject);
        }
    }

    public void GameStart()
    {
        if (isGameStart) return;

        isGameStart = true;
        GameManager.Instance.CreateSpawnPoint();
        GameManager.Instance.isGamePlaying = true;
        AudioManager.Instance.PlaySE("GameStart", seSource);
        AudioManager.Instance.PlayBGM("BGM_1", 0.6f, 5.0f);

        if(currentMenu != null)
        {
            Destroy(currentMenu.gameObject);
        }
    }
}
