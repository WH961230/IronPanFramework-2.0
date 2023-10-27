using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FGameManager : MonoBehaviour {
    public FGameMessage FGameMessage;
    public FGameData FGameData;

    public FGameState FGameState;
    private FGameCreator FGameCreator;

    public static FGameManager Instance;
    public void Awake() {
        Instance = this;
        FGameMessage = new FGameMessage();
    }

    public void Start() {
        FGameMessage.Reg(FMessageCode.StartGame, StartGame);
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