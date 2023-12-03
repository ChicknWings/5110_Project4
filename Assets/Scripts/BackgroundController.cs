using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public float scrollSpeed = 0.5f;
    private Vector3 startPosition;
    private float spriteHeight;
    public GameObject backgroundPrefab;
    private bool isNewBG = false;

    void Start()
    {
        startPosition = transform.position;
        spriteHeight = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void Update()
    {
        transform.Translate(Vector2.down * scrollSpeed * Time.deltaTime);

        if (transform.position.y <= 5 && !isNewBG)
        {
            InstantiateNewBackground();
            isNewBG = true;
            
        }
        if(transform.position.y <= -5)
        {
            Destroy(gameObject);
        }
    }

    void InstantiateNewBackground()
    {
        Vector3 spawnPosition = new Vector3(0, 32, 10);
        Instantiate(backgroundPrefab, spawnPosition, Quaternion.identity);
    }
}
