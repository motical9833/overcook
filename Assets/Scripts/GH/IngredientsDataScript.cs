using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class IngredientsDataScript : MonoBehaviour
{
    public string csvFileName = "IngredientData.csv";
    private Dictionary<string, Vector2> ingredientOffsets;

    //CSVReader reader;

    private void Awake()
    {
        LoadCSVData();
        Vector2 onionOffset = GetIngredientOffset("Onion");
        //Debug.Log($"Onion Offset: {onionOffset}");

        //reader = new CSVReader();
    }

    private void LoadCSVData()
    {
        ingredientOffsets = new Dictionary<string, Vector2>();

        string csvFilePath = Path.Combine(Application.streamingAssetsPath, csvFileName);

        var result = CSVReader.ParseCSV(File.ReadAllText(csvFilePath));

        foreach (var line in result)
        {
            string key = line[0];

            // offsetX와 offsetY 변수를 초기화하고 변환 시도
            float offsetX = 0f;
            float offsetY = 0f;

            if (!float.TryParse(line[1], out offsetX))
            {
                Debug.Log("string 문자열 예외처리");
            }
            if (!float.TryParse(line[2], out offsetY))
            {
                Debug.Log("string 문자열 예외처리");
            }

            // 변환에 성공한 값만 Dictionary에 추가
            ingredientOffsets[key] = new Vector2(offsetX, offsetY);
        }
    }

    private IngredientData ConvertToIngredientData(string[] values)
    {
        if(values.Length == 3)
        {
            return new IngredientData
            {
                Ingredient = values[0],
                OffsetX = float.Parse(values[1]),
                OffsetY = float.Parse(values[2])
            };
        }
        else
        {
            Debug.LogError("유효하지 않은 데이터!!");
            return null;
        }
    }

    public Vector2 GetIngredientOffset(string ingredient)
    {
        if (ingredientOffsets.TryGetValue(ingredient, out Vector2 offset))
        {
            return offset;
        }
        else
        {
            return Vector2.zero;
        }
    }
}