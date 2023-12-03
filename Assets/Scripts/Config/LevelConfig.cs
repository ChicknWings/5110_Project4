using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu]
[Serializable]
public class LevelConfig : ScriptableObject
{
    public string id;// id
    public string title;// weekX

    [TextArea]
    public string description;

    public List<DailyConfig> dailyConfig;
}

[Serializable]
public struct DailyConfig
{
    public string title;
    public float duration;
    public List<BlockConfig> blocks;
}
[Serializable]
public struct BlockConfig
{
    public GameObject block;
    public int spawnNumber;
}
