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
    public List<MergeItem> swordList = new List<MergeItem>();
    [SerializeField] public List<Item> itemdata = new List<Item>();
    [SerializeField] private RectTransform[] sortPos; // 정렬을 위한 트랜스폼의 위치입니다
    [SerializeField] private RectTransform spawnPos;

    [SerializeField] private NewSwordPanel newSwordPanel;
    [SerializeField] private MergeUi mergeUi;
    [SerializeField] private UiManager uiManager;

    [SerializeField] private GameObject itemPrefabs;
    [SerializeField] private Image magnetImage;


    private EffectObject effectObject;
    private BoxCollider2D createArea;
    private GameObject sword;
    private Vector3 magnetPos;

    private int nextNum;
    private bool isMerge = false;

    public int ID { get; set; }
    public GameObject parentObj;
    public int newSwordIndex = -1;

    public bool bSword = false;



    protected override void Awake()
    {
        base.Awake();

        nextNum = 0;
        createArea = GetComponent<BoxCollider2D>();
    }





    public bool IdleIsHave()
    {
        return bSword;
    }

    public int currentSword()
    {
        return swordList.Count;
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





    // Create

    public void ItemCreate(int num)
    {
        if (uiManager.sword > 0)
        {
            bSword = true;
            Vector3 randomPos = GetRandomPosition();
            GameObject go = createSword(spawnPos.position, num);
            go.transform.DOMove(randomPos, 1f);
            uiManager.sword--;
            uiManager.SetMakeSword();
            uiManager.SetCountSword();

        }
    }


    public void SystemItemCreate(int num)
    {
        StartCoroutine(AutoItemCreate(num));
    }

    public IEnumerator AutoItemCreate(int num)
    {
        while (uiManager.sword == 0)
        {
            uiManager.returnSword();
            yield return Yields.WaitSeconds(3f);
            yield return null;
        }

        if (uiManager.sword > 0)
        {
            Vector3 randomPos = GetRandomPosition();
            GameObject go = createSword(spawnPos.position, num);
            go.transform.DOMove(randomPos, 1f);

            uiManager.sword--;
            uiManager.SetMakeSword();
            uiManager.SetCountSword();


            mergeUi.AutoSystem("모루");
            mergeUi.ReloadCo();
        }

        yield break;
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



    // Merge


    public void mergingItem(int num)
    {
        Vector3 Pos = Input.mousePosition;

        effectObject = GameManager.GetCreateCanvasEffect(0);
        effectObject.SetPositionData(Pos, Quaternion.identity);
        GameObject go = createSword(Pos, num);
        uiManager.SetCountSword();


    }

    public void AutoMerge()
    {
        StartCoroutine(SearchSword());
    }

    private IEnumerator SearchSword() // Error - 검이 없으면 에러남 index 에러
    {

        MergeItem swordFinding = null;
        MergeItem sword = null;

        while (swordFinding == null) // swordFinding가 null이면 계속 반복
        {
            sword = swordList[Random.Range(0, swordList.Count)];
            foreach (var i in swordList)
            {
                if (i.item.itemType == sword.item.itemType &&
                   i.item.swordID != sword.item.swordID) //타입이 같고 , 아이디가 다른 객체찾기
                {
                    swordFinding = i;
                }
            }
            if (swordFinding == null && !mergeUi.isAutoMerge)
            {
                yield return Yields.WaitSeconds(3f);
                Debug.Log("같은 등급의 검을 찾는 중");
            }
            yield return null;
        }



        //찾으면 실행
        Vector3 randomPos = GetRandomPosition();
        magnetPos = randomPos;
        StartCoroutine(AutoMergeDelay(sword, swordFinding, randomPos));
        Debug.Log("AutoMergeDelay 실행");
        yield break;
    }


    private IEnumerator AutoMergeDelay(MergeItem sword, MergeItem swordFinding, Vector3 randomPos)
    {
        isMerge = true;
        // 자석 이미지 활성화, 이펙트 풀링
        magnetImage.gameObject.SetActive(true);
        magnetImage.rectTransform.position = magnetPos;
        effectObject = GameManager.GetCreateCanvasEffect(1);
        effectObject.SetPositionData(magnetPos, Quaternion.identity);


        sword.transform.DOMove(randomPos, 1f);   // 검 이동
        swordFinding.transform.DOMove(randomPos, 1f);


        nextNum = swordFinding.item.itemType + 1;    // 검 등급 증가 --> 나중에 제작술을 만들게 되면 확률 구성을 해야함


        Dead(sword, 1.3f);  // Dead F12 누르면 함수 설명으로 이동
        Dead(swordFinding, 1.3f);


        yield return Yields.WaitSeconds(1.3f);  // Yields F12 누르면 함수 설명으로 이동


        createSword(magnetPos, nextNum); // 검 제작

        // 자석 이미지 비활성화, 이펙트 풀링
        magnetImage.gameObject.SetActive(false);
        effectObject = GameManager.GetCreateCanvasEffect(0);
        effectObject.SetPositionData(magnetPos, Quaternion.identity);

        isMerge = false;
        mergeUi.AutoSystem("자석");
        uiManager.SetCountSword();


        yield break; // 종료

    }



    // Etc
    public void SortSword()
    {
        if (!isMerge)
        {

            //단 이 로직으로 할 경우 합치는 도중에 정렬을 해버리면 합쳐지는 녀석은 DOTween이 무시됨. 합쳐지고 았을 때는 정렬이 안되도록 막는 로직이 필요함.
            swordList = swordList.OrderByDescending(x => x.item.itemType).ToList();
            for (int i = 0; i < swordList.Count; i++)
            {
                swordList[i].transform.DOMove(sortPos[i].transform.position, 1);
            }
        }

    }


    public void Dead(MergeItem mergeItem, float count = 0)
    {
        swordList.Remove(mergeItem);
        Destroy(mergeItem.gameObject, count);
    }

    public void CheckNewSword(int itemType)
    {
        if (itemType > newSwordIndex)
        {

            newSwordIndex = itemType;
            newSwordPanel.Init();
            newSwordPanel.gameObject.SetActive(true);
        }

    }



    // ATTACK
    private int rand;
    public int SwordInit()
    {
        rand  = Random.Range(0, swordList.Count);
        return swordList[rand].item.swordPower;
    }

    public void SwordEffect(Transform tr)
    {


        switch(swordList[rand].item.itemType)
        {
            case 0:
                effectObject = GameManager.GetSwordEffect(0);
                effectObject.SetPositionData(tr.position , Quaternion.identity);
            break;

            case 1:

            break;
        }
    }



    public IEnumerator SwordAttack()
    {
        while (GameManager.isAttack)
        {
            List<Item> item = new List<Item>();
            for (int i = 0; i < swordList.Count; i++)
            {
                item.Add(swordList[i].item);
                Debug.Log(item);
            }
            yield return Yields.WaitSeconds(3);
            yield return null;
        }


        yield break;
    }










}