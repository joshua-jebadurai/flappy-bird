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

    private void Start()
    {
        // Without using it.
        // Without using InvokeRepeat.
        // Try to use Update
        //StartCoroutine(SpawnPipe());
    }

    private IEnumerator SpawnPipe ()
    {
        while (true)
        {
            yield return new WaitForSeconds(pipeSpawnInterval);
            Instantiate(pipePrefab, new Vector3(3, 0, 0), Quaternion.identity);
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
        Debug.Log("GameManager enabled");
    }

    private void OnDisable()
    {
        Player.OnPlayerBumped -= Player_OnPlayerBumped;
        Debug.Log("GameManager disabled");
    }

    private void Player_OnPlayerBumped()
    {
        // Paused the game
        Time.timeScale = 0;
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
        pipeSpawnDelta += Time.deltaTime;

        if (pipeSpawnDelta > pipeSpawnInterval)
        {
            Instantiate(pipePrefab, new Vector3(3, 0, 0), Quaternion.identity);
            pipeSpawnDelta = 0;
        }
    }

    public delegate void ScoreHandler(int score);
    public static event ScoreHandler OnScoreUpdated;

    // The one that's firing the event
    // Subscribers.

    // I want to have a GameOver event to happen here.
    // optional by the way, try to create an game state system. Tips: enums.
}
