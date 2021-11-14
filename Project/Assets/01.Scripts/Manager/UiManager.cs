using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class UiManager : Singleton<UiManager>
{
    [Header("PlayerInfo")]
    public Text attackTxt;

    [Header("Data Group")]
    [SerializeField] private Text swordTxt;
    [SerializeField] private Text goldTxt;
    [SerializeField] private Text diaTxt;


    [Header("CreateButton")]
    [SerializeField] private Text countTxt;
    [SerializeField] private Image swordFill;
    [SerializeField] private Slider swordSlider;


    [Header("EnemyHpSlider")]
    public Slider hpSlider;
    public Text hpText;
    public Text monsterName;
    public Image image;

    public int sword;
    public Merge merge;


    protected override  void Awake()
    {
        base.Awake();

    }



    void Start()
    {
        sword = JsonSave.instance.GetDataClass().swordMax;
        SetGold();
        SetDia();


        SetCountSword();
        SetMakeSword();
    }

    public int returnSword()
    {
        return sword;
    }


    public void SetCountSword() // 검 보유 갯수
    {
        swordTxt.text = string.Format("{0}/{1}", Merge.Instance.currentSword(),JsonSave.instance.GetDataClass().swordCurrent);
    }

    public void SetMakeSword() // 검 제작 최대치 보유
    {
        countTxt.text = string.Format("{0}/{1}", sword,JsonSave.instance.GetDataClass().swordMax);
    }


    public void SetGold() // 골드
    {
        goldTxt.text = GetDataText(JsonSave.instance.GetDataClass().gold);

    }

     public void SetDia() // 다이아
    {
         diaTxt.text = GetDataText(JsonSave.instance.GetDataClass().dia);
    }

    public void SetAttack()
    {
        attackTxt.text = string.Format("{0}  [+{1}%]",JsonSave.instance.GetUpgradeClass().
        upAbility[0] - JsonSave.instance.GetUpgradeClass().upCost[0], 0);
    }

    public void SetFill(float health, float initHealth, string name, Sprite _image) // 적 hp
    {
        monsterName.text = name;
        hpSlider.DOValue(health  / initHealth ,0.5f);
        hpText.text = string.Format("{0} [{1}]%",health ,  Mathf.Round((health / initHealth * 100)).ToString());
        image.sprite = _image;

    }



    private string[] goldUnitArr = new string[] {"", "만","억","조"};
    public  string GetDataText(double data)
    {
        int placeN = 4;
        double value = data;
        List<int> numList = new List<int>();
        int p = (int)Math.Pow(10, placeN);

        do
        {
            numList.Add((int)(value % p));
            value /= p;
        }
        while(value >= 1);
        string retStr = "";
        for (int i = 0; i < numList.Count; i++)
        {
            retStr = numList[i] + goldUnitArr[i] + retStr;
        }
        return retStr;
    }












}
