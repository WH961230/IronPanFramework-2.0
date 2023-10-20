using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "MainEditorConsoleSetting", menuName = "ScriptableObjects/MainEditor/MainEditorConsoleSetting", order = 1)]
public class MainEditorConsoleSetting : MainEditorSetting {
    [SerializeField] private bool displayConsole;
    public override void OnGUI() {
        base.OnGUI();
        displayConsole = EditorGUILayout.Toggle("显示控制台", displayConsole);
        if(displayConsole) {
            // OpenConsole();
        } else {
            // CloseConsole();
        }
    }
}