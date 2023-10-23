using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FPointToolSetting))]
public class FPointToolSettingEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        FPointToolSetting setting = (FPointToolSetting)target;
        if(GUILayout.Button("创建读取点位工具探针")) {
            setting.CreatePointToolProbe();
        }

        if(GUILayout.Button("移除读取点位工具探针")) {
            setting.RemovePointToolProbe();
        }
    }
}