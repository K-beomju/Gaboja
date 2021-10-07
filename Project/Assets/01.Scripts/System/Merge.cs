using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public int SwordID;
    public Sprite SwordIMG;
}

public class Merge : MonoBehaviour
{
    public List<Item> itemdata = new List<Item>();
    public GameObject itemPrefabs;
    public GameObject SwordList;
    public BoxCollider2D CreateArea;

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
        GameObject go = Instantiate(itemPrefabs, SpwanPos, Quaternion.identity);
        go.transform.SetParent(SwordList.transform);
        go.GetComponent<MergeItem>().InitItem(itemdata[num]);
    }
}
