using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    [SerializeField] TMP_InputField playerNameInputField;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerNameInputField.onValueChanged.AddListener(OnInputChanged);
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



    public void OnQuitButtonClick()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
