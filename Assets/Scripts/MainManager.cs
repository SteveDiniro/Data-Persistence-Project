using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text BestScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    public static MainManager Instance;

    public Text PlayerName;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this; // assign "this" (current) object to the Instance variable of type MainManager
        //DontDestroyOnLoad(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        // ********************* Toggle the comments on the next 3 lines, to reset any stored data **********************
        //PlayerPrefs.DeleteKey("HighScore");
        //PlayerPrefs.Save();
        //Debug.Log("High score reset.");

        //Debug.Log("Player's name is: " + PlayerData.PlayerName);

        //string playerName = PlayerPrefs.GetString("PlayerName", "Unknown");
        string bestName = PlayerPrefs.GetString("HighScoreName", "Unknown");
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        BestScoreText.text = "Best Score : " + bestName + " : " + highScore;

    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
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

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void SaveHighScore(int score)
    {
        int currentHigh = PlayerPrefs.GetInt("HighScore", 0);
        if (m_Points > currentHigh)
        {
            PlayerPrefs.SetInt("HighScore", m_Points);
            PlayerPrefs.SetString("HighScoreName", PlayerData.PlayerName);
            PlayerPrefs.Save();
        }
    }

    public void GameOver()
    {
        m_GameOver = true;
        int currentHigh = PlayerPrefs.GetInt("HighScore", 0);
        if (m_Points > currentHigh)
        {
            BestScoreText.text = "Best Score : " + PlayerData.PlayerName + " : " + m_Points;
        }

        SaveHighScore(m_Points); GameOverText.SetActive(true);
    }

    void OnApplicationQuit()
    {
        // Ensure high score is saved here too
        SaveHighScore(m_Points);
    }
}
