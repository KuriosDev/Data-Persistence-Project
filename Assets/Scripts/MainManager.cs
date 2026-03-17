using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    [SerializeField] AudioClip audioGameOver;
    private AudioSource m_AudioSource;

    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text PlayerNameText;
    public Text ScoreText;
    public Text BestScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance == null)
        {
            SceneManager.LoadScene(0);
        }

        m_AudioSource = GetComponent<AudioSource>();

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

        PlayerNameText.text = GameManager.Instance.playerName;
        UpdateBestScoreText();
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
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(0);
            }
       }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);

        m_AudioSource.PlayOneShot(audioGameOver);

        if (m_Points > GameManager.Instance.bestScore)
        {
            GameManager.Instance.bestScoreName = GameManager.Instance.playerName;
            GameManager.Instance.bestScore = m_Points;
            
            UpdateBestScoreText();
        }

        GameManager.Instance.EvaluateScore(GameManager.Instance.playerName, m_Points);

        GameManager.Instance.SaveGameData();
    }

    private void UpdateBestScoreText()
    {
        string name = GameManager.Instance.bestScoreName;
        int score = GameManager.Instance.bestScore;

        if (!string.IsNullOrEmpty(name))
        {
            BestScoreText.text = $"Best Score : {name} : {score}";          
        }
    }
}
