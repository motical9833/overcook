using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectStageScript : MonoBehaviour
{
    public StageSelectUIScript stageSelectUIScript;
    private GameObject stageObject;
    public bool isSelectStage = false;
    private float delayTime = 0.0f;

    void Start()
    {
        stageSelectUIScript = GameObject.Find("MainCanvas").GetComponent<StageSelectUIScript>();
    }

    void Update()
    {
        if(delayTime >= 1.0f)
        {
            StartStage();
            SelectStage();
            ExitSelectStage();
        }
        else
        {
            delayTime += Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        stageObject = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        stageObject = null;
    }

    private void StageSelect(Sprite img,string stageName , StageInfo stageData)
    {
        stageSelectUIScript.SelectStage(img, stageName, stageData);
    }

    private void SelectStage()
    {
        if (!isSelectStage && Input.GetKeyDown(KeyCode.Space) && stageObject != null)
        {
            StageSelect(stageObject.GetComponent<Stage>().GetStageImg(),stageObject.name ,stageObject.GetComponent<Stage>().GetStageInfo());
            this.transform.parent.GetComponent<GHMoveScript>().IsMove(false);
            isSelectStage = true;

        }
    }

    private void ExitSelectStage()
    {
        if (isSelectStage && Input.GetKeyDown(KeyCode.Escape) && stageObject != null)
        {
            stageSelectUIScript.ExitStageSelect();
            //this.GetComponent<GHMoveScript>().IsMove(true);
            this.transform.parent.GetComponent<GHMoveScript>().IsMove(false);
            isSelectStage = false;
        }
    }
        
    private void StartStage()
    {
        if(isSelectStage && Input.GetKeyDown(KeyCode.Space) && stageObject != null)
        {
            stageSelectUIScript.ExitStageSelect();
            isSelectStage = false;
            stageSelectUIScript.StartStage(stageObject.name);
        }
    }
}
