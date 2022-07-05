using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Pipe generation
    // Player instance
    // Game over
    // UI Gameoverpanel - UIManager.cs
    // Reload the scene
    // High score
    // Score system
    // Object meme

    // Reference here of the Text box;

    [SerializeField] private GameObject pipePrefab;
    [SerializeField] private float pipeSpawnInterval = 2;

    private List<GameObject> pipePool = new List<GameObject>();

    private GameObject GetPipe ()
    {
        // Go through the list and find out which pipe is inactive.
        for (int i = 0; i < pipePool.Count; i++)
        {
            
            if (!pipePool[i].activeSelf)
            {
                pipePool[i].SetActive(true);
                return pipePool[i];
            }
        }

        // if there are no pipes inactive in the pool then create a new one.
        GameObject newPipe = Instantiate(pipePrefab, new Vector3(3, 0, 0), Quaternion.identity);
        pipePool.Add(newPipe);

        return newPipe;
    }


    // optional for now, we will use this once we write our own abstract object pooling system
    public void ReturnPipe (GameObject usedPipe)
    {
        // or you can use this instead of destroy.
        usedPipe.SetActive(false);
    }

    private void Start()
    {
        // Without using it.
        // Without using InvokeRepeat.
        // Try to use Update
        //StartCoroutine(SpawnPipe());
    }

    // Not using this anymore.
    private IEnumerator SpawnPipe ()
    {
        while (true)
        {
            yield return new WaitForSeconds(pipeSpawnInterval);
            GameObject pipe = GetPipe();
            pipe.SetActive(true);
            pipe.transform.position = new Vector3(3, 0, 0);
            //Instantiate(pipePrefab, new Vector3(3, 0, 0), Quaternion.identity);
        }
    }

    // This is the only reference to the class member
    public static GameManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Can have only one GameManager, destroying this one");
            Destroy(gameObject);
        }

        instance = this;
    }

    private void OnEnable()
    {
        Player.OnPlayerBumped += Player_OnPlayerBumped;
        
    }

    private void OnDisable()
    {
        Player.OnPlayerBumped -= Player_OnPlayerBumped;
    
    }

    private void Player_OnPlayerBumped()
    {
        // Paused the game
        //Time.timeScale = 0;
        SetGameState(GameState.GAME_OVER);
    }

    [SerializeField] private int score = 0;

    // Object memeber method
    public void Scored ()
    {
        score++;
        // If there are subscribers
        if (OnScoreUpdated != null)
        {
            OnScoreUpdated(score);
        }
        //UIManager.instance.UpdateScore(score);
    }

    private float pipeSpawnDelta;
    private void Update()
    {
        PipeSpawnUpdate();
    }

    private void PipeSpawnUpdate ()
    {
        if (gameState == GameState.GAME_OVER || gameState == GameState.PAUSED)
            return;

        pipeSpawnDelta += Time.deltaTime;

        if (pipeSpawnDelta > pipeSpawnInterval)
        {
            GameObject pipe = GetPipe();
            pipe.transform.position = new Vector3(3, 0, 0);
            //Instantiate(pipePrefab, new Vector3(3, 0, 0), Quaternion.identity);
            pipeSpawnDelta = 0;
        }
    }

    public delegate void ScoreHandler(int score);
    public static event ScoreHandler OnScoreUpdated;

    // The one that's firing the event
    // Subscribers.

    // I want to have a GameOver event to happen here.
    // optional by the way, try to create an game state system. Tips: enums.


    // GameStates => Main Menu, Game, GameOver, Pause
    // MENU STATES (UI)=> Main, Bird Selection, Options
    // Character Animation State => Idle, Jumping, Running, Walking, Dead

    [SerializeField] private GameState gameState = GameState.IN_GAME;

    // Property
    public static GameState GetGameState ()
    {
        return instance.gameState;
    }

    public static void SetGameState (GameState gameState)
    {
        instance.gameState = gameState;

        // Firing an event to all the subscribers
        if (OnGameStateChanged != null)
            OnGameStateChanged(gameState);
    }

    public delegate void GameStateHandler(GameState gameState);
    public static event GameStateHandler OnGameStateChanged;
}

public enum GameState
{
    MENU = 0,
    IN_GAME = 1,
    GAME_OVER = 2,
    PAUSED = 3
}
