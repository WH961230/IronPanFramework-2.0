using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "MainEditorConsoleSetting", menuName = "ScriptableObjects/MainEditor/MainEditorConsoleSetting", order = 1)]
public class MainEditorConsoleSetting : MainEditorSetting {
    public List<MainEditorConsoleData> ConsoleDatas = new List<MainEditorConsoleData>();
    public override void OnGUI() {
        base.OnGUI();
    }
}

[Serializable]
public class MainEditorConsoleData {
    public string ConsoleName;
    public MessageCode ConsoleMessageCode;
}