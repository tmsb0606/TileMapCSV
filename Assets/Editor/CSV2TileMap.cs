using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
public class CSV2TileMap : EditorWindow
{
    [MenuItem("MAP/CSV2TileMap")] // �w�b�_���j���[��/�w�b�_�ȉ��̃��j���[��
    private static void ShowWindow()
    {
        var window = GetWindow<CSV2TileMap>("UIElements");
        window.titleContent = new GUIContent("CSV2TileMap"); // �G�f�B�^�g���E�B���h�E�̃^�C�g��
        window.Show();
    }

    // �ȉ��ǉ�
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
                
                // csvData���g�p���ă^�C���}�b�v���\�z���鏈���������ɒǉ�
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
                Debug.LogError("CSV�t�@�C�����I������Ă��܂���");
            }
        };
    }

    private string[][] ParseCSV(string csvText)
    {
        var lines = csvText.Split('\n'); // ���s�ōs�𕪊�
        var csvData = new string[lines.Length][]; // �s���ɉ�����2�����z����쐬

        for (int i = 0; i < lines.Length; i++)
        {
            csvData[i] = lines[i].Split(','); // �e�s���J���}�ŕ������Ĕz��Ɋi�[
        }

        return csvData; // 2�����z���Ԃ�
    }
}
