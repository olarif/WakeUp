using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceChair : MonoBehaviour
{
    private GameObject playerGameObject;
    public float launchSpeed;

    // Start is called before the first frame update
    void Start()
    {
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb = playerGameObject.GetComponent<Rigidbody2D>();
            playerGameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * launchSpeed, ForceMode2D.Impulse);
        }
    }
}
