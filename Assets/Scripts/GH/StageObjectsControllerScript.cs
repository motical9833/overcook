using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class StageObjectsControllerScript : MonoBehaviour
{
    public List<GameObject> stageObject = new List<GameObject>();

    public void OpenStage()
    {
        int count = this.transform.childCount;

        for (int i = 0; i < count; i++)
        {
            GameObject childObject = this.transform.GetChild(i).gameObject;
            Stage stage = childObject.GetComponent<Stage>();

            if(stage == null)
            {
                Debug.Log("Stage스크립트가 존재하지 않음");
            }

            stage.LoadStageData();
            stage.PrintStageInfo();

            stageObject.Add(childObject);
        }
         
        for (int i = 1; i <stageObject.Count; i++)
        {
            Stage previouseStage = stageObject[i - 1].GetComponent<Stage>();
            Stage currentStage = stageObject[i].GetComponent<Stage>();

            bool isbool = previouseStage.GetStageInfo().isCleared;

            if (previouseStage != null && currentStage != null && previouseStage.GetStageInfo().isCleared)
            {
                currentStage.GetStageInfo().isAble = true;
                currentStage.SaveStageData();
                currentStage.LoadStageData();
                currentStage.StartStageSetting();
            }
            else if(previouseStage != null && currentStage != null && !previouseStage.GetStageInfo().isCleared)
            {
                currentStage.StartStageSetting();
            }
        }
    }
}
