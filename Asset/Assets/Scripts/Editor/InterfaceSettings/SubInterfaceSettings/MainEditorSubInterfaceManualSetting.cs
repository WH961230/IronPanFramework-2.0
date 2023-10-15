using UnityEngine;

[CreateAssetMenu(fileName = "MainEditorSubInterfaceSetting", menuName = "ScriptableObjects/MainEditorSubInterface/MainEditorSubInterfaceManualSetting", order = 1)]
public class MainEditorSubInterfaceManualSetting : MainEditorSubInterface {
    public override void OnGUI() {
        base.OnGUI();
        GUILayout.Label("MainEditorSubInterfaceManualSetting");
    }
}