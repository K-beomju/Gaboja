using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System.Collections;


[System.Serializable]
public class Item
{
    public int itemType;
    public Sprite itemImg;
    public int swordID;

}

public class Merge : Singleton<Merge>
{
    [Header("UI")]
    public Button autoSortBtn;
    public Button autoMergeBtn;
    public Button createSwordBtn;

    public Slider autoSortSlider;
    public float sortCTime;

    public Slider autoMergeSlider;
    public float mergeCTime;

    public NewSwordPanel newSwordPanel;
    private EffectObject effectObject;



    public DataClass data;
    [HideInInspector]
    public int ID = 0;


    public GameObject[] sortPos; // 정렬을 위한 트랜스폼의 위치입니다
    public List<MergeItem> swordList = new List<MergeItem>();
    public List<Item> itemdata = new List<Item>();
    [SerializeField]
    private GameObject itemPrefabs;
    private GameObject go;

    [SerializeField]
    private BoxCollider2D createArea;
    public GameObject parentObj;
    private Vector3 magnetPos;
    private int nextNum = 0;
    public MergeItem merge;




    protected override void Awake()
    {
        base.Awake();



        createSwordBtn.onClick.AddListener(() => ItemCreate(0));
        autoMergeBtn.onClick.AddListener(() => AutoMerge(0));
        autoSortBtn.onClick.AddListener(() => SortSword());


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
            GameObject go = createSword(createSwordBtn.transform.position, num);
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
        go = Instantiate(itemPrefabs, pos, Quaternion.identity);
        MergeItem item = go.GetComponent<MergeItem>();
        item.InitItem(itemdata[num]);

        swordList.Add(item);
        return go;
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


        StartCoroutine(CoolTimes(autoMergeSlider, autoMergeBtn, mergeCTime));
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

        swordList = swordList.OrderByDescending(x => x.item.itemType).ToList();
        for (int i = 0; i < swordList.Count; i++)
        {
            //단 이 로직으로 할경우 합치는 도중에 정렬을 해버리면 합쳐지는 녀석은 DOTween이 무시됨.
            // 합쳐지고 았을 때는 정렬이 안되도록 막는 로직이 필요함.
            swordList[i].transform.DOMove(sortPos[i].transform.position, 1);


        }
        StartCoroutine(CoolTimes(autoSortSlider, autoSortBtn, sortCTime));
    }

    public IEnumerator CoolTimes(Slider slider, Button button, float coolTime)
    {
        slider.value = 0;

        button.interactable = false;
        while (slider.value < 1)
        {
            slider.value += 1 * Time.smoothDeltaTime / coolTime;
            yield return null;
        }
        Debug.Log("완료");
        button.interactable = true;


        yield break;
    }



}