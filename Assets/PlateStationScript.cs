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

        PlateStationSoundPlay();
        plateReturnTr.GetComponent<PlateReturnScript>().SettingTablePlate();
    }

    public void PlateStationSoundPlay()
    {
        this.gameObject.GetComponent<AudioSource>().Play();
    }
}