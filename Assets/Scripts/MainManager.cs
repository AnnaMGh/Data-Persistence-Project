using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody[] Balls;
    public Transform BallInstantiatePosition;

    public Text ScoreText;
    public Text BestScoreText;
    public GameObject GameOverText;

    private bool m_Started = false;
    private int m_Points;

    private bool m_GameOver = false;


    // Start is called before the first frame update
    void Start()
    {

        AddBalls(SceneDataPersistence.Instance.BallNumber);

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(5.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-2.1f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        UpdateBestScore();
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;

                foreach (Rigidbody r in Balls)
                {
                    if (r != null)
                    {
                        float randomDirection = Random.Range(-1.0f, 1.0f);
                        Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                        forceDir.Normalize();
                        r.transform.SetParent(null);
                      //  r.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
                        r.AddForce(forceDir * (SceneDataPersistence.Instance.Speed+1f), ForceMode.VelocityChange);
                    }

                }

            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    private void AddBalls(int nr)
    {
        Balls = new Rigidbody[nr];
        for (int i = 0; i < nr; i++)
        {
            GameObject b = Resources.Load<GameObject>("Prefabs/Ball");
            Balls[i] = Instantiate(b, BallInstantiatePosition).GetComponent<Rigidbody>();


            if (i == 1)
            {
                Balls[i].transform.Translate(Vector3.right * 0.25f);
            }
            else if (i == 2)
            {
                Balls[i].transform.Translate(Vector3.left * 0.25f);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    void UpdateBestScore()
    {
        if (SceneDataPersistence.Instance.BestScore != null)
        {
            BestScoreText.text = "BEST SCORE: "
            + SceneDataPersistence.Instance.BestScore.name
            + ":" + SceneDataPersistence.Instance.BestScore.points;
        }
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);

        foreach (Rigidbody r in Balls)
        {
            if (r != null && r.gameObject != null)
            {
                Destroy(r.gameObject);
            }
        }

        if (SceneDataPersistence.Instance.BestScore == null
            || (SceneDataPersistence.Instance.BestScore != null
            && SceneDataPersistence.Instance.BestScore.points < m_Points))
        {
            string name;
            if (SceneDataPersistence.Instance.CurrentPlayerName == null || SceneDataPersistence.Instance.CurrentPlayerName.Length == 0)
            {
                name = "Anonymous";
            }
            else
            {
                name = SceneDataPersistence.Instance.CurrentPlayerName;
            }

            SceneDataPersistence.Instance.SaveData(new SceneDataPersistence.Score(name, m_Points));
        }

        UpdateBestScore();
    }

    public void OnClickBack()
    {
        SceneManager.LoadScene(0);
    }
}
