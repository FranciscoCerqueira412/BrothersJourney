using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]

public class MultipleCameraFollow : MonoBehaviour
{
    public List<Transform> targets;

    public Vector3 offset;
    private Vector3 velocity;
    public float smoothtime = 5f;
    public float minzoom = 40f;
    public float maxzoom = 10f;
    public float zoomlimiter=40f;
    private Camera cam;




    private void Start()
    {
        cam = GetComponent<Camera>();
    }
    private void LateUpdate()
    {

        if (targets.Count == 0)
            return;
        Move();
        Zoom();

        void Zoom()
        {

            float newzoom = Mathf.Lerp(maxzoom, minzoom, GetGreatestDistance()/ zoomlimiter);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView,newzoom,Time.deltaTime);
        }

        void Move()
        {
            Vector3 centerPoint = GetCenterPoint();

            Vector3 newPosition = centerPoint + offset;

            transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothtime);
        }

        float GetGreatestDistance()
        {
            var bounds = new Bounds(targets[0].position, Vector3.zero);

            for (int i = 0; i < targets.Count; i++)
            {
                bounds.Encapsulate(targets[i].position);
            }

            return Mathf.Max(bounds.size.x, bounds.size.y);
        }


    }
    Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
        {
            return targets[0].position;
        }

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for(int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.center;

    }



}
