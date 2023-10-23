using UnityEngine;

public class FGameManager : MonoBehaviour {
    public FGameMessage FGameMessage;
    public FGameData FGameData;

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
        FGameData = new FGameData();
        FGameCreator = new FGameCreator();

        FGameCreator.CreateGame();
    }

    private void QuitGame() {
        FGameCreator.DestroyGame();
        FGameCreator = null;

        FGameData.Clear();
        FGameData = null;
    }

    private void Update() {
        
    }
}