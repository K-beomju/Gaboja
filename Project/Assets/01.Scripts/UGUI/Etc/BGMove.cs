using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMove : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer[] backGrounds;

    [SerializeField]
    [Tooltip("맨뒤 배경 속도")]
    private float offsetBackSpeed;
    private float offsetBack;


    [SerializeField]
    [Tooltip("중간 배경 속도")]
    private float offsetMidSpeed;
    private float offsetMid;


    [SerializeField]
    [Tooltip("맨앞 배경 속도")]
    private float offsetFrontSpeed;
    private float offsetFront;
    
    [SerializeField]
    [Tooltip("그라운드 속도")]
    private float offsetGroundSpeed;
    private float offsetGround;

    [SerializeField]
    [Tooltip("나무 배경 오브젝트")]
    private GameObject subGround1;
    [SerializeField]
    [Tooltip("나무 배경 오브젝트")]
    private GameObject subGround2;

    [SerializeField]
    float subGroundSpeed;


    
    Vector3 endPos = new Vector3(-5.74f,0,0);
    Vector3 startPos = new Vector3(6, 0, 0);

    private void Start() 
    {
        
        backGrounds = GetComponentsInChildren<MeshRenderer>();
    }
    private void Update() 
    {
        
        SubBackGroundMove();
        BackGroundMove();
    }

    public void BackGroundMove()
    {
        offsetBack += Time.deltaTime * offsetBackSpeed;
        offsetMid += Time.deltaTime * offsetMidSpeed;
        offsetFront += Time.deltaTime * offsetFrontSpeed;
        offsetGround += Time.deltaTime * offsetGroundSpeed;

        backGrounds[0].material.mainTextureOffset = new Vector2(offsetGround, 0);
        backGrounds[1].material.mainTextureOffset = new Vector2(offsetBack , 0);
        backGrounds[2].material.mainTextureOffset = new Vector2(offsetMid , 0);
        backGrounds[3].material.mainTextureOffset = new Vector2(offsetFront, 0);
    }
    public void SubBackGroundMove()
    {
        subGround1.transform.Translate(Vector3.left * subGroundSpeed * Time.deltaTime, Space.World);
        subGround2.transform.Translate(Vector3.left * subGroundSpeed * Time.deltaTime, Space.World);
        if (subGround1.transform.position.x < endPos.x)
        {
            subGround1.transform.position = startPos;

        }
        if (subGround2.transform.position.x < endPos.x)
        {
            subGround2.transform.position = startPos;
        }
    }
    
   

    
}
