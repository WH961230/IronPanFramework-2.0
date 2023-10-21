using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameMessage gameMessage;
    public static GameManager instance;

    private void Awake() {
        instance = this;
        gameMessage = new GameMessage();
    }

    private void Start() {
        
    }

    private void Update() {
        
    }
}