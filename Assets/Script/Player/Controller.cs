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
    public float touchPadValue = 0.1f;
    public float cutleryValue = 3.0f;

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

        Gun(device);

        /*
        //持ってる武器の種類事
        switch (weaponType)
        {
            case WeaponType.eHandGun:
                Gun(device);
                break;

            case WeaponType.eAxe:

                Cutlery(device.angularVelocity);
                break;
        }
        */

        //メニュー起動
        if(device.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            menuComponent.StartingAndDelete();
        }

        //武器変更
        /*
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Vector2 position = device.GetAxis();

            if (position.x >= touchPadValue) //右
            {
                weaponType = WeaponType.eHandGun;
                currentGun.SetActive(true);
                currentCutlery.SetActive(false);
            }
            else if (position.x <= -touchPadValue) //左
            {
                weaponType = WeaponType.eAxe;
                currentGun.SetActive(false);
                currentCutlery.SetActive(true);
            }
            else if (position.y >= touchPadValue) //上
            {

            }
            else if (position.y <= -touchPadValue) //下
            {

            }
        }
        */
    }

    //銃の処理
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

        if(device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            gunComponent.Relod();
        }

        /*
        //弾をリロード
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            gunComponent.Relod();
        }
        */
    }

    //刃物処理
    void Cutlery(Vector3 angularVelocity)
    {
        if(angularVelocity.x > cutleryValue || angularVelocity.y > cutleryValue || angularVelocity.z > cutleryValue ||
            angularVelocity.x < -cutleryValue || angularVelocity.y < -cutleryValue || angularVelocity.z < -cutleryValue)
        {
            currentCutlery.GetComponent<Cutlery>().Attack();
        }
        else
        {
            currentCutlery.GetComponent<Cutlery>().NoAttack();
        }
    }
}
