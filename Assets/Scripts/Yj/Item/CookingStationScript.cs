using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingStationScript : TableScript
{
    private void Update()
    {
        if(raisedObj == null)
        {
            return;
        }
        if (raisedObj.tag == "Pot")
        {
            raisedObj.GetComponent<PotScript>().Boiled();
        }
    }
}
