using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class Stage : MonoBehaviour
{
    private StageInfo stageInfo;
    
    private string filePath;

    public Sprite stageImg;

    private int[] stageGoals = { 20, 40, 60 };

    public int procedure;

    private void Awake()
    {
        filePath = Path.Combine(Application.persistentDataPath, gameObject.name + "_stagedata.json");
    }

    private void Start()
    {
        stageImg = Resources.Load<Sprite>("2D/Map_Img/" + gameObject.name);
        StartStageSetting();
    }

    public void SaveStageData()
    {
        string json = JsonUtility.ToJson(stageInfo, true);
        File.WriteAllText(filePath, json);
        Debug.Log("Stage data saved to " + filePath);
    }

    public void SaveStageData(StageInfo data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);
        Debug.Log("Stage data saved to " + filePath);
    }

    public void LoadStageData()     
    {
        procedure = this.transform.GetSiblingIndex();

        if (File.Exists(filePath))
        {   
            string json = File.ReadAllText(filePath);
            stageInfo = JsonUtility.FromJson<StageInfo>(json);
        }
        else
        {
            Debug.LogWarning("Stage data file not found for " + gameObject.name);

            if(gameObject.name == "Level_1-1")
            {
                stageInfo = new StageInfo(gameObject.name, 0, false, procedure, true, stageGoals);
            }
            else
            {
                stageInfo = new StageInfo(gameObject.name, 0, false, procedure, false, stageGoals); // 기본 값 설정
            }
            SaveStageData(); // 기본 값을 저장
        }
    }

    // 스테이지 정보 출력
    public void PrintStageInfo()
    {
        Debug.Log("Stage: " + stageInfo.stageName + ", score: " + stageInfo.score + ", Cleared: " + stageInfo.isCleared);
    }

    public void StartStageSetting()
    {
        if(stageInfo.isAble == false)
        {
            this.gameObject.gameObject.SetActive(false);
        }
    }

    public Sprite GetStageImg()
    {
        return stageImg;
    }

    public StageInfo GetStageInfo()
    {
        return stageInfo;
    }
}