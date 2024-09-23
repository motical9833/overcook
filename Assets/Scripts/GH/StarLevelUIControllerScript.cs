using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StarLevelUIControllerScript : MonoBehaviour
{

    GameObject smallStarsParent;
    GameObject largeStarsParent;


    private void Start()
    {
        SetStarImageBasedOnCount();
    }

    private void OnEnable()
    {
        smallStarsParent = this.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
        largeStarsParent = this.transform.GetChild(0).GetChild(0).GetChild(1).gameObject;

        if(smallStarsParent == null || largeStarsParent == null)
        {
            Debug.Log("할당되지 않은 UI오브젝트가 존재함!");
        }
    }

    public void SetStarImageBasedOnCount(int count)
    {
        int maxCount = 4;

        for (int i = 0; i < maxCount; i++)
        {
            smallStarsParent.GetComponent<Transform>().GetChild(0).GetChild(i).gameObject.SetActive(false);
            largeStarsParent.GetComponent<Transform>().GetChild(0).GetChild(i).gameObject.SetActive(false);
        }

        smallStarsParent.GetComponent<Transform>().GetChild(0).GetChild(count).gameObject.SetActive(true);
        largeStarsParent.GetComponent<Transform>().GetChild(0).GetChild(count).gameObject.SetActive(true);
    }
    
    public void SetStarImageBasedOnCount()
    {
        if(this.GetComponent<Stage>().GetStageInfo() == null)
        {
            Debug.Log("StageInfo 정보가 존재하지 않음");
            return;
        }

        int count = this.GetComponent<Stage>().GetStageInfo().GetStarCount();

        int maxCount = 4;

        for (int i = 0; i < maxCount; i++)
        {
            smallStarsParent.GetComponent<Transform>().GetChild(0).GetChild(i).gameObject.SetActive(false);
            largeStarsParent.GetComponent<Transform>().GetChild(0).GetChild(i).gameObject.SetActive(false);
        }

        smallStarsParent.GetComponent<Transform>().GetChild(0).GetChild(count).gameObject.SetActive(true);
        largeStarsParent.GetComponent<Transform>().GetChild(0).GetChild(count).gameObject.SetActive(true);
    }
}