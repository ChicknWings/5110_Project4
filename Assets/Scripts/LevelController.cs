using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController instance;

    public List<LevelConfig> levels;
    public BlockGenerator blockGenerator;

    public Transform textSpawnPoint;
    public GameObject textPrefab;

    public float scrollingSpeed = 3.0f;

    private bool isInGame = false;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        StartCoroutine(GameProgress());
    }

    private IEnumerator GameProgress()
    {
        //先显示一个开始动画
        //然后按照week播放
        for(int i = 0; i < levels.Count; i ++)
        {
            Debug.Log("关卡" + i +"开始");
            yield return StartCoroutine(RunLevel(levels[i]));
        }
    }

    private IEnumerator RunLevel(LevelConfig level)
    {
        //从周一播到周日
        for(int j = 0; j < level.dailyConfig.Count; j ++)
        {
            Debug.Log("第" + j + "阶段开始");
            yield return StartCoroutine(blockGenerator.SpawnBlocks(level.dailyConfig[j], level.dailyConfig[j].duration, 0.8f));
        }
    }
}
