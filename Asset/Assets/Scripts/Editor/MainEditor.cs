using System;
using UnityEditor;

public class MainEditor : EditorWindow {
    [MenuItem("Tools/MainEditor _F1")]
    static void Init() {
        MainEditorTitle.InitMainEditorTitleSetting();
        MainEditorInterface.InitMainEditorInterfaceSetting();
        MainEditor window = (MainEditor)GetWindow(typeof(MainEditor));
        window.Show();
    }

    private void OnGUI() {
        MainEditorEnum mainEditorEnum = MainEditorTitle.OnGUI();
        MainEditorInterface.OnGUI(mainEditorEnum);
    }
}

[Serializable]
public enum MainEditorEnum {
    Manual,
    Installer,
}