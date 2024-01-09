using System.IO;
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
            }
            return instance;
        }
    }

    public void AddPointToEnemiesKilled()
    {
        try { LoadData(); } catch { }
        this.enemiesKilled++;
        SaveData();
    }

    public void AddPointToBarriersDestroyed()
    {
        try { LoadData(); } catch { }
        this.barriersDestroyed++;
        SaveData();
    }

    private void SaveData()
    {
        string filePath = Application.persistentDataPath + "/scoring.json";
        string fileText = JsonUtility.ToJson(Scoring.Instance);
        File.WriteAllText(filePath, fileText);
    }

    public void LoadData()
    {
        string filePath = Application.persistentDataPath + "/scoring.json";
        string fileText = File.ReadAllText(filePath);
        Scoring score = JsonUtility.FromJson<Scoring>(fileText);
        Scoring.Instance.enemiesKilled = score.enemiesKilled;
        Scoring.Instance.barriersDestroyed = score.barriersDestroyed;
    }
}
