using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public static ScoreController instance;

    public Text socialScoreText;
    public Text acdemicScoreText;

    private int socialScore;
    private int acdemicScore;

    public Slider socialSlider;
    public Slider acdemicSlider;

    private float socialRate;
    private float acdemicRate;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        socialScoreText.text = "000000";
        acdemicScoreText.text = "000000";
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
        socialScore += num;
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

        }
        acdemicSlider.value = acdemicRate;
    }
}
