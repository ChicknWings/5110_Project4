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

    public DailyConfig Monday;
    public DailyConfig Tuesday;
    public DailyConfig Wednesday;
    public DailyConfig Thursday;
    public DailyConfig Friday;
    public DailyConfig Saturday;
    public DailyConfig Sunday;

}

[Serializable]
public struct DailyConfig
{
    public float duration;
    public int speed;
    public List<BlockConfig> blocks;
}
[Serializable]
public struct BlockConfig
{
    public GameObject block;
    public int spawnNumber;
}
