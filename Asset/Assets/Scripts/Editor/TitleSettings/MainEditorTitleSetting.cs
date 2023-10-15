using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MainEditorTitleSettings", menuName = "ScriptableObjects/MainEditorTitleSettings", order = 1)]
public class MainEditorTitleSetting : ScriptableObject {
    public List<MainEditorEnum> TitleList = new List<MainEditorEnum>();
}