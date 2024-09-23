using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetFollowViewScript : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public bool maintainRotation = true;

    private Quaternion initRotation;

    void Start()
    {
        offset = transform.position;
        target = GameObject.FindWithTag("Player").transform;
        initRotation = transform.rotation;
    }

    void Update()
    {
        transform.position = target.position + offset;

        if (maintainRotation)
        {
            transform.rotation = initRotation;
        }
        else
        {
            transform.LookAt(target);
        }
    }
}
