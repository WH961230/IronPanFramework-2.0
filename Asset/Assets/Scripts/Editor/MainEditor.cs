using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MainEditor : EditorWindow {
    private static List<string> TitleList = new List<string>();
    private static int index = 0;

    [MenuItem("Tools/MainEditor _F1")]
    static void Init() {
        MainEditorTitle.InitMainEditorTitleSetting();
        TitleList = MainEditorTitle.GetMainEditorTitleSettingTitleList();

        MainEditor window = (MainEditor)GetWindow(typeof(MainEditor));
        window.Show();
    }

    private void OnGUI() {
        index = GUILayout.Toolbar(index, TitleList.ToArray());
    }
}