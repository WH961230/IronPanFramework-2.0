﻿using System;
using UnityEngine;

public class ObjectManager : MonoBehaviour {
    private void Start() {
        GameManager.instance.gameMessage.Reg((int)MessageCode.CreateObject, CreateObject);
    }

    public void CreateObject() {
        Debug.Log("创建玩家");
    }
}