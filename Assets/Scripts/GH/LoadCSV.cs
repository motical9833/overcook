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

public class csvReader : MonoBehaviour
{
    public List<Tuple<string, int>> Read(string file) //list[문제 번호]=dic(문제, 정답 인덱스)
    {
        var list = new List<Tuple<string, int>>();
        TextAsset sourcefile = Resources.Load<TextAsset>("stage");
        StringReader sr = new StringReader(sourcefile.text);

        while (sr.Peek() > -1)
        {
            string data_String = sr.ReadLine();

            var data_values = data_String.Split(','); //string, string타입
            var tmp = new Tuple<string, int>(data_values[0], int.Parse(data_values[1])); //문제, 정답
            list.Add(tmp);
        }

        return list;
    }
}