using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BufferCollider : MonoBehaviour
{
    private FallingBlock fallingBlock;
    private void Start()
    {
        fallingBlock = GetComponentInParent<FallingBlock>();
    }

}
