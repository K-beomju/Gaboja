using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMove : MonoBehaviour
{
     public float speed;
    public Transform[] backgrounds;
 
    float leftPosX = 0f;
    float rightPosX = 0f;
    float xScreenHalfSize;
    float yScreenHalfSize;

    public enum BGType
    {
        Ground,
        Tree,
        BIG_Tree,
        BG_Back,
        BG_Mid,
        BG_Front

    }

    public BGType bGType;
    // Start is called before the first frame update
    void Start()
    {
        yScreenHalfSize = Camera.main.orthographicSize;
        xScreenHalfSize = yScreenHalfSize * Camera.main.aspect;
 
        leftPosX = -(xScreenHalfSize * 2);
        rightPosX = xScreenHalfSize * 2 * backgrounds.Length;
    }

    // Update is called once per frame
    void Update()
    {
       for(int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].position += new Vector3(-speed, 0, 0) * Time.deltaTime;
 
            if(backgrounds[i].position.x < leftPosX)
            {
                Vector3 nextPos = backgrounds[i].position;
                nextPos = new Vector3(nextPos.x + rightPosX, nextPos.y, nextPos.z);
                backgrounds[i].position = nextPos;
            }
        }
    }

    void MoveStage()
    {
        // for (int i = 0; i < backgroundIMG.Length; i++)
        // {
        //     backgroundIMG[i].position += new Vector3(speed, 0, 0) * Time.deltaTime;

        //     if (backgroundIMG[i].position.x < m_leftPosX)
        //     {
        //         Vector3 t_selfPos = backgroundIMG[i].position;
        //         t_selfPos.Set(t_selfPos.x + m_rightPosX, t_selfPos.y, t_selfPos.z);
        //         backgroundIMG[i].position = t_selfPos;
        //     }
        // }


    }
}
