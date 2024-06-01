using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveData
{
    private static string s_Path = Application.dataPath + "HighScoreData.dat";

    public static void Save(HighScoreData highScoreData)
    {
        string saveData = JsonUtility.ToJson(highScoreData);

        File.WriteAllText(s_Path, saveData);
    }

    public static HighScoreData Load()
    {
        if (File.Exists(s_Path))
        {
            string saveData = File.ReadAllText(s_Path);
            return JsonUtility.FromJson<HighScoreData>(saveData);
        }

        return new HighScoreData();
    }
}
