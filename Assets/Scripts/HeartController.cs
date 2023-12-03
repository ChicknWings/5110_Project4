using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartController : MonoBehaviour
{
    public static HeartController instance;
    public int iniHeart = 3;//初始生命
    private int currentHeart = 3;
    public List<GameObject> hearts;//最大生命

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        currentHeart = iniHeart;
        for (int i = 0; i < currentHeart; i++)
        {
            hearts[i].SetActive(true);
        }
    }

    public bool EditHeart(int num)//返回true是没死 返回false是死了
    {
        currentHeart += num;
        if (currentHeart >= hearts.Count) currentHeart = hearts.Count;
        if (currentHeart <= 0)
        {
            currentHeart = 0;
        }
        for(int i = 0; i< currentHeart; i ++)
        {
            hearts[i].SetActive(true);
        }
        if (currentHeart == 0) return false;
        return true;
    }
}
