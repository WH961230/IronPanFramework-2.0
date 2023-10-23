using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ConsoleManager : MonoBehaviour {
    public List<ConsoleData> ConsoleDatas = new List<ConsoleData>();
    public TextMeshProUGUI ConsoleText;
    public PlayerInput PlayerInput;

    public void SendConsoleMessage(InputAction.CallbackContext context) {
        if (context.performed) {
            for (var i = 0; i < ConsoleDatas.Count; i++) {
                ConsoleData data = ConsoleDatas[i];
                if (ConsoleText.text.CompareTo(data.ConsoleName) == 1) {
                    GameManager.instance.gameMessage.Dis((int)data.ConsoleMessageCode);
                }
            }
        }
    }
}

[Serializable]
public class ConsoleData {
    public string ConsoleName;
    public MessageCode ConsoleMessageCode;
}