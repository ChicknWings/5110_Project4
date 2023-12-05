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

    public int score;
    public Type type;
    public bool isFirst = false;
    public float bonus = 0.2f;//如果完成了+多少rate，范围0-1
    public float punish = -0.1f;//如果失败了+多少rate，范围-1-0
    public float finishTime = 5.0f;//完成需要多少时间
    private float timer = 0.0f;

    public Image timerCircle;

    public FallingBlock preivousBlock;
    public FallingBlock nextBlock;
    public ShakeController shake;

    private GameObject player;

    AudioPlayer audioPlayer;

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Start()
    {
        if (isFirst) Balance.instance.gameObjects.Add(gameObject);
        player = GameObject.Find("Player");
        fallingSpeed = iniFallingSpeed;
        UpdateTimerCircle(0);
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

    public enum Type
    {
        acdemic,
        social
    }

    public void CollisionEnter(Collider2D collision, float buffer)//buffer是指collision的物体要向上挪动多少
    {
        var other = collision.gameObject.GetComponent<FallingBlock>();
        if (other != null && other.isCaught == true && other.isCatchOthers == false)
        {
            isCaught = true;

            transform.position = new Vector3(transform.position.x, transform.position.y + buffer, transform.position.z);
            transform.parent = player.transform;

            other.isCatchOthers = true;



            other.nextBlock = this;

            preivousBlock = other;

            Balance.instance.AddBlock(gameObject);
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
        if (nextBlock != null)
        {

            int index = Balance.instance.gameObjects.IndexOf(nextBlock.gameObject);

            Debug.Log("index"+index);
            Balance.instance.gameObjects[index].GetComponent<FallingBlock>().ReFall();
            nextBlock.ReFall();

            nextBlock = null;
            isCatchOthers = false;
        }

        int d = Balance.instance.gameObjects.IndexOf(gameObject);
        Balance.instance.gameObjects.RemoveRange(d, Balance.instance.gameObjects.Count - d);
        //调用后一个物体的refall，如果有后一个物体



        //调用前一个物体的recatch
        if (preivousBlock != null)
        {
            preivousBlock.Recatch();
        }
        preivousBlock = null;
        transform.parent = null;
        //加分，加进度条
        if(type == Type.social)
        {
            Debug.Log("是social" + bonus);
            ScoreController.instance.SocScoreAdd(score);
            ScoreController.instance.SocRateAdj(bonus);
            audioPlayer.PlaySocialSuccessClip();
        }
        else if(type == Type.acdemic)
        {
            Debug.Log("是acdemic" + bonus);
            ScoreController.instance.AcdScoreAdd(score);
            ScoreController.instance.AcdRateAdj(bonus);
            audioPlayer.PlayAcademicSuccessClip();
        }
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

    public void Unbalance()
    {
        shake.StartShake(1.0f, 5f, 6f);
        //掉下去
        Debug.Log("unbalance");
        return;
        preivousBlock.nextBlock = null;
        isCaught = false;
        preivousBlock.isCatchOthers = false;
        preivousBlock = null;
        nextBlock = null;
        transform.parent = null;
        GetComponent<Collider2D>().enabled = false;
        
        //transform.Translate(Vector3.right * 1.0f * Time.deltaTime);

    }
    public void Aftershake()
    {
        int d = Balance.instance.gameObjects.IndexOf(gameObject);
        if(d >= 0)
        {
            Balance.instance.gameObjects.RemoveRange(d, Balance.instance.gameObjects.Count - d);

        }
        if (nextBlock != null)
        {
            int index = Balance.instance.gameObjects.IndexOf(nextBlock.gameObject);//找到下一个block的index
            Debug.Log(index);
            if(index>=0)
            {
                Balance.instance.gameObjects[index].GetComponent<FallingBlock>().ReFall();

            }
            nextBlock.ReFall();

            nextBlock = null;

            isCatchOthers = false;
        }
        //int d = Balance.instance.gameObjects.IndexOf(gameObject);
        //Balance.instance.gameObjects.RemoveRange(d, Balance.instance.gameObjects.Count - d);

        Vector3 newPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -3.0f);
        gameObject.transform.position = newPosition;
        isCaught = false;
        if(preivousBlock != null)
        {
            preivousBlock.nextBlock = null;
            preivousBlock.isCatchOthers = false;
            preivousBlock = null;
        }
        if(nextBlock != null)
        {
            nextBlock.preivousBlock = null;
            nextBlock.isCaught = false;
        }
        nextBlock = null;
        preivousBlock = null;
        transform.parent = null;
        GetComponent<Collider2D>().enabled = false;
    }
}
