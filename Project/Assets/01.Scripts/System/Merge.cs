using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[System.Serializable]
public class Item
{
    public int SwordID;
    public Sprite SwordIMG;
}

public class Merge : MonoBehaviour
{
    public List<Item> itemdata = new List<Item>();
    public GameObject SwordList;
    public BoxCollider2D CreateArea;

    public GameObject SpawnObject;
    private GameObject item;
    public GameObject itemPrefabs;


    public Vector3 GetRandomPosition() // 랜덤 위치 받아오기
    {
        Vector3 basePosition = transform.position;
        Vector3 size = CreateArea.size;

        float posX = basePosition.x + Random.Range(-size.x / 2.5f, size.x / 2.5f);
        float posY = basePosition.y + Random.Range(-size.y / 2.5f, size.y / 2.5f);

        Vector3 randomPos = new Vector3(posX, posY, 0);

        return randomPos;
    }

    public void ItemCreate(int num) //아이템 크리에이트 (다음 아이템 만드는거 까지)
    {
        


        Vector3 SpwanPos = GetRandomPosition();
        item = Instantiate(itemPrefabs, SpawnObject.transform.position, Quaternion.identity);  
        item.GetComponent<MergeItem>().InitItem(itemdata[num], SwordList);
        item.transform.DOMove(SpwanPos, 0.5f);
         

      

    }

    public void MergeItem(int num)
    {
        Vector3 MergePos = Input.mousePosition;
        item = Instantiate(itemPrefabs, MergePos, Quaternion.identity);
        item.GetComponent<MergeItem>().InitItem(itemdata[num], SwordList);


    }

    
}