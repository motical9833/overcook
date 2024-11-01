using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPotUIScript : MonoBehaviour
{
    public GameObject mPot;
    void Start()
    {
        
    }

    void Update()
    {
        this.transform.position = mPot.transform.position;
    }
}
