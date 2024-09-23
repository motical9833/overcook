using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    StageInfo currentStageData;


    private void Start()
    {
        currentStageData = new StageInfo();
    }


    public void SaveStageData(StageInfo data)
    {
        currentStageData = data;
    }
}