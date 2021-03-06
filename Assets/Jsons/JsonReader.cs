﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public enum Qtype
{
    Text,
    Formula,
    TextAndFormula
}
public class JsonReader : MonoBehaviour
{
    public static string path;
    //public static string fileName;

    public static Questions[] questions;

    [System.Serializable]
    public struct Questions
    {
        public string questionName;
        public string question;
        public string formula;
        public Qtype IsQuestionFormula;
        public string[] answers;
        public bool IsAnswersFormula;
        public int courrectAnswer;
    }

    void Start()
    {
        //path = Application.dataPath + "/Jsons/";
        //fileName = "TryJson";
        //ReadData();
        //SaveData(questions);
    }
    public static void SetUp(string packName)
    {
        path = Application.dataPath + "/Jsons/";
        //fileName = "TryJson";
        ReadData(packName);
        //ReadData("TryJson");
    }
    private static void ReadData(string packName)
    {

        string jsonStr = ((TextAsset)Resources.Load("Jsons/"+ packName, typeof(TextAsset))).text;
        //string jsonStr = File.ReadAllText(path + fileName + ".json");
        questions = JsonHelper.getJsonArray<Questions>(jsonStr);
    }

    private void SaveData(Questions[] questions)
    {
        string stringJson = "[";
        foreach (Questions q in questions)
        {
            stringJson += JsonUtility.ToJson(q) + ",";
        }
        stringJson = stringJson.Remove(stringJson.Length - 1);
        stringJson += "]";
        File.WriteAllText(Application.dataPath + "/saveFile.json", stringJson);
    }
}

public class JsonHelper
{
    public static T[] getJsonArray<T>(string json)
    {
        string newJson = "{ \"array\": " + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
        return wrapper.array;
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] array;
    }
}