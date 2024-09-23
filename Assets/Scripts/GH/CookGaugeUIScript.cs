using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CookGaugeUIScript : MonoBehaviour
{
    public float currentTime = 0.0f;
    public float cookingTime = 30.0f;
    Slider slider;
    public float choppingGauge;
    bool isSlice = false;

    void Start()
    {
        slider = this.gameObject.GetComponent<Transform>().GetChild(0).GetComponent<Slider>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            HandleIngredientChopping();
        }


        if (!isSlice)

            return;

        currentTime += Time.deltaTime;

        float ratio = Mathf.Clamp01(currentTime / cookingTime);

        slider.value = ratio;
    }


    public void HandleIngredientChopping()
    {
        slider.value += choppingGauge;
    }

    public void CompleteIngredientChop()
    {
        //로직 작성 필요
    }
}