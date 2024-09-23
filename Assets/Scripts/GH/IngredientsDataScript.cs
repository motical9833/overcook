using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class IngredientsDataScript : MonoBehaviour
{
    public string csvFilePath = "Assets/Resources/IngredientData/IngredientData.csv";
    private Dictionary<string, Vector2> ingredientOffsets;

    private void Awake()
    {
        LoadCSVData();
        Vector2 onionOffset = GetIngredientOffset("Onion");
        //Debug.Log($"Onion Offset: {onionOffset}");
    }

    private void LoadCSVData()
    {
        ingredientOffsets = new Dictionary<string, Vector2>();

        var ingredientDataList = CSVLoader.LoadCSV<IngredientData>(csvFilePath, ConvertToIngredientData);

        foreach(var data in ingredientDataList)
        {
            ingredientOffsets[data.Ingredient] = new Vector2(data.OffsetX, data.OffsetY);
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