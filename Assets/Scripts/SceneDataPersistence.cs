using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SceneDataPersistence : MonoBehaviour
{
    public static SceneDataPersistence Instance { get; private set; }
    public string CurrentPlayerName { get; private set; }
    public ScoreList ListOfScore { get; private set; }
    public Score BestScore { get; private set; }
    public int BallNumber { get; private set; }
    public float Speed { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            //DeleteData();

            BallNumber = 1;
            Speed = 1f;
            LoadData();
           
            DontDestroyOnLoad(Instance);
        }
    }

    public void SetPlayerName(string name) {
        CurrentPlayerName = name;
    }

    public void SetBallAndSpeed(int ball, float speed)
    {
        BallNumber = ball;
        Speed = speed;
    }

    public void SaveData(Score score)
    {
        if (ListOfScore == null)
        {
            ListOfScore = new ScoreList();
        }
        if (ListOfScore.list == null)
        {
            ListOfScore.list = new List<Score>();
        }

        BestScore = score;
        ListOfScore.list.Add(BestScore);

        string json = JsonUtility.ToJson(ListOfScore);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void SaveData(ScoreList scorel)
    {
        ListOfScore = scorel;
        string json = JsonUtility.ToJson(ListOfScore);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadData() {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
        
            ListOfScore = JsonUtility.FromJson<ScoreList>(json);
            if (ListOfScore.list.Count > 0)
            {
                BestScore = ListOfScore.list[ListOfScore.list.Count-1];
            }

            Debug.Log("List:");
            foreach(Score s in ListOfScore.list){
                Debug.Log(s.ToString());
            }
        }
    }

    public void DeleteData() {
        string[] filePaths = Directory.GetFiles(Application.persistentDataPath);
        foreach (string filePath in filePaths) File.Delete(filePath);
    }

    [System.Serializable]
    public class ScoreList {
        public List<Score> list;
    }

    [System.Serializable]
    public class Score {
       public string name = "_";
       public int points = 0;

        public Score(string name, int points) {
            this.name = name;
            this.points = points;
        }

        override public string ToString() {
            return "(" + name + "," + points + ")";
        }
    }


}
