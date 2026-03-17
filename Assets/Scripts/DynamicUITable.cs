using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class FixedWidthTable : MonoBehaviour
{
    public Canvas canvas;             // Assign your Canvas in Inspector
    public Sprite bgImage;
    public TMP_FontAsset fontAsset;   // Optional: assign TMP font
    public float column1Width = 150f;
    public float column2Width = 75;
    public float rowHeight = 30f;
    public float rowSpacing = 5f;

    void Start()
    {
        CreateTable(GameManager.Instance.listHighScores);
    }

    void CreateTable(List<GameManager.Score> data)
    {
        // 1. Table container
        GameObject table = new GameObject("Table");
        table.transform.SetParent(canvas.transform, false);
        //table.transform.position = new Vector3(20,0,0);
        VerticalLayoutGroup vLayout = table.AddComponent<VerticalLayoutGroup>();
        vLayout.childAlignment = TextAnchor.UpperCenter;
        vLayout.childControlWidth = false;
        vLayout.childForceExpandHeight = false;
        vLayout.childForceExpandWidth = false;
        vLayout.spacing = rowSpacing;

        ContentSizeFitter tableFitter = table.AddComponent<ContentSizeFitter>();
        tableFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

        // 2. Create rows
        foreach (var item in data)
        {
            GameObject row = new GameObject("Row");
            row.transform.SetParent(table.transform, false);

            HorizontalLayoutGroup hLayout = row.AddComponent<HorizontalLayoutGroup>();
            hLayout.childForceExpandWidth = false; // important for fixed width
            hLayout.childForceExpandHeight = false;
            hLayout.spacing = 10;
            hLayout.padding.left = 10;
            hLayout.padding.right = 10;            

            ContentSizeFitter rowFitter = row.AddComponent<ContentSizeFitter>();
            rowFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;

            Image image = row.AddComponent<Image>();
            image.sprite = bgImage;
            image.color = Color.gray;
            image.type = Image.Type.Sliced;



            // Column 1
            CreateCell(row.transform, item.name, column1Width, TextAlignmentOptions.Left);

            // Column 2
            CreateCell(row.transform, item.score.ToString(), column2Width, TextAlignmentOptions.Right);
        }
    }

    void CreateCell(Transform parent, string text, float width, TextAlignmentOptions align)
    {
        GameObject cell = new GameObject("Cell");
        cell.transform.SetParent(parent, false);

        TextMeshProUGUI tmp = cell.AddComponent<TextMeshProUGUI>();
        tmp.text = text;
        tmp.fontSize = 24;
        tmp.color = Color.white;
        tmp.alignment = align;
        if (fontAsset != null) tmp.font = fontAsset;

        // Set fixed width
        LayoutElement le = tmp.gameObject.AddComponent<LayoutElement>();
        le.preferredWidth = width;
    }
}