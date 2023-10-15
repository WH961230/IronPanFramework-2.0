using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MainEditorInterfaceSettings", menuName = "ScriptableObjects/MainEditorInterfaceSettings", order = 1)]
public class MainEditorInterfaceSetting : ScriptableObject {
    public List<MainEditorSubInterfaceRelation> SubRelationList = new List<MainEditorSubInterfaceRelation>();

    [Serializable]
    public class MainEditorSubInterfaceRelation {
        public MainEditorEnum MainEditorEnum;
        public MainEditorSubInterface MainEditorSubInterface;
    }
}