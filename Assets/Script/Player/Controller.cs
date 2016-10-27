using UnityEngine;
using System.Collections;

public enum WeaponType
{
    eHandGun,
    eAxe,
}

public class Controller : MonoBehaviour
{
    public WeaponType weaponType;
    public GameObject gunObject;
    public GameObject cutleryObject;
    public GameObject menuObject;

    private SteamVR_TrackedObject trackedObject;
    private GameObject currentGun;
    private GameObject currentCutlery;
    private Gun gunComponent;
    private Menu menuComponent;

    void Start()
    {
        currentGun = Instantiate(gunObject);
        currentGun.transform.SetParent(transform);

        currentCutlery = Instantiate(cutleryObject);
        currentCutlery.transform.SetParent(transform);

        switch (weaponType)
        {
            case WeaponType.eHandGun:
                currentCutlery.SetActive(false);
                break;

            case WeaponType.eAxe:
                currentGun.SetActive(false);
                break;
        }

        gunComponent = currentGun.GetComponent<Gun>();

        menuComponent = menuObject.GetComponent<Menu>();

        trackedObject = GetComponent<SteamVR_TrackedObject>();
    }

    void Update()
    {
        var device = SteamVR_Controller.Input((int)trackedObject.index);

        //持ってる武器の種類事
        switch (weaponType)
        {
            case WeaponType.eHandGun:
                Gun(device);
                break;

            case WeaponType.eAxe:
                Cutlery();
                break;
        }

        //メニュー起動
        if(device.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            menuComponent.StartingAndDelete();
        }

        //武器変更
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Debug.Log("タッチパッドをクリックした");

            Vector2 position = device.GetAxis();

            //右
            if (position.x >= 0.1f)
            {
                weaponType = WeaponType.eHandGun;
                currentGun.SetActive(true);
                currentCutlery.SetActive(false);
                Debug.Log("右をクリックした");
            }
            else if (position.x <= -0.1f) //左
            {
                weaponType = WeaponType.eAxe;
                currentGun.SetActive(false);
                currentCutlery.SetActive(true);
                Debug.Log("左をクリックした");
            }
            else if (position.y >= 0.5f) //上
            {

                Debug.Log("上をクリックした");
            }
            else if (position.y <= -0.5f)
            {

                Debug.Log("下をクリックした");
            }
        }
    }

    void Gun(SteamVR_Controller.Device device)
    {
        //弾を打つ
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            if (gunComponent != null)
            {
                device.TriggerHapticPulse(3000); //振動
                string tag = gunComponent.Fire();

                if (tag == "GameStart")
                {
                    menuComponent.GameStart();
                }

                if (tag == "Restart")
                {
                    menuComponent.Restart();
                }
            }
        }

        //弾をリロード
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            gunComponent.Relod();
        }
    }

    void Cutlery()
    {

    }
}
