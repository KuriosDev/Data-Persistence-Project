using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI bestScoreText;
    [SerializeField] TMP_InputField playerNameInputField;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!string.IsNullOrEmpty(GameManager.Instance.bestScoreName))
        {
            bestScoreText.text = $"Best Score: {GameManager.Instance.bestScoreName} : {GameManager.Instance.bestScore}";
        }

        playerNameInputField.onValueChanged.AddListener(OnInputChanged);

        playerNameInputField.text = GameManager.Instance.playerName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnInputChanged(string value)
    {
        ColorBlock cb = playerNameInputField.colors;
        cb.normalColor = Color.white;
        playerNameInputField.colors = cb;

        GameManager.Instance.playerName = value;
    }

    public void OnStartButtonClick()
    {
        if (playerNameInputField.text == "")
        {
            ColorBlock cb = playerNameInputField.colors;
            cb.normalColor = Color.softRed;
            playerNameInputField.colors = cb;

            return;    
        }

        GameManager.Instance.playerName = playerNameInputField.text;

        SceneManager.LoadScene(1);
    }

    public void OnHighScoresButtonClick()
    {
        SceneManager.LoadScene(2);
    }

    public void OnQuitButtonClick()
    {
        GameManager.Instance.SaveGameData();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
