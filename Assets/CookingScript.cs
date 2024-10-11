using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingScript : MonoBehaviour
{
    public GameObject activatedFood;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ActiveFood(GameObject food)
    {
        activatedFood = food;
        food.SetActive(true);
    }

    public void DeactivateFood()
    {
        activatedFood.SetActive(false);
        activatedFood = null;
    }

    public GameObject GetActivatedFood()
    {
        return activatedFood;
    }
}