using UnityEngine;
using System.Collections;

public enum GunType
{
    eHandGun = 0,
    eShotGun = 1,
}

public class Fire : MonoBehaviour
{
    SteamVR_TrackedObject trackedObject;

    public GunType guntype = GunType.eHandGun;
    private GunType oldGuntype;
    public GameObject bullet;
    public GameObject[] gunArray;

    private GameObject gun;

    void Start()
    {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
        oldGuntype = guntype;

        gun = Instantiate(gunArray[(int)guntype]) as GameObject;

        gun.transform.SetParent(this.gameObject.transform);
    }

    void Update()
    {
        var device = SteamVR_Controller.Input((int)trackedObject.index);

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            Transform firePoint = gun.transform.FindChild("FirePoint");

            GameObject bulletObject = Instantiate(bullet, firePoint.position, firePoint.rotation) as GameObject;

            bulletObject.GetComponent<Bullet>().direction = firePoint.forward;
        }
    }
}
