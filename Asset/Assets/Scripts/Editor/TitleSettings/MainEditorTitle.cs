using System.Collections.Generic;
using UnityEditor;

public static class MainEditorTitle {
    private static string MainEditorTitleSettingPath = "Assets/Scripts/Editor/TitleSettings/MainEditorTitleSettings.asset";
    public static MainEditorTitleSetting MainEditorTitleSettings;

    public static void InitMainEditorTitleSetting() {
        MainEditorTitleSettings = null;
        MainEditorTitleSettings = LoadMainEditorTitleSettings(MainEditorTitleSettingPath);
    }

    private static MainEditorTitleSetting LoadMainEditorTitleSettings(string mainEditorTitleSettingPath) {
        return AssetDatabase.LoadAssetAtPath<MainEditorTitleSetting>(mainEditorTitleSettingPath);
    }

    public static List<string> GetMainEditorTitleSettingTitleList() {
        return MainEditorTitleSettings.TitleList;
    }
}