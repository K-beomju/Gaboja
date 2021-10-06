using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordManager : MonoBehaviour
{
    public static SwordManager instance;

    public GameObject[] SwordObject;  //칼 오브젝트
    List<GameObject> SwordList = new List<GameObject>();

    public BoxCollider2D CreatArea;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CreateSword();
        }
    }

    public Vector3 GetRandomPosition() // 랜덤 위치 받아오기
    {
        Vector3 basePosition = transform.position;
        Vector3 size = CreatArea.size;

        float posX = basePosition.x + Random.Range(-size.x / 2.5f, size.x / 2.5f);
        float posY = basePosition.y + Random.Range(-size.y /2.5f, size.y / 2.5f);

        Vector3 randomPos = new Vector3(posX, posY, 0);

        return randomPos;
    }

    private void CreateSword() // 칼 생성 함수
    {
        Vector3 createPos = GetRandomPosition();
        Instantiate(SwordObject[0], createPos, Quaternion.identity);
    }


}
