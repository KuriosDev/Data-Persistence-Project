using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string playerName;
    public string bestScoreName;
    public int bestScore;
    public List<Score> listHighScores;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadGameData();
    }

    [Serializable]
    public class Score : IComparable<Score>
    {
        public string name;
        public int score;

        public int CompareTo(Score other)
        {
            return -score.CompareTo(other.score);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    void LoadGameData()
    {
        Debug.Log("Loading data...");
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            playerName = data.lastPlayerName;
            bestScoreName = data.bestScore_Name;
            bestScore = data.bestScore_Score;
            listHighScores = data.listHighScores;
        }
    }

    public void SaveGameData()
    {
        SaveData data = new SaveData();

        data.lastPlayerName = playerName;
        data.bestScore_Name = bestScoreName;
        data.bestScore_Score = bestScore;

        BuildListHighScores();

        data.listHighScores = listHighScores;
        Debug.Log("Count: " + listHighScores.Count);

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void EvaluateScore(string name, int score)
    {
        Score lastHS = listHighScores.LastOrDefault<Score>();
        
        if (lastHS == null || score > lastHS.score)
        {
            Score scoreTmp = new()
            {
                name = name,
                score = score
            };

            listHighScores.Add(scoreTmp);
        }
    }

    private void BuildListHighScores()
    {
        listHighScores = listHighScores.OrderByDescending(i => i.score)
                                       .ThenBy(i => i.name)
                                       .Take(5)
                                       .ToList();
    }

    [Serializable]
    private class SaveData
    {
        public string lastPlayerName;
        public string bestScore_Name;
        public int bestScore_Score;

        public List<Score> listHighScores;
    }
}
