using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balance : MonoBehaviour
{
    public static Balance instance;
    public List<GameObject> gameObjects;

    private float weight;
    private Vector3 position;
    private void Awake()
    {
        instance = this;
    }
    public void AddBlock(GameObject newBlock)
    {
        gameObjects.Add(newBlock);
        //如果是接住的第一个方块的情况
        if (newBlock.GetComponent<FallingBlock>().preivousBlock.isFirst == true)
        {
            Debug.Log("是第一个");
            if (IfHit(newBlock.transform.position, newBlock.GetComponent<FallingBlock>().preivousBlock.gameObject))
            {
                return;
            }
            else
            {
                newBlock.GetComponent<FallingBlock>().Unbalance();
                return;
            }
        }
        else
        {
            //如果接到的不是第一个
            Vector3 temMassCenter = gameObjects[gameObjects.Count - 1].transform.position;//最后一个方块的重心位置
            float temWeight = gameObjects[gameObjects.Count - 1].GetComponent<FallingBlock>().weight;
            int count = 0;
            for (int i = gameObjects.Count - 1; i > 1; i--)
            {
                Debug.Log(i);
                if(i != gameObjects.Count - 1)
                {
                    //Debug.Log(temWeight);
                    CalculateCenterOfMass(temMassCenter, gameObjects[i].transform.position, temWeight, gameObjects[i].GetComponent<FallingBlock>().weight, out temWeight, out temMassCenter);
                }
                count++;
                Debug.DrawLine(temMassCenter, Vector2.down*100f);
                if(gameObjects[i].GetComponent<FallingBlock>().preivousBlock != null)
                {
                    if (IfHit(temMassCenter, gameObjects[i].GetComponent<FallingBlock>().preivousBlock.gameObject))
                    {
                    }
                    else
                    {
                        Debug.Log("没击中");
                        List<GameObject> objs = gameObjects.GetRange(i, count);
                        objs.Reverse();
                        BalanceAndRemove(objs);
                        return;
                    }
                }
                
            }
            
        }

    }
    
    private void CalculateCenterOfMass(Vector3 pos1, Vector3 pos2, float weight1, float weight2, out float weight, out Vector3 position)
    {
        float totalWeight = weight1 + weight2;
        Vector3 weightedPos1 = pos1 * weight1;
        Vector3 weightedPos2 = pos2 * weight2;
        Vector3 centerOfMass = (weightedPos1 + weightedPos2) / totalWeight;
        position = centerOfMass;
        weight = weight1 + weight2;
        //return centerOfMass;
    }
    
    private bool IfHit(Vector3 newBlockTransform, GameObject previousBlock)//从newBlock发射线看有没有击中previousBlock
    {
        RaycastHit2D[] hitResults = Physics2D.RaycastAll(newBlockTransform, Vector2.down, 100.0f);
        Debug.DrawLine(newBlockTransform, newBlockTransform + Vector3.down * 100.0f, Color.red);
        // 检查射线是否击中了指定的 GameObject
        foreach (RaycastHit2D hit in hitResults)
        {
            if (hit.collider != null && hit.collider.gameObject == previousBlock)
            {
                Debug.Log("射线击中了指定的 GameObject: " + previousBlock.name);
                return true; // 找到指定对象后，停止进一步搜索
            }
        }
        return false;
    }
    
    void CalculateAndBalance()
    {
        Vector3 cumulativePosition = Vector3.zero;
        int count = 0;

        for (int i = gameObjects.Count - 1; i > 0; i--)
        {
            cumulativePosition += gameObjects[i].transform.position;
            count++;
            Vector3 centerOfMass = cumulativePosition / count;

            RaycastHit hit;
            if (!Physics.Raycast(centerOfMass, Vector3.down, out hit) || hit.transform.gameObject != gameObjects[i - 1])
            {
                BalanceAndRemove(gameObjects.GetRange(i, count));
                break;
            }

            if (i == 1 && hit.transform.gameObject == gameObjects[0]) // 如果击中了列表中的第一个元素
            {
                BalanceAndRemove(gameObjects);
                break;
            }
        }
    }

    void BalanceAndRemove(List<GameObject> objectsToBalance)
    {
        foreach (var obj in objectsToBalance)
        {
            // 调用 Balance 方法，这里假设 GameObject 有一个 Balance 方法
            //obj.SendMessage("Balance", SendMessageOptions.DontRequireReceiver);
            obj.GetComponent<FallingBlock>().Unbalance();
            gameObjects.Remove(obj);
        }
    }
}
