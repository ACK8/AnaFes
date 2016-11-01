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
    public GameObject rayPoint;
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

        rayPoint.transform.position = new Vector3(0.0f, 0.0f, 0.0f);

        //ポイント表示位置
        if (Physics.Raycast(ray, out hit, 200.0f))
        {
            rayPoint.transform.position = hit.point;
            line.SetPosition(1, hit.point);
        }

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

        if(numBullet <= 0)
        {
            Relod();
        }
    }

    //弾を撃つ
    public string Fire()
    {
        if (isRelod) return "Reloading";

        if (numBullet > 0)
        {
            Instantiate(firePointLight, firePoint.position, firePoint.rotation); //発砲時ポイントライト生成  
            GameObject flash = Instantiate(muzzleFlash, firePoint.position, firePoint.rotation) as GameObject; //発砲時パーティクル生成
            flash.transform.SetParent(transform);

            AudioManager.Instance.PlaySE("HandgunFire");

            numBullet -= 1; //弾減らす

            if (Physics.Raycast(ray, out hit, 200.0f))
            {
                //ゾンビに当たった処理
                if (hit.transform.tag == "Zombie")
                {
                    hit.collider.GetComponent<ChildeColliderTrigger>().HitRaycast(hit, 1);
                }

                //ゾンビに当たった処理（頭）
                if (hit.transform.tag == "ZombieHead")
                {
                    hit.collider.GetComponent<ChildeColliderTrigger>().HitRaycast(hit, 3);
                }

                //グールに当たった処理
                if (hit.transform.tag == "Ghoul")
                {
                    hit.collider.GetComponent<ChildeColliderTrigger>().HitRaycast(hit, 1);
                }

                //グールに当たった処理(頭)
                if (hit.transform.tag == "GhoulHead")
                {
                    hit.collider.GetComponent<ChildeColliderTrigger>().HitRaycast(hit, 3);
                }

                //ゲームスタートに当たった処理
                if (hit.transform.tag == "GameStart")
                {
                    return "GameStart";
                }

                //リスタートに当たった処理
                if (hit.transform.tag == "Restart")
                {
                    return "Restart";
                }

                //ゲーム終了に当たった処理
                if (hit.transform.tag == "QuitGame")
                {
                    Application.Quit();
                }
            }
        }

        return "None";
    }

    //弾をリロード
    public void Relod()
    {
        AudioManager.Instance.PlaySE("Reload");
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
