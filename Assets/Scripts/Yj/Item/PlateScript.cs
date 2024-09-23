using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateScript : GrabAbleObjScript
{
    bool isDirty;

    public string plateFoodName;

    void Start()
    {
        base.Initialize();
    }

    public void PlateSoup(string soupIngredient)
    {
        plateFoodName = "Soup_" + soupIngredient;
    }

    public string GetPlateFoodName()
    {
        return plateFoodName;
    }

    void PlateFood(string foodName)
    {   

    }
}
