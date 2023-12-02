using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController instance;
    public List<LevelConfig> levels;

    private void Awake()
    {
        instance = this;
    }
}
