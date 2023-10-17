using UnityEngine;

[CreateAssetMenu(fileName = "MainEditorSubInterfaceEvoreekSetting", menuName = "ScriptableObjects/MainEditorSubInterface/MainEditorSubInterfaceEvoreekSetting", order = 1)]
public class MainEditorSubInterfaceEvoreekSetting : MainEditorSubInterfaceSetting {
    public override void OnGUI() {
        base.OnGUI();
        GUILayout.Label("Evoreek");
    }
}