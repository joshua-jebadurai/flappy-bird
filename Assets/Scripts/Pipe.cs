using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{

    [SerializeField] private float speed = 1;
    [SerializeField] private float timeToDestroy = 7;

    private void Start()
    {
        // Write me a better solution than this.
        Destroy(gameObject, timeToDestroy);
    }

    private void OnEnable()
    {
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;
    }

    private GameState gameState;
    private void GameManager_OnGameStateChanged(GameState gameState)
    {
        this.gameState = gameState;
    }

    private void Update()
    {
        if (gameState == GameState.GAME_OVER || gameState == GameState.PAUSED)
            return;

        transform.Translate(-transform.right * speed * Time.deltaTime);
        // Checking the position and destroying it - One way of doing it.
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Score increase");
        GameManager.instance.Scored();
    }
}
