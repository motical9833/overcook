using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class IngredientData
{
    public string Ingredient { get; set; }
    public float OffsetX { get; set; }
    public float OffsetY { get; set; }
}

public class StageOrderData
{
    public string stageLevel { get; set; }
    public List<string> Orders { get; set; }
}

public static class CSVLoader
{
    public static List<T> LoadCSV<T>(string filepath, Func<string[], T> convertFunc)
    {
        var dataList = new List<T>();

        string[] lines = File.ReadAllLines(filepath);

        for (int i = 1; i < lines.Length; i++)
        {
            string[] values = lines[i].Split(',');

            T data = convertFunc(values);
            dataList.Add(data);
        }

        return dataList;
    }
}