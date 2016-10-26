using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    public int numMaxBullet;
    public float maxRelodTime;
    public GameObject bulletObject;
    public GameObject firePointLight;
    public GameObject bulletGUI;
    public GameObject muzzleFlash;
    public Transform firePoint;

    private bool isRelod = false;
    private int numBullet;
    private float currentRelodTime = 0.0f;
    private Ray ray;
    private RaycastHit hit;
    private LineRenderer line;
    private TextMesh bulletTextMesh;

    void Start ()
    {
        numBullet = numMaxBullet;
        line = GetComponent<LineRenderer>();

        bulletTextMesh = bulletGUI.GetComponent<TextMesh>();
    }

    void Update ()
    {
        ray.direction = firePoint.transform.forward;
        ray.origin = firePoint.transform.position;

        line.SetPosition(0, ray.origin);
        line.SetPosition(1, ray.GetPoint(200));

        //リロード中
        if (isRelod)
        {
            if (currentRelodTime <= 0)
            {
                currentRelodTime = 0.0f;
                isRelod = false;
            }
            else
            {
                bulletTextMesh.text = string.Format("{0}", (int)currentRelodTime);
                bulletTextMesh.color = new Color(1.0f, 0.0f, 0.0f);
                currentRelodTime -= Time.deltaTime;
            }
        }
        else
        {
            bulletTextMesh.text = string.Format("{0}", numBullet);
            bulletTextMesh.color = new Color(0.0f, 1.0f, 0.0f);
        }
    }

    //弾を打つ
    public string Fire()
    {
        if (isRelod) return "Reloading";

        if (numBullet > 0)
        {
            Instantiate(firePointLight, firePoint.position, firePoint.rotation); //発砲時ポイントライト生成  
            GameObject flash = Instantiate(muzzleFlash, firePoint.position, firePoint.rotation) as GameObject; //発砲時パーティクル生成
            flash.transform.SetParent(transform);

            numBullet -= 1; //弾減らす

            if (Physics.Raycast(ray, out hit, 200.0f))
            {
                //ゾンビに当たった処理
                if (hit.transform.tag == "Zombie")
                {
                    hit.collider.GetComponent<ChildeColliderTrigger>().HitRaycast(hit);
                }

                //グールに当たった処理
                if (hit.transform.tag == "Ghoul")
                {
                    hit.collider.GetComponent<ChildeColliderTrigger>().HitRaycast(hit);
                }

                //ゲームスタートに当たった処理
                if (hit.transform.tag == "GameStart")
                {
                    Debug.Log("GameStartをクリック");
                    return "GameStart";
                }

                //リスタートに当たった処理
                if (hit.transform.tag == "Restart")
                {
                    Debug.Log("Restartをクリック");
                    return "Restart";
                }

                //ゲーム終了に当たった処理
                if (hit.transform.tag == "QuitGame")
                {
                    Debug.Log("QuitGameをクリック");
                    Application.Quit();
                }
            }
        }

        return "None";
    }

    //弾をリロード
    public void Relod()
    {
        if (isRelod) return;

        if (numBullet < numMaxBullet)
        {
            isRelod = true;
            numBullet = numMaxBullet;
            currentRelodTime = maxRelodTime;
        }
    }

    public void InitFire()
    {
        if (numBullet < numMaxBullet)
        {
            numBullet = numMaxBullet;
        }
    }
}
