using System;
using UnityEngine;
using UnityEngine.Events;

public class FGameManager : MonoBehaviour {
    public FGameMessage FGameMessage;

    public FGameState FGameState;

    private UnityEvent UpdateEvent = new UnityEvent();
    private UnityEvent FixedUpdateEvent = new UnityEvent();
    private UnityEvent LateUpdateEvent = new UnityEvent();

    public void Awake() {
        FGameMessage = new FGameMessage();
    }

    public void Start() {
        FGameMessage.Reg(FMessageCode.StartGame, StartGame);
        FGameMessage.Reg(FMessageCode.QuitGame, QuitGame);
        FGameMessage.Reg<FUpdateType, UnityAction>(FMessageCode.AddUpdateListener, MsgAddUpdate);
        FGameMessage.Reg<FUpdateType, UnityAction>(FMessageCode.RemoveUpdateListener, MsgRemoveUpdate);
    }

    private void StartGame() {
        if (FGameState == FGameState.GameStart) {
            return;
        }
        new FGameData();
        new FGameCreator();
        FGameMessage.Dis(FMessageCode.CreateAll);
        FGameState = FGameState.GameStart;
    }

    private void QuitGame() {
        if (FGameState == FGameState.GameQuit) {
            return;
        }
        FGameMessage.Dis(FMessageCode.DestoryAll);
        FGameMessage = null;
        FGameState = FGameState.GameQuit;
    }

    private void MsgAddUpdate(FUpdateType updateType, UnityAction unityAction) {
        switch (updateType) {
            case FUpdateType.Update:
                UpdateEvent?.AddListener(unityAction);
                break;
            case FUpdateType.FixedUpdate:
                FixedUpdateEvent?.AddListener(unityAction);
                break;
            case FUpdateType.LateUpdate:
                LateUpdateEvent?.AddListener(unityAction);
                break;
        }
    }

    private void MsgRemoveUpdate(FUpdateType updateType, UnityAction unityAction) {
        switch (updateType) {
            case FUpdateType.Update:
                UpdateEvent?.RemoveListener(unityAction);
                break;
            case FUpdateType.FixedUpdate:
                FixedUpdateEvent?.RemoveListener(unityAction);
                break;
            case FUpdateType.LateUpdate:
                LateUpdateEvent?.RemoveListener(unityAction);
                break;
        }
    }

    private void Update() {
        UpdateEvent?.Invoke();
    }

    private void FixedUpdate() {
        FixedUpdateEvent?.Invoke();
    }

    private void LateUpdate() {
        LateUpdateEvent?.Invoke();
    }
}

[Serializable]
public enum FGameState {
    GameQuit,
    GameStart,
}

public enum FUpdateType {
    Update,
    FixedUpdate,
    LateUpdate,
}