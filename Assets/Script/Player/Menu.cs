using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Transform head;
    public GameObject menuObject;
    public Text gameText;

    public bool isGameStart = false;
    private GameObject currentMenu = null;
    private AudioSource menuAudio;

    void Start()
    {
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
        GameManager.Instance.InitGame();
        EnemyManager.Instance.DestroyEnemys();

        Destroy(currentMenu.gameObject);
    }

    public void GameStart()
    {
        if (isGameStart) return;

        isGameStart = true;
        GameManager.Instance.CreateSpawnPoint();
        GameManager.Instance.isGamePlaying = true;
        AudioManager.Instance.PlaySE("GameStart", menuAudio);
        Destroy(currentMenu.gameObject);
    }
}
