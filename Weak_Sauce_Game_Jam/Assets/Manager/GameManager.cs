using UnityEngine;

// Interface for GameManager
public interface IGameManager
{
    void OnMenu();
    void OnStart();
    void OnPause();
    void OnSearch();
    void OnChase();
    void OnGameOver();
}

// GameManager script
public class GameManager : MonoBehaviour
{
    public IGameManager gameManagerEvents;

    // Game states
    public enum GameState
    {
        Menu,
        Start,
        Pause,
        Search,
        Chase,
        GameOver
    }

    // Current game state
    [SerializeField] private GameState currentState;

    // Function to change game state
    public void ChangeState(GameState newState)
    {
        currentState = newState;

        // Call the appropriate function based on the state change
        switch (newState)
        {
            case GameState.Menu:
                gameManagerEvents.OnMenu();
                break;
            case GameState.Start:
                gameManagerEvents.OnStart();
                break;
            case GameState.Pause:
                gameManagerEvents.OnPause();
                break;
            case GameState.Search:
                gameManagerEvents.OnSearch();
                break;
            case GameState.Chase:
                gameManagerEvents.OnChase();
                break;
            case GameState.GameOver:
                gameManagerEvents.OnGameOver();
                break;
        }
    }

    public void StartState()
    {
        currentState = GameState.Start;
    }

}
