using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingBoardScript : RaisedObjectScript
{
    public GameObject knife;
    bool isCutting = false;

    int cutters = 0;

    SliceGuageUIScript sliceGuageScr;

    private void Start()
    {
        sliceGuageScr = GetComponent<SliceGuageUIScript>();
    }

    private void Update()
    {
        if (cutters <= 0)
        {
            SetCutting(false);
            sliceGuageScr.DisableSliceUI();
            return;
        }
        if (GetRaisedObject() != null && GetRaisedObject().GetComponent<IngredientScript>().GetIsChopped())
        {
            SetCutting(false);
            sliceGuageScr.DisableSliceUI();
            return;
        }
        if (cutters > 0)
        {
            SetCutting(true);
            IngredientScript ingredientScr = GetRaisedObject().GetComponent<IngredientScript>();
            ingredientScr.Chopping();
            float guage = ingredientScr.GetSliceGuage();
            sliceGuageScr.ShowSliceUI(guage);
        }
    }

    public void AddCutter()
    {
        cutters += 1;
        Debug.Log(cutters);
    }


    public void RemoveCutter()
    {
        cutters -= 1;
        Debug.Log(cutters);
    }


    public void SetCutting(bool cutOnOff)
    {
        if (cutOnOff)
        {
            isCutting = true;
            knife.SetActive(false);
        }    
        else
        {
            isCutting = false;
            knife.SetActive(true);
        }
    }

    public bool GetChoppingIsDone()
    {
        return false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (isCutting)
            {
                Debug.Log("플레이어가 도마 위를 벗어남");
                SetCutting(false);
            }
        }
    }
}
