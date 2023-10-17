using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class MainEditor : EditorWindow {
    private const string MainEditorSubInterfaceSettingPath = "Assets/Scripts/Editor/MainEditor/Config";
    private static List<MainEditorSubInterfaceSetting> subInterfaceSettingLists = new List<MainEditorSubInterfaceSetting>();
    private static List<string> titleSignList = new List<string>();
    private static int index = 0;

    [MenuItem("Tools/MainEditor _F1")]
    static void Init() {
        MainEditorSubInterfaceSetting[] subInterfaceSettings = MainEditorTool.FindAllSetting<MainEditorSubInterfaceSetting>(MainEditorSubInterfaceSettingPath);

        subInterfaceSettingLists.Clear();
        for (int i = 0; i < subInterfaceSettings.Length; i++) {
            MainEditorSubInterfaceSetting subInterfaceSetting = subInterfaceSettings[i];
            if(!String.IsNullOrEmpty(subInterfaceSetting.SubInterfaceTitleSign)) {
                subInterfaceSettingLists.Add(subInterfaceSetting);
            }
        }

        titleSignList.Clear();
        titleSignList.AddRange(subInterfaceSettingLists.Select(subInterfaceSetting => subInterfaceSetting.SubInterfaceTitleSign));

        MainEditor window = (MainEditor)GetWindow(typeof(MainEditor));
        window.Show();
    }

    private void OnGUI() {
        if (titleSignList.Count > 0) {
            index = GUILayout.Toolbar(index, titleSignList.ToArray());
            subInterfaceSettingLists[index].OnGUI();
        }
    }
}