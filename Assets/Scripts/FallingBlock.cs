using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FallingBlock : MonoBehaviour
{
    public float iniFallingSpeed = 5.0f; // fall speed
    private float fallingSpeed = 5.0f;
    public float weight = 1.0f; // weight
    public bool isCaught = false; // if be caught by others
    public bool isCatchOthers = false;// if catch others

    public float score;
    public float bonus = 0.2f;//如果完成了+多少分，范围0-1
    public float punish = -0.1f;//如果失败了+多少分，范围-1-0
    public float finishTime = 5.0f;//完成需要多少时间
    private float timer = 0.0f;

    public Image timerCircle;

    public FallingBlock preivousBlock;
    public FallingBlock nextBlock;

    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
        fallingSpeed = iniFallingSpeed;
    }


    void Update()
    {
        // if not be caught, falling
        if (!isCaught && fallingSpeed != 0)
        {
            transform.Translate(Vector2.down * fallingSpeed * Time.deltaTime);
        }

        //if be caught, -time
        if (isCaught)
        {
            timer += Time.deltaTime;
            UpdateTimerCircle(timer / finishTime);
        }
        if (timer >= finishTime)
        {
            Finish();
            timer = 0.0f;
        }
    }

    public void CollisionEnter(Collider2D collision)
    {
        var other = collision.gameObject.GetComponent<FallingBlock>();
        if (other != null && other.isCaught == true && other.isCatchOthers == false)
        {
            isCaught = true;

            transform.parent = player.transform;

            other.isCatchOthers = true;

            preivousBlock = other;
            other.nextBlock = this;
        }
    }

    /*
    public void CollisionExit(Collider2D other)
    {
        if (preivousBlock != null)
        {
            if (other.gameObject == preivousBlock.gameObject)
            {
                ReFall();
                Debug.Log("reflall");
            }
        }
        
    }
    */
    

    void UpdateTimerCircle(float progress)
    {
        if (timerCircle != null)
        {
            timerCircle.fillAmount = progress;
        }
    }

    void Finish()
    {
        Debug.Log("Finished!");
        //调用后一个物体的refall，如果有后一个物体
        
        if(nextBlock != null)
        {
            nextBlock.ReFall();
        }
        
        //调用前一个物体的recatch
        if(preivousBlock != null)
        {
            preivousBlock.Recatch();
        }
        preivousBlock = null;
        nextBlock = null;
        transform.parent = null;
        //加分
        //加进度条
        //让自己消失
        Debug.Log("destroy:" + this.gameObject);
        Destroy(this.gameObject);
    }

    public void ReFall()//如果下面的一个block消失了，就要调用上一个block的这个方法
    {
        transform.parent = null;
        isCaught = false;
        isCatchOthers = false;
        if (preivousBlock != null)
        {
            fallingSpeed = preivousBlock.fallingSpeed;
        }
        else
        {
            fallingSpeed = iniFallingSpeed;
        }
        if (nextBlock != null)
        {
            nextBlock.ReFall();
        }
    }

    public void Recatch()
    {
        isCatchOthers = false;
    }
}
