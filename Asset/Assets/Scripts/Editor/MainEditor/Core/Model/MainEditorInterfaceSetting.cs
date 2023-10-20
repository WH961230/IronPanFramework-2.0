using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "MainEditorInterfaceSetting", menuName = "ScriptableObjects/MainEditor/MainEditorInterfaceSetting", order = 1)]
public class MainEditorInterfaceSetting : MainEditorSetting {
    public string ConsoleInterfaceName;
    public GameObject ConsoleInterfacePrefab;
    public override void OnGUI() {
        base.OnGUI();
        ConsoleInterfaceName = EditorGUILayout.TextField("控制台界面命名", ConsoleInterfaceName);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("控制台界面预制体");
        ConsoleInterfacePrefab = (GameObject)EditorGUILayout.ObjectField(ConsoleInterfacePrefab, typeof(GameObject), true);
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("创建界面")) {
            CreateObject();
        }
    }

    void CreateObject() {
        if (ConsoleInterfacePrefab == null) {
            return;
        }
        GameObject go = Instantiate(ConsoleInterfacePrefab);
        go.name = ConsoleInterfaceName;
        GameObject interfaceRoot = GameObject.Find(GetMainSetting().InterfaceRootName);
        go.transform.SetParent(interfaceRoot.transform);
        go.GetComponent<RectTransform>().localPosition = Vector3.zero;
    }
}