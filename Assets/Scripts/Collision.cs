using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    private FallingBlock fallingBlock;
    private void Start()
    {
        fallingBlock = GetComponentInParent<FallingBlock>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        float bottomOfCurrentObject = transform.position.y - (GetComponent<Collider2D>().bounds.extents.y);
        float topOfCollidedObject = collision.transform.position.y + (collision.bounds.extents.y);
        if (collision.gameObject.CompareTag("Buffer"))
        {
            Debug.Log("Buffer碰撞");
            Transform parentTransform = collision.gameObject.GetComponentInParent<Transform>();
            bottomOfCurrentObject = parentTransform.position.y - (parentTransform.gameObject.GetComponent<Collider2D>().bounds.extents.y);
            fallingBlock.CollisionEnter(collision, topOfCollidedObject - bottomOfCurrentObject);
            return;
        }

        if (bottomOfCurrentObject+0.05 >= topOfCollidedObject)
        {
            //Debug.Log("发生了底面碰撞");

            fallingBlock.CollisionEnter(collision, topOfCollidedObject - bottomOfCurrentObject);
        }
        else if(bottomOfCurrentObject+0.5 >= topOfCollidedObject)
        {
            //Debug.Log("没有发生地面碰撞");
        }
    }
}
