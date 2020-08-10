using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class StatsManager
{
    public static string SaveFilePath { get; set; }


    public static void SaveCharacterStats(CharacterStats_SO characterStats_SO)
    {
        string json = JsonUtility.ToJson(characterStats_SO);
        File.WriteAllText(SaveFilePath, json);
    }


    public static void LoadCharacterStats(CharacterStats_SO characterStats_SO)
    {
        if(File.Exists(SaveFilePath))
        {
            string json = File.ReadAllText(SaveFilePath);
            JsonUtility.FromJsonOverwrite(json, characterStats_SO);
        }
    }
}
