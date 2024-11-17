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

    // 활성화된 레시피 리스트
    public List<Recipe> recipeClass = new List<Recipe>();

    // 대기 중인 레시피 큐
    public Queue<Recipe> recipeQueue = new Queue<Recipe>();

    public int maxOrderCnt = 5;
    int orderCnt = 0;
    bool isFull = false;

    StagePointScript stagePointScript;
    StageSummaryControllerScript stageSummaryControllerScript;
    GameObject gameManager;
    void Start()
    {
        InitializeGameManager();
        InitializeStageScripts();

        List<string> recipes = LoadRecipes();
        if (recipes != null && recipes.Count > 0)
        {
            SpawnRecipe(recipes);
        }
    }
    
    private bool InitializeGameManager()
    {
        gameManager = GameObject.FindWithTag("GameManager");

        if(gameManager == null)
        {
            Debug.LogError("GameManager를 찾을 수 없음");
            return false;
        }

        return true;
    }

   private void InitializeStageScripts()
    {
        stagePointScript = transform.parent.GetChild(1).GetComponent<StagePointScript>();

        if(stagePointScript == null)
        {
            Debug.LogWarning("StagePointScript를 찾을 수 없음");
        }

        stageSummaryControllerScript = GameObject.FindWithTag("StageManager").GetComponent<StageSummaryControllerScript>();

        if (stageSummaryControllerScript == null)
        {
            Debug.LogWarning("StageSummaryControllerScript를 찾을 수 없습니다.");
        }
    }

    // 현재 스테이지에서 사용될 레시피를 로드하는 메서드
    private List<string> LoadRecipes()
    {
        if (gameManager == null)
            return null;

        string sceneName = SceneManager.GetActiveScene().name;
        var recipes = gameManager.GetComponent<CookingSchedulerScript>().GetOrdersData(sceneName);

        if(recipes == null || recipes.Count == 0)
        {
            Debug.LogWarning("레시피 데이터를 찾을 수 없습니다.");
        }

        return recipes;
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

    // 레시피 생성 메서드
    private void SpawnRecipe(List<string> recipes)
    {
        Debug.Log("SpawnRecipe 시작");

        Transform parentTr = transform.GetChild(0);

        if(parentTr == null)
        {
            Debug.Log("parentTr를 찾을 수없음");
            return;
        }

        // 각 레시피당 5개씩 레시피 UI(prefab) 생성
        foreach (var recipeName in recipes)
        {
            GameObject prefab = Resources.Load<GameObject>($"GHPrefabs/Foods/{recipeName}");
            if (prefab == null)
            {
                Debug.LogWarning($"프리팹 {recipeName}을(를) 찾을 수 없습니다.");
                continue;
            }

            // 레시피 생성
            for (int j = 0; j < 5; j++)
            {
                GameObject ob = Instantiate
                    (prefab, parentTr.position, Quaternion.identity, parentTr);
                ob.name = recipeName;

                Recipe recipe = new Recipe(ob, 0);
                recipeQueue.Enqueue(recipe);
            }
        }
    }
    
    // 생성한 레시피를 스폰시키고 이동시키는 메서드
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

        Recipe servefood = FindRecipe(name);

        if(servefood == null)
        {
            Debug.LogWarning("해당 음식이 없음!");
            return;
        }

        if(stagePointScript == null)
        {
            Debug.LogWarning("stagePointScript가 존재하지 않음");
        }

        stagePointScript.SetPoint(20);

        ResetRecipe(servefood);
        recipeQueue.Enqueue(servefood);
        recipeClass.Remove(servefood);

        orderCnt--;
        isFull = false;

        stageSummaryControllerScript.SetOrderDelivered(1);
        OrderUIRelocation();
    }

    private Recipe FindRecipe(string name)
    {
        Recipe serveFood = null;


        foreach(var recipe in recipeClass)
        {
            if (recipe.RecipeUIObject.name != name)
                continue;

            if (serveFood == null ||
                serveFood.RecipeUIObject.GetComponent<OrderUIScript>().GetCurrentTime() >
                recipe.RecipeUIObject.GetComponent<OrderUIScript>().GetCurrentTime())
            {
                serveFood = recipe;
            }
        }

        return serveFood;
    }

    private void ResetRecipe(Recipe recipe)
    {
        recipe.OrderNumber = 0;
        recipe.RecipeUIObject.GetComponent<OrderUIScript>().ResetTimer();
        recipe.RecipeUIObject.GetComponent<RecipeUIMoveEffectScript>().ResetUIPos();
        recipe.RecipeUIObject.SetActive(false);
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