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

    // FPS
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rb.velocity = transform.up * jumpForce;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("GameOver");
        // GameOver
    }

    private void OnDisable()
    {
        // Dereference
    }

    private void OnDestroy()
    {
        
    }

}
