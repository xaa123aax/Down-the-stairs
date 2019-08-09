using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GroundManager : MonoBehaviour
{
    readonly float initPosotionY = 0;
    readonly int Max_Ground_Count = 10;//最大地板數量
    readonly int Min_Ground_Count_Under_Player = 3;//玩家下方最小地板數量
    static int groundNumber = -1;
    readonly float leftBorder = -3;//左邊界
    readonly float rightBorder = 3;//右邊界
    [Range(2, 6)] public float spacingY;
    [Range(1, 20)] public float singleFloorHeight;
    public List<Transform> grounds;
    public Transform player;
    public Text displayCountFloor;


    void Start()
    {
        grounds = new List<Transform>();
        for (int i = 0; i < Max_Ground_Count; i++)
        {
            SpawnGround();
        }
    }
    public void ControlSpawnGround()//控制產生地板
    {
        int groundCountUnderPlayer = 0;
        foreach(Transform ground in grounds)
        {
            if (ground.position.y < player.position.y)
            {
                groundCountUnderPlayer++;
            }
        }
        if (groundCountUnderPlayer < Min_Ground_Count_Under_Player)
        {
            SpawnGround();
            ControlGroundsCount();
        }
      
    }
    public void ControlGroundsCount()
    {
        if (grounds.Count > Max_Ground_Count)
        {
            Destroy(grounds[0].gameObject);
            grounds.RemoveAt(0);
        }
    }
    float newGroundPosotionX()
    {
        if(grounds.Count==0)
        {
            return 0;  
        }
        return Random.Range(leftBorder, rightBorder);
    }
    //計算新地板Y座標
    float newGroundPosotionY()
    {
        if (grounds.Count == 0)
        {
            return initPosotionY;
        }
        int lowerIndex = grounds.Count - 1;
        return grounds[lowerIndex].transform.position.y - spacingY;
    }
    //產生單一地板
    void SpawnGround()
    {
        GameObject newGround = Instantiate(Resources.Load<GameObject>("普通的地板"));
        newGround.transform.position = new Vector2(newGroundPosotionX(), newGroundPosotionY());
        grounds.Add(newGround.transform);
    }
    float CountLowerGroundFloor()
    {
        float playerPositionY = player.transform.position.y;
        float deep = Mathf.Abs(initPosotionY - playerPositionY);
        return (deep / singleFloorHeight)+1;
    }
    void DisplayCountFloor()
    {
        displayCountFloor.text = "地下" + CountLowerGroundFloor().ToString("0000") + "樓"; 
    }
    void Update()
    {
        ControlSpawnGround();
        DisplayCountFloor();
    }
}
