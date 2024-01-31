using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public interface IGameManager
{
    void OnMenu();
    void OnStart();
    void OnPause();
    void OnSearch();
    void OnChase();
    void OnGameOver();
}

public class GameManager : MonoBehaviour
{
    private IGameManager gameManagerEvents;
    [SerializeField] private List<GameObject> toys = new List<GameObject>();

    [SerializeField] private List<Transform> spawnPoints = new List<Transform>(); // Use a list of spawn points

    [Range(1, 10)]
    public int availbleToys = 5;
    private static System.Random rnd;

    public enum GameState
    {
        Menu,
        Start,
        Pause,
        Search,
        Chase,
        GameOver
    }

    [SerializeField] private GameState currentState;

    public void ChangeState(GameState newState)
    {
        currentState = newState;

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

    void Awake()
    {
        SpawnToys();
    }

    private void SpawnToys()
    {
        rnd = new System.Random();
        toys = toys.OrderBy(a => rnd.Next()).ToList();

        for (int currToy = 0; currToy < availbleToys; currToy++)
        {
            if (currToy < toys.Count)
            {
                toys[currToy].SetActive(true);

                // Use modulo to loop through spawn points in a circular manner
                Transform spawnPoint = spawnPoints[currToy % spawnPoints.Count];
                toys[currToy].transform.position = RandomPosition(spawnPoint);
            }
        }
    }

    private Vector3 RandomPosition(Transform spawnPoint)
    {
        float x = Random.Range(spawnPoint.position.x - spawnPoint.localScale.x / 2, spawnPoint.position.x + spawnPoint.localScale.x / 2);
        float y = 2.5f;
        float z = Random.Range(spawnPoint.position.z - spawnPoint.localScale.z / 2, spawnPoint.position.z + spawnPoint.localScale.z / 2);
        return new Vector3(x, y, z);
    }
}
