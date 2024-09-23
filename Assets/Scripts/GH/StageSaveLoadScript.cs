using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System;
using System.Xml;
using Unity.VisualScripting;

public class StageSaveLoadScript : MonoBehaviour
{
    public Dictionary<string, Stage> stageDictionary;

    StageInfo prevStageInfo;
    public GameObject sceneMainCanvas;

    int allStarCount = 0;

    private void Start()
    {
        SceneManager.sceneLoaded += SceneLoad;
        prevStageInfo = new StageInfo();
        stageDictionary = new Dictionary<string, Stage>();
    }

    public void StageToSceneLoad()
    {
        SceneManager.sceneLoaded += SceneLoad;
    }

    public void SceneLoad(Scene scene, LoadSceneMode mode)
    {
        stageDictionary.Clear();
        sceneMainCanvas = GameObject.FindWithTag("MainCanvas");
        GameObject objs = GameObject.FindGameObjectWithTag("StageObject");

        for (int i = 0; i < objs.transform.childCount; i++)
        {

            stageDictionary.Add(objs.transform.GetChild(i).name, objs.transform.GetChild(i).GetComponent<Stage>());
        }

        foreach (Stage value in stageDictionary.Values)
        {
            value.LoadStageData();
        } 

        if(sceneMainCanvas)
        SaveStarCount();

        SceneManager.sceneLoaded -= SceneLoad;
    }

    public void SaveAllStages()
    {

        foreach (Stage value in stageDictionary.Values)
        {
            value.LoadStageData();
        }
        Debug.Log("ALL stage data saved.");
    }

    public void LoadAllStages()
    {

        foreach (Stage value in stageDictionary.Values)
        {
            value.LoadStageData();
        }
        Debug.Log("All stage data loaded.");
    }

    public void SavePrevStageInfo(StageInfo info)
    {
        prevStageInfo = info;

        Debug.Log(prevStageInfo.stageName + ":" + prevStageInfo.score);
    }

    public StageInfo GetPrevStageInfo()
    {
        return prevStageInfo;
    }

    public List<bool> GetAllisAble()
    {
        List<bool> bools = new List<bool>();

        foreach (Stage value in stageDictionary.Values)
        {
            bools.Add(value.GetStageInfo().isAble);
        }

        return bools;
    }

    public List<Stage> GetStageList()
    {
        List<Stage> stages = new List<Stage>();

        foreach (Stage value in stageDictionary.Values)
        {
            stages.Add(value);
        }

        return stages;
    }

    public int GetAllStarCount()
    {
        return allStarCount;
    }

    public void SaveStarCount()
    {
        allStarCount = 0;

        foreach (Stage value in stageDictionary.Values)
        {
            StageInfo info = value.GetStageInfo();

            int score = info.score;
            int[] goals = info.goals;

            for (int i = 0; i < goals.Length; i++)
            {
                if (score > goals[i])
                    allStarCount++;
            }
        }

        sceneMainCanvas.GetComponent<StarCountUIScript>().ApplyStarCountUI(allStarCount);

        Debug.Log("스타 개수" + allStarCount);
    }
}
