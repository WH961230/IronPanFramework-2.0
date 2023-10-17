using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class MainEditor : EditorWindow {
    private const string MainEditorSettingPath = "Assets/Scripts/Editor/MainEditor/Config";
    private static List<MainEditorSetting> settingLists = new List<MainEditorSetting>();
    private static List<string> titleSignList = new List<string>();
    private static int index = 0;

    [MenuItem("Tools/MainEditor _F1")]
    static void Init() {
        MainEditorSetting[] settings = MainEditorTool.FindAllSetting<MainEditorSetting>(MainEditorSettingPath);

        settingLists.Clear();
        for (int i = 0; i < settings.Length; i++) {
            MainEditorSetting setting = settings[i];
            if(!String.IsNullOrEmpty(setting.TitleSign)) {
                settingLists.Add(setting);
            }
        }
        SortList(settingLists);

        titleSignList.Clear();
        titleSignList.AddRange(settingLists.Select(setting => setting.TitleSign));
        
        MainEditor window = (MainEditor)GetWindow(typeof(MainEditor));
        window.Show();
    }

    private static void SortList(List<MainEditorSetting> list) {
        list.Sort((a, b) => {
            if (a.DisplayWeight > b.DisplayWeight) {
                return 1;
            } else if (a.DisplayWeight < b.DisplayWeight) {
                return -1;
            } else {
                return 0;
            }
        });
    }

    private void OnGUI() {
        if (titleSignList.Count > 0) {
            index = GUILayout.Toolbar(index, titleSignList.ToArray());
            settingLists[index].OnGUI();
        }
    }
}