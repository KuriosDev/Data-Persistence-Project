using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HighScores : MonoBehaviour
{

    [SerializeField] GameObject table;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject textObj = new()
        {
            name = "Name1"
        };
        textObj.transform.position = new Vector3(0,0,0);

        TextMeshProUGUI textCmp = textObj.AddComponent<TextMeshProUGUI>();
        textCmp.text = "Albert";

        textObj.transform.SetParent(table.transform, false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
