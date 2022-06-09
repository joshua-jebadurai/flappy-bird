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

    private void Update()
    {
        transform.Translate(-transform.right * speed * Time.deltaTime);
        // Checking the position and destroying it - One way of doing it.
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Score increase");
        GameManager.instance.Scored();
    }
}
