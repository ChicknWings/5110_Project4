using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeatroyBlocks : MonoBehaviour
{
    AudioPlayer audioPlayer;

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("检测到碰撞");
        if(collision.gameObject.CompareTag("Text"))
        {
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.CompareTag("Academic block"))
        {
            ScoreController.instance.AcdRateAdj(collision.gameObject.GetComponent<FallingBlock>().punish);
            audioPlayer.PlayAcademicFailClip();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Social block"))
        {
            ScoreController.instance.SocRateAdj(collision.gameObject.GetComponent<FallingBlock>().punish);
            audioPlayer.PlaySocialFailClip();
            Destroy(collision.gameObject);
        }
    }
}
