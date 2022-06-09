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
        StartCoroutine(SpawnPipe());
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

    [SerializeField] private int score = 0;

    // Object memeber method
    public void Scored ()
    {
        score++;
        UIManager.instance.UpdateScore(score);
    }
}
