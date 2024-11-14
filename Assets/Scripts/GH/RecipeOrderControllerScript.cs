using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class RecipeOrderControllerScript : MonoBehaviour
{
    public class Recipe
    {
        GameObject recipeUIObject;
        public GameObject RecipeUIObject
        {
            get { return recipeUIObject; }
            private set { recipeUIObject = value; }
        }

        int orderNumber;
        public int OrderNumber
        {
            get { return orderNumber; }
            set { orderNumber = value; }
        }

        public Recipe(GameObject recipe, int number)
        {
            recipeUIObject = recipe;
            orderNumber = number;
        }

        public Recipe(Recipe recipe)
        {
            recipeUIObject = recipe.recipeUIObject;
            orderNumber = recipe.orderNumber;
        }
    }

    //public GameObject gameManager;

    public List<Recipe> recipeClass = new List<Recipe>();

    public Queue<Recipe> recipeQueue = new Queue<Recipe>();

    public int maxOrderCnt = 5;
    int orderCnt = 0;
    bool isFull = false;

    StagePointScript stagePointScript;
    StageSummaryControllerScript stageSummaryControllerScript;
    void Start()
    {
        GameObject gameManager = GameObject.FindWithTag("GameManager");

        string name = SceneManager.GetActiveScene().name;

        if (gameManager == null)
        {
            Debug.Log("GameManager를 찾을 수 없음");
            return;
        }

        List<string> recipes = gameManager.GetComponent<CookingSchedulerScript>().GetOrdersData(name);

        if (recipes.Count == 0 || recipes == null)
        {
            Debug.Log("recipes를 찾을 수 없음");
        }

        if (recipes != null && recipes.Count > 0)
        {
            SpawnRecipe(recipes);
        }

        stagePointScript = this.transform.parent.GetChild(1).GetComponent<StagePointScript>();

        if(stagePointScript == null)
        {
            Debug.Log("stagePointScript를 찾을 수 없음");
        }

        stageSummaryControllerScript = GameObject.FindWithTag("StageManager").GetComponent<StageSummaryControllerScript>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            FoodOrderComesIn(new Vector3(90.0f, 1030.0f, 0));
        }

        if(Input.GetKeyDown(KeyCode.B))
        {
            ServeFood("Soup_Onion");
        }
    }

    private void SpawnRecipe(List<string> recipes)
    {
        Debug.Log("SpawnRecipe 시작");

        Transform parentTr = this.gameObject.GetComponent<Transform>().GetChild(0);

        if(parentTr == null)
        {
            Debug.Log("parentTr를 찾을 수없음");
        }
        else
        {
            Debug.Log("parentTr를 찾음 ! :" + parentTr);
        }

        for (int i = 0; i < recipes.Count; i++)
        {
            string recipeName = recipes[i];

            GameObject prefab = Resources.Load<GameObject>("GHPrefabs/Foods/" + recipeName);

            if(prefab == null)
            {
                Debug.Log("프리팹을 찾을 수 없음");
            }
            else
            {
                Debug.Log("프리팹을 찾음! : " + prefab);
            }

            if (prefab != null)
            {
                for (int j = 0; j < 5; j++)
                {
                    GameObject ob = Instantiate(prefab, parentTr.position, Quaternion.identity, parentTr);
                    ob.name = recipeName;

                    Recipe recipe = new Recipe(ob, 0);
                    recipeQueue.Enqueue(recipe);
                }
            }
        }
    }
    
    // 레시피 생성 메서드
    public void FoodOrderComesIn(Vector3 targetPos)
    {
        if (isFull)
        {
            return;
        }

        // Order의 위치값
        targetPos.x += 210 * orderCnt;

        Recipe recipe = recipeQueue.Dequeue();

        recipeClass.Add(recipe);
        recipe.RecipeUIObject.SetActive(true);
        recipe.RecipeUIObject.GetComponent<RecipeUIMoveEffectScript>().UiMoveEvent(targetPos);
        recipe.OrderNumber = recipeClass.Count;

        orderCnt++;

        if (orderCnt >= maxOrderCnt)
        {
            isFull = true;
        }

    }

    public void ServeFood(string name)
    {
        if (orderCnt <= 0)
            return;

        Recipe servefood = null;

        for (int i = 0; i < recipeClass.Count; i++)
        {
            if (recipeClass[i].RecipeUIObject.name != name)
                continue;

            if (servefood == null || servefood.RecipeUIObject.GetComponent<OrderUIScript>().GetCurrentTime() > recipeClass[i].RecipeUIObject.GetComponent<OrderUIScript>().GetCurrentTime())
            {
                servefood = recipeClass[i];
            }
        }

        if(servefood == null)
        {
            Debug.Log("해당 음식이 없음!");
            return;
        }

        if(stagePointScript == null)
        {
            Debug.Log("stagePointScript가 없음");
        }
        else
        {
            Debug.Log("stagePointScript가 존재함");
        }

        stagePointScript.SetPoint(20);

        servefood.OrderNumber = 0;
        servefood.RecipeUIObject.GetComponent<OrderUIScript>().ResetTimer();
        servefood.RecipeUIObject.GetComponent<RecipeUIMoveEffectScript>().ResetUIPos();
        servefood.RecipeUIObject.SetActive(false);

        recipeQueue.Enqueue(servefood);
        recipeClass.Remove(servefood);

        orderCnt--;
        isFull = false;

        stageSummaryControllerScript.SetOrderDelivered(1);
        OrderUIRelocation();
    }

    public bool IsFull()
    {
        return isFull;
    }

    public int GetOrderCount()
    {
        return orderCnt;
    }


    private void OrderUIRelocation()
    {
        int count = 0;

        for (int i = 0; i < recipeClass.Count; i++)
        {
            if (recipeClass[i].RecipeUIObject.activeSelf)
            {
                count++;

                recipeClass[i].RecipeUIObject.GetComponent<RecipeUIMoveEffectScript>().PositionUIElements(new Vector3(90.0f, 1030.0f, 0), i);

                if(count == orderCnt)
                    break;
            }
        }
    }

    public bool CompareWithRecipeName(string _name)
    {
        if(recipeQueue.Count<=0)
        {
            return false;
        }

        if(recipeQueue.Peek().RecipeUIObject.name == _name)
        {
            return true;
        }

        Debug.LogWarning("레시피에 있는 이름과 일치 하지 않습니다.  완성된 음식의 이름과 일치하는지 확인 해 주세요.");
        return false;
    }

    public void RequestMatchRemoveQueue()
    {
        if (recipeQueue.Count <= 0)
        {
            Debug.LogWarning("레시피 큐에 아무것도 들어있지 않은데 삭제를 시도 했습니다.");
            return;
        }
        recipeQueue.Dequeue();
    }
}