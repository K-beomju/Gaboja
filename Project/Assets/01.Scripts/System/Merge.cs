using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;



[System.Serializable]
public class Item
{
    public int SwordID;
    public Sprite SwordIMG;
}

public class Merge : MonoBehaviour
{
    public List<Item> itemdata = new List<Item>();

    public Transform swordList;
    public MergeItem itemPrefabs;
    public Transform spawnObject;

    private BoxCollider2D createArea;

    public event Action<MergeItem, int> merge;

    private void Awake()
    {
        createArea = GetComponent<BoxCollider2D>();



        merge = (x, y) =>
        {
            x.InitItem(itemdata[y], swordList);
        };


    }

    public Vector3 GetRandomPosition()
    {
        Vector3 basePosition = transform.position;
        Vector3 size = createArea.size;

        float posX = basePosition.x + UnityEngine.Random.Range(-size.x / 2.5f, size.x / 2.5f);
        float posY = basePosition.y + UnityEngine.Random.Range(-size.y / 2.5f, size.y / 2.5f);

        Vector3 randomPos = new Vector3(posX, posY, 0);

        return randomPos;
    }


    // 왜인진 모르겠는데 풀링갯수가 초과하면 자동으로 막 합쳐지는 버그있음 ㅋ
    public void ItemCreate(int num)
    {
        Vector3 SpwanPos = GetRandomPosition();
        //MergeItem mergeItem = Instantiate(itemPrefabs, SpawnObject.transform.position, Quaternion.identity);
         MergeItem mergeItem = GameManager.instance.GetCreateSword();
         mergeItem.transform.position = spawnObject.position;
        merge(mergeItem, num);
        mergeItem.transform.DOMove(SpwanPos, 0.5f);
    }

    public void MergeItem(int num)
    {
        Vector3 mergePos = Input.mousePosition;
        MergeItem mergeItem = GameManager.instance.GetCreateSword();
        //MergeItem mergeItem = Instantiate(itemPrefabs, mergePos, Quaternion.identity);
        mergeItem.transform.position = mergePos;
        merge(mergeItem, num);



    }


}