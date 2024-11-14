using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OrderUIControllerScript : MonoBehaviour
{
    float currentTime = 0.0f;
    float orderTime = 30.0f;

    bool isStart = false;

    GameObject orderPanal;
    

    void Start()
    {
        orderPanal = GameObject.FindWithTag("MainCanvas");

        if (orderPanal == null)
        {
            Debug.LogError("orderPanal이 비어있음!! 태그 확인필요...");
            return;
        }
    }

    void Update()
    {
        if (!isStart)
            return;

        currentTime += Time.deltaTime;

        //  orderTime이 되었을 때 || 현재 진행중인 주문이 0개가 되었을 때 즉시 주문생성
        if (currentTime >= orderTime || orderPanal.transform.GetChild(0).GetComponent
            <RecipeOrderControllerScript>().GetOrderCount() == 0)
        {
            orderPanal.transform.GetChild(0).GetComponent
                <RecipeOrderControllerScript>().FoodOrderComesIn(new Vector3(90.0f, 1030.0f, 0));
            currentTime = 0.0f;
        }
    }

    // 게임 시작할 때 시작되는 코루틴
    IEnumerator GameStartCorutine()
    {
        yield return new WaitForSeconds(3);

        orderPanal.transform.GetChild(0).GetComponent
            <RecipeOrderControllerScript>().FoodOrderComesIn(new Vector3(90.0f, 1030.0f, 0));

        isStart = true;
    }
    public void OrderStart()
    {
        StartCoroutine(GameStartCorutine());
    }

    public void ServingDishes(string orderName)
    {
        orderPanal.transform.GetChild(0).GetComponent<RecipeOrderControllerScript>().ServeFood(orderName);
    }

}
