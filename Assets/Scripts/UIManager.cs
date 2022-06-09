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

    public void UpdateScore (int score)
    {
        scoreText.text = score.ToString();
    }
}
