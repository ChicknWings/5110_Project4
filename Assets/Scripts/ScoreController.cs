using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public static ScoreController instance;

    public Text socialScoreText;
    public Text acdemicScoreText;

    private int socialScore = 0;
    private int acdemicScore = 0;

    public Slider socialSlider;
    public Slider acdemicSlider;

    private float socialRate = 0.5f;
    private float acdemicRate = 0.5f;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        socialScoreText.text = "000000";
        acdemicScoreText.text = "000000";
        socialSlider.value = 0.5f;
        acdemicSlider.value = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SocScoreAdd(int num)
    {
        socialScore += num;
        if (socialScore >= 1000000) socialScore = 999999;
        if (socialScore <= 0) socialScore = 0;
        socialScoreText.text = SwitchScore(socialScore);
    }
    public void AcdScoreAdd(int num)
    {
        acdemicScore += num;
        if (acdemicScore >= 1000000) acdemicScore = 999999;
        if (acdemicScore <= 0) acdemicScore = 0;
        acdemicScoreText.text = SwitchScore(acdemicScore);
    }

    private string SwitchScore(int num)
    {
        return num.ToString("D6");
    }

    public void SocRateAdj(float num)
    {
        socialRate += num;
        if (socialRate >= 1) 
        {
            socialRate = 1;
            //other functions

        }
        if (socialRate <= 0) 
        {
            socialRate = 0;
            //other functions
            if(HeartController.instance.EditHeart(-1))
            {
                socialRate = 0.5f;
            }
            else
            {
                LevelController.instance.GameFailed();
            }
        }
        socialSlider.value = socialRate;
    }
    public void AcdRateAdj(float num)
    {
        acdemicRate += num;
        if (acdemicRate >= 1)
        {
            acdemicRate = 1;
            //other functions

        }
        if (acdemicRate <= 0)
        {
            acdemicRate = 0;
            //other functions
            if (HeartController.instance.EditHeart(-1))
            {
                acdemicRate = 0.5f;
            }
            else
            {
                LevelController.instance.GameFailed();
            }
        }
        acdemicSlider.value = acdemicRate;
    }
}
