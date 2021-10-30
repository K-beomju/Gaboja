using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System.Collections;


[System.Serializable]
public class Item
{
    public Sprite itemImg; // 이미지
    public int itemType; // 등급
    public int swordID; // 고유 번호
    public string swordName; // 이름
    public int swordPower; // 공격력

}

public class Merge : Singleton<Merge>
{
    [SerializeField] private List<MergeItem> swordList = new List<MergeItem>();
    [SerializeField] public List<Item> itemdata = new List<Item>();
    [SerializeField] private RectTransform[] sortPos; // 정렬을 위한 트랜스폼의 위치입니다
    [SerializeField] private RectTransform spawnPos;

    [SerializeField] private DataClass data;
    [SerializeField] private GameObject itemPrefabs;

    private EffectObject effectObject;
    private BoxCollider2D createArea;
    private GameObject sword;
    private Vector3 magnetPos;
    private int nextNum;



    public int ID{ get; set;}
    public GameObject parentObj;
    public NewSwordPanel newSwordPanel;
    public int newSwordIndex = -1;









    protected override void Awake()
    {
        base.Awake();

        nextNum = 0;
        createArea = GetComponent<BoxCollider2D>();
    }


    public Vector3 GetRandomPosition() // 랜덤 위치 받아오기
    {
        Vector3 basePosition = transform.position;
        Vector3 size = createArea.size;
        float posX = basePosition.x + Random.Range(-size.x / 2.5f, size.x / 2.5f);
        float posY = basePosition.y + Random.Range(-size.y / 2.5f, size.y / 2.5f);
        Vector3 randomPos = new Vector3(posX, posY, 0);
        return randomPos;

    }
    public void ItemCreate(int num)
    {
        if (data.sword > 0)
        {
            Vector3 randomPos = GetRandomPosition();
            GameObject go = createSword(spawnPos.position, num);
            go.transform.DOMove(randomPos, 1f);
            data.sword--;
            UiManager.Instance.SetSword();
            if (data.sword < data.swordMax && !UiManager.Instance.bRelad)
            {
                UiManager.Instance.ReloadSword();
            }
        }
    }

    public GameObject createSword(Vector3 pos, int num)
    {
        sword = Instantiate(itemPrefabs, pos, Quaternion.identity);
        MergeItem item = sword.GetComponent<MergeItem>();
        item.InitItem(itemdata[num]);

        swordList.Add(item);
        CheckNewSword(item.item.itemType);

        return sword;
    }

    public void mergingItem(int num)
    {
        Vector3 Pos = Input.mousePosition;
        effectObject = GameManager.GetCreateCanvasEffect(0);
        effectObject.SetPositionData(Pos, Quaternion.identity);
        GameObject go = createSword(Pos, num);
    }


    public void AutoMerge(int num)
    {
        Vector3 randomPos = GetRandomPosition();
        magnetPos = randomPos;
        MergeItem sword = swordList[Random.Range(0, swordList.Count)];
        MergeItem swordFinding = null;

        //같은 칼 검색
        foreach (var i in swordList)
        {
            if (i.item.itemType == sword.item.itemType &&
               i.item.swordID != sword.item.swordID) //타입이 같고 , 아이디가 다른 객체찾기
            {
                swordFinding = i;
            }
        }

        if (swordFinding == null)
        {


        }
        if (swordFinding != null)
        {
            sword.transform.DOMove(randomPos, 1f);
            swordFinding.transform.DOMove(randomPos, 1f);

            nextNum = swordFinding.item.itemType + 1;


            Destroy(swordFinding.gameObject, 1.3f);
            Destroy(sword.gameObject, 1.3f);

            swordList.Remove(sword);
            swordList.Remove(swordFinding);

            Invoke("AutoMergeDelay", 1.3f);
        }
    }

    void AutoMergeDelay()
    {
        createSword(magnetPos, nextNum);
        effectObject = GameManager.GetCreateCanvasEffect(0);
        effectObject.SetPositionData(magnetPos, Quaternion.identity);
    }

    public void SortSword()
    {
        //단 이 로직으로 할 경우 합치는 도중에 정렬을 해버리면 합쳐지는 녀석은 DOTween이 무시됨. 합쳐지고 았을 때는 정렬이 안되도록 막는 로직이 필요함.
        swordList = swordList.OrderByDescending(x => x.item.itemType).ToList();
        for (int i = 0; i < swordList.Count; i++)
        {
            swordList[i].transform.DOMove(sortPos[i].transform.position, 1);
        }

    }

    public void Dead(MergeItem mergeItem)
    {
        swordList.Remove(mergeItem);
        Destroy(mergeItem.gameObject);
    }


    public void CheckNewSword(int itemType)
    {
        if(itemType > newSwordIndex)
        {
            Debug.Log("새로운 검 획득");
            newSwordIndex = itemType;
            newSwordPanel.Init();
            newSwordPanel.gameObject.SetActive(true);


        }
    }





}