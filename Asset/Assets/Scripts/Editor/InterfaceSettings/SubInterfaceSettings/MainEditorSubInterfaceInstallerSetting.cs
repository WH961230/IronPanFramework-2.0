using UnityEngine;

[CreateAssetMenu(fileName = "MainEditorSubInterfaceInstallerSetting", menuName = "ScriptableObjects/MainEditorSubInterface/MainEditorSubInterfaceInstallerSetting", order = 1)]
public class MainEditorSubInterfaceInstallerSetting : MainEditorSubInterface {
    public override void OnGUI() {
        base.OnGUI();
        GUILayout.Label("MainEditorSubInterfaceInstallerSetting");
    }
}