using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;

    public static UIManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Can have only one UIManager, destroying this one");
            Destroy(gameObject);
        }

        instance = this;
    }

    private void OnEnable()
    {
        GameManager.OnScoreUpdated += GameManager_OnScoreUpdated;
        Player.OnPlayerBumped += Player_OnPlayerBumped;

        // Listen to gameover event from GameManager and show a game over panel.
    }

    private void Player_OnPlayerBumped()
    {
        Debug.Log("Played bumped in UI Mangager");
    }

    private void OnDisable()
    {
        GameManager.OnScoreUpdated -= GameManager_OnScoreUpdated;
        Player.OnPlayerBumped -= Player_OnPlayerBumped;
    }

    private void GameManager_OnScoreUpdated(int score)
    {
        UpdateScore(score);
    }

   
    public void UpdateScore (int score)
    {
        scoreText.text = score.ToString();
    }
}
