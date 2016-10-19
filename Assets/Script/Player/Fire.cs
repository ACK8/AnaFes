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

    public LayerMask layerMask;
    public GunType guntype = GunType.eHandGun;
    private GunType oldGuntype;
    public GameObject bullet;
    public GameObject[] gunArray;
    public GameObject firePointLight;

    private Ray ray;
    private RaycastHit hit;
    private GameObject gun;
    private NumberBulletGUI numberBullet;
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
        numberBullet = gun.transform.FindChild("NumberBulletGUI").gameObject.GetComponent<NumberBulletGUI>();
    }

    void Update()
    {
		ray.direction = firePoint.transform.forward;
		ray.origin = firePoint.transform.position;
       

		line.SetPosition (0, ray.origin);
		line.SetPosition (1, ray.GetPoint(200));

        var device = SteamVR_Controller.Input((int)trackedObject.index);

        //弾を打つ
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            if(numberBullet.numBullet > 0)
            {
                //GameObject bulletObject = Instantiate(bullet, firePoint.position, firePoint.rotation) as GameObject;

                //bulletObject.GetComponent<Bullet>().direction = firePoint.forward;
                Instantiate(firePointLight, firePoint.position, firePoint.rotation);
                device.TriggerHapticPulse(3000);
                numberBullet.numBullet -= 1;

                if (Physics.Raycast(ray, out hit, 200.0f))
                {
                    if (hit.transform.tag == "Zombie")
                    {
                        hit.collider.GetComponent<ChildeColliderTrigger>().HitRaycast(hit);
                    }
                }
            }
        }
        
        //弾をリロード
        if(device.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            switch (guntype)
            {
                case GunType.eHandGun:
                    numberBullet.numBullet = 15;
                    break;

                case GunType.eShotGun:
                    numberBullet.numBullet = 10;
                    break;
            }
        }
    }
}
