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

	private Ray ray;
	private GameObject gun;
	private Transform firePoint;
	private LineRenderer line;

    void Start()
	{
		line = GetComponent<LineRenderer> ();
        trackedObject = GetComponent<SteamVR_TrackedObject>();
        oldGuntype = guntype;

        gun = Instantiate(gunArray[(int)guntype]) as GameObject;

        gun.transform.SetParent(this.gameObject.transform);

        firePoint = gun.transform.FindChild("FirePoint");
    }

    void Update()
    {
		ray.direction = firePoint.transform.forward;
		ray.origin = firePoint.transform.position;

		line.SetPosition (0, ray.origin);
		line.SetPosition (1, ray.GetPoint(200));

        var device = SteamVR_Controller.Input((int)trackedObject.index);

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
			device.TriggerHapticPulse (3000);


            GameObject bulletObject = Instantiate(bullet, firePoint.position, firePoint.rotation) as GameObject;

            bulletObject.GetComponent<Bullet>().direction = firePoint.forward;
        }
    }
}
