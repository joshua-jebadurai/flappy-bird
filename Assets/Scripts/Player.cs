using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float jumpForce = 1.4f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /*private void OnEnable()
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
    }*/

    // FPS
    private void Update()
    {
        var gameState = GameManager.GetGameState();
        if (gameState == GameState.GAME_OVER || gameState == GameState.PAUSED)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            rb.velocity = transform.up * jumpForce;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // This will show how many subscribers are there for this event.
        Debug.Log("Subscribers: " + OnPlayerBumped.GetInvocationList().Length);

        // checking if there are subscribers for this event.
        if (OnPlayerBumped != null)
            OnPlayerBumped(); // executing all the callbacks registered
    }

 
    public delegate void PlayerBumpedHandler();
    public static event PlayerBumpedHandler OnPlayerBumped; // Think of this as an array of callbacks

}
