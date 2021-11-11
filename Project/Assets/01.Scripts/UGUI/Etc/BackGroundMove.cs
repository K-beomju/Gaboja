using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMove : MonoBehaviour
{

    [SerializeField] private MeshRenderer[] backGrounds;

    [SerializeField] private float offsetBackSpeed;
    private float offsetBack;

    [SerializeField]private float offsetMidSpeed;
    private float offsetMid;

    [SerializeField]private float offsetGroundSpeed;
    private float offsetGround;


   [SerializeField] private GameObject subGround1;
   [SerializeField] private GameObject subGround2;

   [SerializeField] private float subGroundSpeed;

    private Vector3 endPos = new Vector3(-5.74f,0,0);
    private Vector3 startPos;

    public bool isAttack = true;

    private void Start()
    {
        startPos  = new Vector3(6,transform.position.y,0);
        backGrounds = GetComponentsInChildren<MeshRenderer>();
    }
    private void Update()
    {
        if(isAttack)
        {
        SubBackGroundMove();
        EtcMove();
        }
    }

    public void EtcMove()
    {
        offsetBack += Time.deltaTime * offsetBackSpeed;
        offsetMid += Time.deltaTime * offsetMidSpeed;
        offsetGround += Time.deltaTime * offsetGroundSpeed;

        backGrounds[0].material.mainTextureOffset = new Vector2(offsetBack , 0);
        backGrounds[1].material.mainTextureOffset = new Vector2(offsetMid , 0);
        backGrounds[2].material.mainTextureOffset = new Vector2(offsetGround, 0);
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
