using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageSummaryScript : MonoBehaviour
{
    GameObject audioManager;
    GameObject gameManager;

    public TextMeshProUGUI[] unChangeingTexts;
    public TextMeshProUGUI[] changeableTexts;
    public Image[] starImgs;
    public GameObject maskObject;
    StageManagerScript stageManagerScript;

    float timer;

    private void Start()
    {
        changeableTexts = new TextMeshProUGUI[3];
        unChangeingTexts = new TextMeshProUGUI[3];
        starImgs = new Image[3];

        for (int i = 0; i < 3; i++)
        {
            unChangeingTexts[i] = this.transform.GetChild(1).GetChild(i).gameObject.GetComponent<TextMeshProUGUI>();
            changeableTexts[i] = this.transform.GetChild(2).GetChild(i).gameObject.GetComponent<TextMeshProUGUI>();
            starImgs[i] = this.transform.GetChild(3).GetChild(i).gameObject.GetComponent<Image>();
        }

        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        audioManager = GameObject.FindGameObjectWithTag("AudioManager");
        stageManagerScript = GameObject.FindGameObjectWithTag("StageManager").GetComponent<StageManagerScript>();

        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer > 5.0f)
        {
            if(Input.anyKeyDown)
            {
                stageManagerScript.GameClear();
            }
        }
    }

    public void SetSummaryTextUI(int orderDelivered, int tip, int failedCount)
    {
        int score = (orderDelivered * 20) + tip - (failedCount * 10);

        changeableTexts[0].text = (orderDelivered + " x " + 20 + " = " + orderDelivered * 20).ToString();
        changeableTexts[1].text = tip.ToString();
        changeableTexts[2].text = (failedCount + " x " + 10 + " = " + failedCount * 10).ToString();


        StartCoroutine(TextsAlphaCoroutine(score));
    }

    public IEnumerator TextsAlphaCoroutine(int score)
    {
        for (int i = 0; i < changeableTexts.Length;i++)
        {
            StartCoroutine(LerpAlphaToMax(unChangeingTexts[i], 1));
            StartCoroutine(LerpAlphaToMax(changeableTexts[i], 1));
        }

        yield return new WaitForSeconds(1);

        int[] testarr = { 20, 40, 60 };

        StartCoroutine(LerpAlphaToMax(starImgs, 1, score, testarr));
    }

    private IEnumerator LerpAlphaToMax(TextMeshProUGUI text,float duration)
    {
        Color color = text.color;
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            color.a = Mathf.Lerp(0, 1, t);
            text.color = color;
            yield return null;
        }
    }

    private IEnumerator LerpAlphaToMax(Image[] image, float duration,int score,int[] goal)
    {
        for (int i = 0; i < image.Length; i++)
        {
            if(score > goal[i])
            {
                Color color = image[i].color;
                float elapsedTime = 0.0f;

                while (elapsedTime < duration)
                {
                    elapsedTime += Time.deltaTime;
                    float t = Mathf.Clamp01(elapsedTime / duration);
                    color.a = Mathf.Lerp(0, 1, t);
                    image[i].color = color;
                    yield return null;
                }
            }
        }
    }
}