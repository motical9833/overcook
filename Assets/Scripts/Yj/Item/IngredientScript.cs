using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Enummrous;
using static Enummrous.IngredientInfo;

public class IngredientScript : GrabAbleObjScript
{
    bool isChopped = false;

    Animator animator;
    public string controllerPath = "AnimatorController";  // Resources 폴더 안의 경로 (확장자 제외)
    
    float sliceGuage;

    private GameObject wholeObj;
    private GameObject sliceObj;
    bool isFirstSlice = false;

    private string originName;
    
    public override void Initialize()
    {
        base.Initialize();
        SetAnimator();

        originName = gameObject.name.Replace("(Clone)", "");
        switch (originName)
        {
            case "Onion":
                wholeObj = transform.GetChild(0).Find("Onion_Mesh").transform.Find("Onion_Whole").gameObject;
                sliceObj = transform.GetChild(0).Find("Onion_Mesh").transform.Find("Onion_Sliced").gameObject;
                sliceObj.SetActive(false);
                break;
            case "Mushroom":
                wholeObj = transform.GetChild(0).Find("MushRoom_Mesh").transform.Find("MushRoom_Whole").gameObject;
                sliceObj = transform.GetChild(0).Find("MushRoom_Mesh").transform.Find("MushRoom_Sliced").gameObject;
                sliceObj.SetActive(false);
                break;
            case "Beef":
                wholeObj = null;
                sliceObj = null;
                break;
        }
    }
  
    private void SetAnimator()
    {
        animator = transform.AddComponent<Animator>();
        string name = gameObject.name;
        string path = name.Replace("(Clone)", "");
        RuntimeAnimatorController controller = Resources.Load<RuntimeAnimatorController>(controllerPath + "/" + path);
        animator.runtimeAnimatorController = controller;
    }
    public string GetOriginName() { return originName; }

    public bool GetIsBoiled()
    {
        foreach (IngredientElements element in Ingredients)
        {
            if (originName == element.name)
            {
               return element.GetIsBoiled();
            }
        }
        return false;
    }
    public void Gather()
    {
        if (!isChopped)
            return;
        animator.SetTrigger("Gather");
    }

    public void Drop()
    {
        animator.SetTrigger("Drop");
    }

    public void Chopping()
    {
        if (!isChopped)
        {
            sliceGuage += Time.deltaTime * 33;
            animator.SetFloat("Slice", sliceGuage);
            if (!isFirstSlice && sliceGuage <= 33)
            {
                isFirstSlice = true;
                wholeObj.SetActive(false);
                sliceObj.SetActive(true);
            }
            if (sliceGuage >= 100)
            {
                isFirstSlice = false;
                isChopped = true;
            }
        }
    }
    public float GetSliceGuage()
    {
        return sliceGuage;
    }

    public bool GetIsFisrtSlice()
    {
        return isFirstSlice;
    }
    public bool GetIsChopped()
    {
        return isChopped;
    }
}
