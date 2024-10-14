using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateStationScript : MonoBehaviour
{
    public Transform plateReturnTr;
    void Start()
    {
        plateReturnTr = transform.GetChild(0);
    }

    public void SetPlateReturn(GameObject plate)
    {
        plate.transform.SetParent(plateReturnTr, false);
        plate.SetActive(false);

        plateReturnTr.GetComponent<PlateReturnScript>().SettingTablePlate();
    }
}
