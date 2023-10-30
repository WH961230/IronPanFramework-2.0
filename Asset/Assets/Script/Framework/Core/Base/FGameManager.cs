using System;
using UnityEngine;
using UnityEngine.Events;

public class FGameManager : MonoBehaviour {
    public FGameMessage FGameMessage;
    public FGameData FGameData;

    public FGameState FGameState;
    private FGameCreator FGameCreator;

    public UnityEvent UpdateEvent;
    public UnityEvent FixedUpdateEvent;
    public UnityEvent LateUpdateEvent;

    public static FGameManager Instance;
    public void Awake() {
        Instance = this;
        FGameMessage = new FGameMessage();
    }

    public void Start() {
        FGameMessage.Reg(FMessageCode.StartGame, StartGame);
        FGameMessage.Reg<FUpdateType, UnityAction>(FMessageCode.UpdateEvent, MsgUpdate);
        FGameMessage.Reg(FMessageCode.QuitGame, QuitGame);
    }

    private void StartGame() {
        if (FGameState == FGameState.GameStart) {
            return;
        }
        FGameData = new FGameData();
        FGameCreator = new FGameCreator();

        FGameCreator.CreateGame();

        FGameState = FGameState.GameStart;
    }

    private void MsgUpdate(FUpdateType updateType, UnityAction unityAction) {
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

    private void Update() {
        UpdateEvent?.Invoke();
    }

    private void FixedUpdate() {
        FixedUpdateEvent?.Invoke();
    }

    private void LateUpdate() {
        LateUpdateEvent?.Invoke();
    }

    private void QuitGame() {
        if (FGameState == FGameState.GameQuit) {
            return;
        }
        FGameCreator.DestroyGame();
        FGameCreator = null;

        FGameData.Clear();
        FGameData = null;

        FGameState = FGameState.GameQuit;
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