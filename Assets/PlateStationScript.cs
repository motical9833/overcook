using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateStationScript : MonoBehaviour
{
    public GameObject plateReturnObject;
    void Start()
    {
        plateReturnObject = GameObject.FindGameObjectWithTag("PlateReturn");
    }


    void Update()
    {
        
    }
}
