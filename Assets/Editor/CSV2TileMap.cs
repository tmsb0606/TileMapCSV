using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
public class CSV2TileMap : EditorWindow
{
    [MenuItem("MAP/CSV2TileMap")] // ヘッダメニュー名/ヘッダ以下のメニュー名
    private static void ShowWindow()
    {
        var window = GetWindow<CSV2TileMap>("UIElements");
        window.titleContent = new GUIContent("CSV2TileMap"); // エディタ拡張ウィンドウのタイトル
        window.Show();
    }

    // 以下追加
    [SerializeField] private VisualTreeAsset _rootVisualTreeAsset;
    [SerializeField] private StyleSheet _rootStyleSheet;

    private void CreateGUI()
    {
        _rootVisualTreeAsset.CloneTree(rootVisualElement);
        rootVisualElement.styleSheets.Add(_rootStyleSheet);
        var copyButton = rootVisualElement.Q<Button>("StartButton");
        copyButton.clicked += () =>
        {
            var tilemap = (GameObject)rootVisualElement.Q<ObjectField>("TileMap").value;
            var csvFile = rootVisualElement.Q<ObjectField>("CSV").value as TextAsset;
            if (csvFile != null)
            {
                string[][] csvData = ParseCSV(csvFile.text);
                
                // csvDataを使用してタイルマップを構築する処理をここに追加
                for (int i = 0; i < csvData.Length;i++)
                {
                    for(int j = 0; j < csvData[i].Length;j++)
                    {
                        Debug.Log(csvData[i][j]);
                        if(!csvData[i][j].Trim().Equals("0"))
                        {
                            GameObject prefab = (GameObject)Resources.Load("Prefabs/" + csvData[i][j]);
                            if (prefab != null)
                            {
                                                           GameObject obj = Instantiate(prefab, new Vector3(j, 0-i, 0), Quaternion.identity);
                            obj.transform.SetParent(tilemap.transform, false); 
                            }

                        }
                    }
                }
            }
            else
            {
                Debug.LogError("CSVファイルが選択されていません");
            }
        };
    }

    private string[][] ParseCSV(string csvText)
    {
        var lines = csvText.Split('\n'); // 改行で行を分割
        var csvData = new string[lines.Length][]; // 行数に応じた2次元配列を作成

        for (int i = 0; i < lines.Length; i++)
        {
            csvData[i] = lines[i].Split(','); // 各行をカンマで分割して配列に格納
        }

        return csvData; // 2次元配列を返す
    }
}
