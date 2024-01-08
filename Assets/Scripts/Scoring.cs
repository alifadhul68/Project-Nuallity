using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Presets;
using UnityEngine;

[System.Serializable]
public sealed class Scoring
{
    public int enemiesKilled = 0;
    public int barriersDestroyed = 0;

    private static Scoring instance = null;

    private Scoring() { }

    public static Scoring Instance
    {
        get 
        { 
            if (instance == null)
            {
                instance = new Scoring();
                try { LoadData(); } catch { }
            }
            return instance;
        }
    }

    public void AddPointToEnemiesKilled()
    {
        this.enemiesKilled++;
        SaveData();
    }

    public void AddPointToBarriersDestroyed()
    {
        this.barriersDestroyed++;
        SaveData();
    }

    private void SaveData()
    {
        string filePath = Application.persistentDataPath + "/scoring.json";
        string fileText = JsonUtility.ToJson(Scoring.Instance);
        File.WriteAllText(filePath, fileText);
    }

    private static void LoadData()
    {
        string filePath = Application.persistentDataPath + "/scoring.json";
        string fileText = File.ReadAllText(filePath);
        Scoring score = JsonUtility.FromJson<Scoring>(fileText);
        Scoring.Instance.enemiesKilled = score.enemiesKilled;
        Scoring.Instance.barriersDestroyed = score.barriersDestroyed;
    }
}
