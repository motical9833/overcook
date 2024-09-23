using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPathsControllerScript : MonoBehaviour
{
    public void OpenPath(int procedure)
    {
        this.transform.GetChild(1).GetComponent<MapPathsScript>().UnLockPath(procedure);
    }

    public void InitializePathGrop()
    {
        GameObject gameManager = GameObject.FindWithTag("GameManager");
        MapPathsScript mapPathScript = this.transform.GetChild(1).GetComponent<MapPathsScript>();

        if (!gameManager)
        {
            Debug.Log("게임 매니저가 존재하지 않음");
            return;
        }

        List<Stage> stages = gameManager.GetComponent<StageSaveLoadScript>().GetStageList();

        //추후 스테이지의 값을 늘리면 값 수정 필요
        for (int i = 0; i < 2; i++)
        {
            if (stages[i].GetStageInfo().isCleared)
            {
                mapPathScript.ActivePath(i);
            }
        }
    }
}