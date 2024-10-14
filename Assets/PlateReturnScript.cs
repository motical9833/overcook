using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateReturnScript : MonoBehaviour
{
    TableScript tableScr;
    bool isPlate;
    float timer;

    void Start()
    {
        tableScr = this.GetComponent<TableScript>();
    }

    public void SettingTablePlate()
    {
        if (tableScr == null || tableScr.GetRaisedObject() != null)
        {
            Debug.Log("스크립트가 존재하지 않거나 이미 테이블에 접시가 존재함");
            return;
        }

        GameObject plate = transform.GetChild(0).gameObject;
        tableScr.RaisObject(plate);
        plate.SetActive(true);
    }
}
