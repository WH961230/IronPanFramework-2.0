using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class MainEditorTitle {
    private static string MainEditorTitleSettingPath = "Assets/Scripts/Editor/TitleSettings/MainEditorTitleSettings.asset";
    private static MainEditorTitleSetting MainEditorTitleSettings;

    private static int index = 0;
    private static List<MainEditorEnum> TitleList = new List<MainEditorEnum>();
    private static List<string> TitleSignList = new List<string>();
    public static void InitMainEditorTitleSetting() {
        MainEditorTitleSettings = LoadMainEditorTitleSettings(MainEditorTitleSettingPath);
        TitleList = GetMainEditorTitleSettingTitleList();

        TitleSignList.Clear();
        for (int i = 0; i < TitleList.Count; i++) {
            MainEditorEnum mainEditorEnum = TitleList[i];
            TitleSignList.Add(mainEditorEnum.ToString());
        }

        index = 0;
    }

    private static MainEditorTitleSetting LoadMainEditorTitleSettings(string mainEditorTitleSettingPath) {
        return AssetDatabase.LoadAssetAtPath<MainEditorTitleSetting>(mainEditorTitleSettingPath);
    }

    private static List<MainEditorEnum> GetMainEditorTitleSettingTitleList() {
        return MainEditorTitleSettings.TitleList;
    }

    public static MainEditorEnum OnGUI() {
        index = GUILayout.Toolbar(index, TitleSignList.ToArray());
        return TitleList[index];
    }
}