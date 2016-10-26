using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{
    public GameObject gunObject;
    public GameObject menuObject;

    private SteamVR_TrackedObject trackedObject;
    private GameObject currentGun;
    private Gun gunComponent;
    private Menu menuComponent;

    void Start()
    {
        currentGun = Instantiate(gunObject);
        currentGun.transform.SetParent(transform);

        gunComponent = currentGun.GetComponent<Gun>();

        menuComponent = menuObject.GetComponent<Menu>();

        trackedObject = GetComponent<SteamVR_TrackedObject>();
    }

    void Update()
    {
        var device = SteamVR_Controller.Input((int)trackedObject.index);

        //弾を打つ
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            if(gunComponent != null)
            {
                device.TriggerHapticPulse(3000); //振動
                string tag = gunComponent.Fire();

                if(tag == "GameStart")
                {
                    menuComponent.GameStart();
                }

                if(tag == "Restart")
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

        //メニュー起動
        if(device.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            menuComponent.StartingAndDelete();
        }
    }
}
