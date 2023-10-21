using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleManager : MonoBehaviour {
    void Start() {
    }

    void Update() {
    }

    public void SendConsoleMessage(int messageCode) {
        GameManager.instance.gameMessage.Dis((MessageCode)messageCode);
    }
}