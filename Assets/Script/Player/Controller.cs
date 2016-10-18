using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{
	private LineRenderer line;
	private RaycastHit hit;
	private Ray ray;
    SteamVR_TrackedObject trackedObject;

    public GameObject bullet;
    public GameObject firingPorts;

    void Start()
    {
		line = GetComponent<LineRenderer> ();

        trackedObject = GetComponent<SteamVR_TrackedObject>();
    }

    void Update()
	{
		var device = SteamVR_Controller.Input((int) trackedObject.index);

		ray.direction = firingPorts.transform.forward;
		ray.origin = firingPorts.transform.position;

		line.SetPosition (0, ray.origin);
		line.SetPosition (1, ray.GetPoint(200));

        line.enabled = false;

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            line.enabled = true;

            GameObject bulletObject = Instantiate(bullet, firingPorts.transform.position, firingPorts.transform.rotation) as GameObject;

            bulletObject.GetComponent<Bullet>().direction = firingPorts.transform.forward;

            /*
            if (Physics.Raycast(ray, out hit, 200.0f))
            {
                Instantiate(cube, hit.point, hit.transform.rotation);
            }
            */
        }
    }
}
