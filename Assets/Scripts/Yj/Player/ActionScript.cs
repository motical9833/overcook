using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Enummrous;

public class ActionScript : MonoBehaviour
{
    Vector3 grabPos;

    BoxCollider frontCol = null;

    GameObject hand = null; 
    
    GameObject currGrabObj = null; // 현재 내가 집어들고 있는 오브젝트
    GameObject currCuttingBoard = null; // 현재 내가 사용하고 있는 보드

    bool isGrab = false;
    bool isChop = false;

    //grab과 release는 space로 작동
    //Action은 leftCtrl로 작동

    PlayerAnimationScript pAnimScript;

    RecipeOrderControllerScript recipeOrderCtrlScr;

    public void InitialSet(BoxCollider _frontCol,GameObject _hand)
    {
        frontCol = _frontCol;
        hand = _hand;
        pAnimScript = GetComponent<PlayerAnimationScript>();
    }

  
    public PlayerAnimState CtrlAction()
    {
       

        Bounds fColBound = frontCol.bounds;
        Collider[] hitColliders = Physics.OverlapBox(fColBound.center, fColBound.extents, Quaternion.identity);

        string[] tags = { "Table", "IngredientBox", "Pot", "Ingredient", "Plate" };

        foreach (string tag in tags)
        {
            foreach (Collider collider in hitColliders)
            {
                if (collider.CompareTag(tag))
                {
                    switch (collider.tag)
                    {
                        case "Table":
                            TableScript tableScr = collider.GetComponent<TableScript>();
                            GameObject cuttingBoard = null;

                            if (tableScr.GetRaisedObject().tag == "CuttingBoard")
                            {
                                cuttingBoard = tableScr.GetRaisedObject();
                                if (tableScr.GetTopRaisedObj().tag == "Ingredient")
                                {
                                    if (tableScr.GetTopRaisedObj().GetComponent<IngredientScript>().GetIsChopped())
                                    {
                                        return PlayerAnimState.None;
                                    }
                                    else
                                    {
                                        tableScr.GetRaisedObject().GetComponent<CuttingBoardScript>().SetCutting(true);
                                        currCuttingBoard = cuttingBoard;
                                        currCuttingBoard.GetComponent<CuttingBoardScript>().AddCutter();
                                        isChop = true;
                                        return PlayerAnimState.Chop;
                                    }
                                }
                            }
                            break;
                    }
                }
            }
        }
        return PlayerAnimState.None;
    }


    public void CtrilHoldAction()
    {
        if (isGrab)
        {
            if (currGrabObj.tag == "Extinguisher")
            {
                var extinguisherScr = currGrabObj.GetComponent<FireExtinguisher>();
                extinguisherScr.SprayPowder(true);
                //extinguishd애니메이션 재생 및 extinguisher에 작동 스크립트 적용
            }
        }
    }

    public bool BarAction()
    {
        Debug.Log("Barpress");

        Bounds fColBound = frontCol.bounds;
        Collider[] hitColliders = Physics.OverlapBox(fColBound.center, fColBound.extents, Quaternion.identity);

        string[] tags = { "Table", "IngredientBox", "Pot", "Ingredient", "Plate", "CookingStation", "PlateStation" };

        if (isGrab)
        {
            #region"TryingRelease"

            foreach (string tag in tags)
            {
                foreach (Collider collider in hitColliders)
                {
                    if (collider.CompareTag(tag))
                    {
                        switch (tag)
                        {
                            case "Table":
                                TableScript tableScr = collider.GetComponent<TableScript>();
                                if (tableScr.IsRaised())
                                {
                                    switch (tableScr.GetTopRaisedObj().tag)
                                    {
                                        case "Pot":
                                            Debug.Log("Pot에 접근중");
                                            if (currGrabObj.tag == "Ingredient")
                                            {
                                                var ingredientScr = currGrabObj.GetComponent<IngredientScript>();
                                                var potScr = tableScr.GetRaisedObject().GetComponent<PotScript>();

                                                if (ingredientScr.GetIsChopped())
                                                {
                                                    if (potScr.PutIngredient(ingredientScr.GetOriginName()))
                                                    {
                                                        Release();
                                                        ingredientScr.gameObject.SetActive(false);
                                                        return true;
                                                    }
                                                }
                                                else
                                                {
                                                    Debug.Log("썰리지 않은 재료를 투입하려고자 함");
                                                }
                                            }
                                            return false;
                                        case "Plate":
                                            var plateScr = tableScr.GetTopRaisedObj().GetComponent<PlateScript>();
                                            if(currGrabObj.tag =="Pot")
                                            {
                                                var potScr = currGrabObj.GetComponent<PotScript>();
                                                if (potScr.GetIsCookedDone())
                                                {
                                                    potScr.PlatingSoup(plateScr);
                                                }
                                            }
                                            Debug.Log("재료를 접시에 넣음");

                                            return false;
                                        case "CuttingBoard":
                                            if (currGrabObj.tag == "Ingredient")
                                            {
                                                tableScr.GetRaisedObject().GetComponent
                                                    <RaisedObjectScript>().RaisObject(currGrabObj);
                                                Release();
                                                return true;
                                            }
                                            Debug.Log("재료를 도마에 넣음");
                                            return false;
                                    }
                                    //만약 테이블에 올려진것이 Pot이나 Plate라면 true 리턴하고 계속 작성
                                    return false;
                                }
                                else
                                {
                                    tableScr.RaisObject(currGrabObj);
                                    Release();
                                    return true;
                                }
                            case "CookingStation":
                                var cookingStationScr = collider.transform.GetComponent<CookingStationScript>();
                                if (cookingStationScr.GetRaisedObject() == null)
                                {
                                    if (currGrabObj.tag == "Pot")
                                    {
                                        tableScr = collider.GetComponent<TableScript>();
                                        tableScr.RaisObject(currGrabObj);
                                        Release();
                                        return true;
                                    }
                                    if (currGrabObj.tag == "Flyingpan")
                                    {
                                        tableScr = collider.GetComponent<TableScript>();
                                        tableScr.RaisObject(currGrabObj);
                                        Release();
                                        return true;
                                    }
                                }
                                else
                                {
                                    if (cookingStationScr.GetRaisedObject().tag == "Pot")
                                    {
                                        if (currGrabObj.tag == "Ingredient")
                                        {
                                            var ingredientScr = currGrabObj.GetComponent<IngredientScript>();
                                            var potScr = cookingStationScr.GetRaisedObject().GetComponent<PotScript>();

                                            if (ingredientScr.GetIsChopped())
                                            {
                                                if (potScr.PutIngredient(ingredientScr.GetOriginName()))
                                                {
                                                    Release();
                                                    ingredientScr.gameObject.SetActive(false);
                                                    return true;
                                                }
                                            }
                                            else
                                            {
                                                Debug.Log("썰리지 않은 재료를 투입하려고자 함");
                                            }
                                        }
                                    }
                                }
                               
                                return false;
                            case "PlateStation":
                                if (currGrabObj.tag == "Plate")
                                {
                                    var plateScr = currGrabObj.GetComponent<PlateScript>();
                                    string platedFoodName =  plateScr.GetPlateFoodName();
                                    if (platedFoodName == "")
                                    {
                                        Debug.Log("빈 식기를 내려고 시도함");
                                        return false;
                                    }
                                    else
                                    {
                                        Debug.Log("음식이 든 식를 제출함");
                                        recipeOrderCtrlScr = GameObject.FindGameObjectWithTag("MainCanvas").transform.GetChild(0).GetComponent<RecipeOrderControllerScript>();
                                        recipeOrderCtrlScr.ServeFood(platedFoodName);
/*                                        if (recipeOrderCtrlScr != null) 
                                        {
                                            recipeOrderCtrlScr = GameObject.FindGameObjectWithTag("MainCanvas").transform.GetChild(0).GetComponent<RecipeOrderControllerScript>();
                                            recipeOrderCtrlScr.ServeFood(platedFoodName);
                                            if (recipeOrderCtrlScr.CompareWithRecipeName(platedFoodName))
                                            {
                                                //recipeOrderCtrlScr.RequestMatchRemoveQueue();
                                                //점수랑 연결
                                            }
                                        }*/
                                    }
                                }
                                return false;
                        }
                    }
                }
            }
            #endregion
            return false;
        }
        #region"TryingGrab"
        else
        {
            foreach (string tag in tags)
            {
                foreach (Collider collider in hitColliders)
                {
                    switch (collider.transform.gameObject.tag)
                    {
                        case "Table":
                            TableScript tableScr = collider.GetComponent<TableScript>();
                            IngredientScript ingredientScr;
                            if (tableScr.IsRaised())
                            {
                                switch (tableScr.GetTopRaisedObj().tag)
                                {
                                    case "Pot":
                                        currGrabObj = tableScr.GetTopRaisedObj();
                                        Grab(currGrabObj);
                                        tableScr.GetTopRaisedScr().Release();
                                        return true;
                                    case "CuttingBoard":
                                        var cuttingBoardScr = tableScr.GetRaisedObject().GetComponent<CuttingBoardScript>();
                                        if (cuttingBoardScr.raisedObj == null)
                                        {
                                            return false;
                                        }

                                        ingredientScr = cuttingBoardScr.raisedObj.GetComponent<IngredientScript>();
                                        if (!ingredientScr.GetIsChopped())
                                        {
                                            if (ingredientScr.GetIsFisrtSlice())
                                            {
                                                return false;
                                            }
                                        }
                                        currGrabObj = cuttingBoardScr.GetRaisedObject();
                                        cuttingBoardScr.Release();
                                        Debug.Log("릴리스");
                                        Grab(currGrabObj);
                                        return true;
                                    case "Ingredient":
                                        currGrabObj = tableScr.GetTopRaisedObj();
                                        ingredientScr = currGrabObj.GetComponent<IngredientScript>();
                                        if (ingredientScr.GetIsChopped())
                                        {
                                            ingredientScr.Gather();
                                        }
                                        Grab(currGrabObj);
                                        tableScr.GetTopRaisedScr().Release();
                                        return true;
                                    case "Plate":
                                        Debug.Log("식기를 집으려 함");
                                        currGrabObj = tableScr.GetTopRaisedObj();
                                        Grab(currGrabObj);
                                        tableScr.GetTopRaisedScr().Release();
                                        return true;
                                    case "Extinguisher":
                                        currGrabObj = tableScr.GetTopRaisedObj();
                                        Grab(currGrabObj);
                                        tableScr.GetTopRaisedScr().Release();
                                        return false;
                                }
                                return false;
                            }
                            return false;
                        case "CookingStation":
                            tableScr = collider.GetComponent<TableScript>();
                            if (tableScr.GetRaisedObject() == null)
                            {
                                return false;
                            }
                            else
                            {
                                currGrabObj = tableScr.GetRaisedObject();
                                Grab(currGrabObj);
                                tableScr.Release();
                                return true;
                            }
                        case "IngredientBox":
                            IngredientObjectPoolScript ingObjPoolSrc;
                            ingObjPoolSrc = collider.GetComponent<IngredientObjectPoolScript>();
                            currGrabObj = ingObjPoolSrc.GetIngredientPoolObject();
                            Grab(currGrabObj);
                            return true;
                    }
                }
            }
            #endregion
        }
        return false;
    }

    public bool GetChopIsDone()
    {
        if (isChop) // 썰고 있는 중이라면
        {
            if (currCuttingBoard == null)
                Debug.LogError("썰고 있음에도 불구하고 cuttingboard가 null입니다");

            var boardScr = currCuttingBoard.GetComponent<CuttingBoardScript>();

            if (!CollisionCheck.Check(frontCol.bounds, currCuttingBoard))
            {
                boardScr.RemoveCutter();
                currCuttingBoard = null;
                isChop = false;
                return true;
                //이곳을 작성해야함
            }
            if (boardScr.GetRaisedObject().GetComponent<IngredientScript>().GetIsChopped())
            {
                boardScr.RemoveCutter();
                currCuttingBoard = null;
                isChop = false;
                return true;
            }
            return false;
        }
        else
        {
            return true;
        }
    }
    
    public void Grab(GameObject _grabObj) 
    {
        currGrabObj = _grabObj;
        currGrabObj.transform.position = hand.transform.position;
        currGrabObj.transform.SetParent(hand.transform);
        currGrabObj.SendMessage("Grabbed");
        isGrab = true;
    }   

    public void Release()
    {
        currGrabObj.SendMessage("Release");
        currGrabObj.transform.parent = null;
        currGrabObj = null;
        isGrab = false;
    }

}
