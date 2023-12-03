using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour
{
    public BoxCollider2D generationArea; // 生成区域的Transform
    //public float duration = 10f; // 持续时间d
    //public float randomness = 0f; // 随机性变量x

    //private List<GameObject> blocksList = new List<GameObject>(); // 存储要生成的blocks的数组
    private float timeBetweenSpawns; // 生成blocks之间的时间间隔

    void Start()
    {

    }

    List<GameObject> ShuffleBlocks(List<GameObject> blocksList)//洗牌
    {
        for (int i = 0; i < blocksList.Count; i++)
        {
            GameObject temp = blocksList[i];
            int randomIndex = Random.Range(i, blocksList.Count);
            blocksList[i] = blocksList[randomIndex];
            blocksList[randomIndex] = temp;
        }
        return blocksList;
    }

    private void iniBlocksList(List<GameObject> blocksList, DailyConfig dailyConfig)
    {
        for(int i = 0; i < dailyConfig.blocks.Count; i ++)
        {
            for(int j = 0; j < dailyConfig.blocks[i].spawnNumber; j ++)
            {
                blocksList.Add(dailyConfig.blocks[i].block);
            }
        }
    }

    public IEnumerator SpawnBlocks(DailyConfig dailyConfig, float duration, float randomness)//生成方块，传入方块列表，持续时间d和混乱值r
    {
        List<GameObject> blocksList = new List<GameObject>();
        iniBlocksList(blocksList, dailyConfig);

        //随机方块列表顺序
        ShuffleBlocks(blocksList);

        timeBetweenSpawns = duration / blocksList.Count;

        foreach (var block in blocksList)
        {
            float delay = timeBetweenSpawns * (1 - randomness) + Random.Range(0, timeBetweenSpawns * randomness);
            yield return new WaitForSeconds(delay);

            SpawnBlock(block);
        }
    }
    void SpawnBlock(GameObject block)
    {
        Vector2 spawnPoint = new Vector2(
            Random.Range(generationArea.bounds.min.x, generationArea.bounds.max.x),
            Random.Range(generationArea.bounds.min.y, generationArea.bounds.max.y)
        );

        Instantiate(block, spawnPoint, Quaternion.identity);
    }

    string ListToString<T>(List<T> list)
    {
        return "[" + string.Join(", ", list) + "]";
    }
}
