﻿using UnityEngine;

public class Lightsaber : MonoBehaviour
{
    [SerializeField]
    private GameObject laserPos;
    [SerializeField]
    private float MaxlaserLength = 1.5f;
    [SerializeField]
    private float addingLaserLength;
    [SerializeField]
    private Color color;

    private SteamVR_TrackedObject trackedObject;
    private LineRenderer line;
    private float laserLengh = 0f;

    Ray ray;
    RaycastHit hit;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.SetColors(color, color);

        trackedObject = gameObject.transform.parent.gameObject.GetComponent<SteamVR_TrackedObject>();
    }

    void Update()
    {
        var device = SteamVR_Controller.Input((int)trackedObject.index);

        if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger))
        {
            if (laserLengh <= MaxlaserLength)
                laserLengh += addingLaserLength * Time.deltaTime;
        }
        else
        {
            if (0 < laserLengh)
                laserLengh -= addingLaserLength * Time.deltaTime;
        }

        laserLengh = Mathf.Clamp(laserLengh, 0f, MaxlaserLength);

        ray.origin = laserPos.transform.position;
        ray.direction = laserPos.transform.forward;

        line.SetPosition(0, ray.origin);
        line.SetPosition(1, ray.GetPoint(laserLengh));

        if (Physics.Raycast(ray, out hit, laserLengh))
        {
            if (hit.transform.tag == "Zombie")
            {
                hit.collider.GetComponent<ChildeColliderTrigger>().HitRaycast(hit, 1);
            }
            if (hit.transform.tag == "Ghoul")
            {
                hit.collider.GetComponent<ChildeColliderTrigger>().HitRaycast(hit, 1);
            }
        }
    }
}
