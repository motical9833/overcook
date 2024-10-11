using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateScript : GrabAbleObjScript
{
    bool isDirty;

    public string plateFoodName;

    GameObject cookingParent;


    void Start()
    {
        base.Initialize();

        cookingParent = this.transform.GetChild(1).gameObject;
    }

    public void PlateSoup(string soupIngredient)
    {
        plateFoodName = "Soup_" + soupIngredient;
        GameObject cooking = cookingParent.transform.GetChild(0).gameObject;
        cookingParent.GetComponent<CookingScript>().ActiveFood(cooking);
    }

    public string GetPlateFoodName()
    {
        return plateFoodName;
    }

    void PlateFood(string foodName)
    {   

    }

    public void Reset()
    {
        cookingParent.GetComponent<CookingScript>().DeactivateFood();
    }
}
