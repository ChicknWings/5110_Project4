using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour
{
    public float fallingSpeed = 5.0f; // fall speed
    public float weight = 1.0f; // weight
    public bool isCaught = false; // if be caught by others
    public bool isCatchOthers = false;// if catch others

    private GameObject player; 

    void Start()
    {
        player = GameObject.Find("Player");
    }

    
    void Update()
    {
        // if not be caught, falling
        if (!isCaught && fallingSpeed != 0)
        {
            transform.Translate(Vector2.down * fallingSpeed * Time.deltaTime);
        }
    }
    
    public void CollisionEnter(Collider2D collision)
    {
        var other = collision.gameObject.GetComponent<FallingBlock>();
        if (other != null && other.isCaught == true && other.isCatchOthers == false)
        {
            isCaught = true;

            transform.parent = player.transform;

            other.isCatchOthers = true;
        }
    }
}
