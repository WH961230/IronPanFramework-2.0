using System;
using System.Collections.Generic;
using UnityEditor;

public static class MainEditorInterface {
    private static string MainEditorInterfaceSettingPath = "Assets/Scripts/Editor/InterfaceSettings/MainEditorInterfaceSettings.asset";
    private static MainEditorInterfaceSetting MainEditorInterfaceSettings;

    private static List<MainEditorInterfaceSetting.MainEditorSubInterfaceRelation> SubInterfaceList =
        new List<MainEditorInterfaceSetting.MainEditorSubInterfaceRelation>();

    public static void InitMainEditorInterfaceSetting() {
        MainEditorInterfaceSettings = GetMainEditorSubInterfaceList(MainEditorInterfaceSettingPath);
        SubInterfaceList = LoadMainEditorSubInterfaceRelationList();
    }

    private static MainEditorInterfaceSetting GetMainEditorSubInterfaceList(string mainEditorInterfaceSettingPath) {
        return AssetDatabase.LoadAssetAtPath<MainEditorInterfaceSetting>(mainEditorInterfaceSettingPath);
    }

    private static List<MainEditorInterfaceSetting.MainEditorSubInterfaceRelation> LoadMainEditorSubInterfaceRelationList() {
        return MainEditorInterfaceSettings.SubRelationList;
    }

    public static void OnGUI(MainEditorEnum mainEditorEnum) {
        for (int i = 0; i < SubInterfaceList.Count; i++) {
            MainEditorInterfaceSetting.MainEditorSubInterfaceRelation subInterfaceRelation = SubInterfaceList[i];
            if (subInterfaceRelation.MainEditorEnum != mainEditorEnum) {
                continue;
            }
            subInterfaceRelation.MainEditorSubInterface.OnGUI();
        }
    }
}