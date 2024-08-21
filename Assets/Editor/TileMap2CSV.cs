using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using Unity.VisualScripting.FullSerializer;
using UnityEngine.Rendering; // 追加

public class TileMap2CSV :  EditorWindow
{
    private StreamWriter sw;
    [MenuItem("MAP/TileMap2CSV")] // ヘッダメニュー名/ヘッダ以下のメニュー名
    private static void ShowWindow()
    {
        var window = GetWindow<TileMap2CSV>("UIElements");
        window.titleContent = new GUIContent("TileMap2CSV"); // エディタ拡張ウィンドウのタイトル
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
            using (StreamWriter sw = new StreamWriter(@"SaveData.csv", false, Encoding.GetEncoding("Shift_JIS")))
            {
                var tilemap = (GameObject)rootVisualElement.Q<ObjectField>("TileMap").value;
                var xy = (Vector2)rootVisualElement.Q<Vector2Field>("ColumnRow").value;
                var offset = (Vector2)rootVisualElement.Q<Vector2Field>("OffSet").value;
                //saki1.GetComponent<Renderer>().sharedMaterial = moto1.GetComponent<Renderer>().sharedMaterial;
                //Debug.Log(tilemap);
                var children = new Transform[tilemap.transform.childCount];
                for (var i = 0; i < children.Length; ++i)
                {
                    children[i] = tilemap.transform.GetChild(i);
                    //children[i].transform.position += new Vector3(offset.x,offset.y,0);
                    // Debug.Log(children[i].transform.position);
                }

                string[][] data = new string[(int)xy.y][];

                //csv作成
                for (int i = 0; i < xy.y; i++)
                {
                    data[i] = new string[(int)xy.x];
                    for (int j = 0; j < xy.x; j++)
                    {
                        Debug.Log(false);
                        data[i][j] = "0";
                        for (var k = 0; k < children.Length; k++)
                        {


                            if (children[k].transform.position.x + offset.x == j)
                            {
                                if (children[k].transform.position.y + offset.y == i)
                                {
                                    Debug.Log(true);
                                    //data[i][j] ="1";
                                    data[i][j] = children[k].name;
                                    break;
                                }

                            }
                            
                        }
                    }

                }

/*                for (int i = 0; i < xy.y; i++)
                {
                    string str = string.Join(",", data[i]);
                    sw.WriteLine(str);
                }*/

                //反転
                for (int i = (int)xy.y - 1; i >= 0; i--)
                {
                    string str = string.Join(",", data[i]);
                    sw.WriteLine(str);
                }
            }

        };

    }
    
}
