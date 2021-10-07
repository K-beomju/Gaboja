using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SwordInfo
{
    //Į�� ����
    public int SWORD_ID;
    public float SWORD_ATK;
    public string SWORD_NAME;
    public Sprite SWORD_IMG;

}

public class SwordManager : MonoBehaviour
{
    public static SwordManager instance;

    public GameObject SwordObject;  //Į ������Ʈ
    List<SwordInfo> SwordList = new List<SwordInfo>();

    public BoxCollider2D CreatArea;
    public GameObject SwordParents;

    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CreateSword();
        }
    }

    public Vector3 GetRandomPosition() // ���� ��ġ �޾ƿ���
    {
        Vector3 basePosition = transform.position;
        Vector3 size = CreatArea.size;

        float posX = basePosition.x + Random.Range(-size.x / 2.5f, size.x / 2.5f);
        float posY = basePosition.y + Random.Range(-size.y /2.5f, size.y / 2.5f);

        Vector3 randomPos = new Vector3(posX, posY, 0);

        return randomPos;
    }

    public void CreateSword() // Į ���� �Լ�
    {
        Vector3 createPos = GetRandomPosition();
        GameObject swordObj = Instantiate(SwordObject, createPos, Quaternion.identity);
        swordObj.transform.SetParent(SwordParents.transform);
    }

    public void MergeSword(int num)
    {
        GameObject go = Instantiate(SwordObject);
        go.transform.SetParent(SwordParents.transform);
        //go.GetComponent<MergeItem>().InitItem(SwordList[num]);
    }


}
