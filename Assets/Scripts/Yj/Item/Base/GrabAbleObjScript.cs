using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAbleObjScript : MonoBehaviour
{
    Rigidbody rb;

    public virtual void Initialize()
    {
        rb = transform.GetComponent<Rigidbody>();
    }

    public virtual void Grabbed()
    {
        rb.useGravity = false;
        rb.isKinematic = true;
        transform.localRotation = Quaternion.identity;

        Debug.Log("Grabbed");

        if(transform.GetComponent<BoxCollider>())
        {
            transform.GetComponent<BoxCollider>().isTrigger = true;
        }

        if(transform.GetComponent<CapsuleCollider>())
        {
            transform.GetComponent<CapsuleCollider>().isTrigger = true;
        }

        if (transform.GetComponent<SphereCollider>())
        {
            transform.GetComponent<SphereCollider>().isTrigger = true;
        }
    }

    public virtual void Release()
    {
        rb.useGravity = true;
        rb.isKinematic = false;

        if (transform.GetComponent<BoxCollider>())
        {
            transform.GetComponent<BoxCollider>().isTrigger = false;
        }
        if (transform.GetComponent<CapsuleCollider>())
        {
            transform.GetComponent<CapsuleCollider>().isTrigger = false;
        }
        if (transform.GetComponent<SphereCollider>())
        {
            transform.GetComponent<SphereCollider>().isTrigger = false;
        }
    }
}
