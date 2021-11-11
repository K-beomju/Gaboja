using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class UiManager : Singleton<UiManager>
{

    [Header("Data Group")]
    [SerializeField] private Text swordTxt;
    [SerializeField] private Text goldTxt;
    [SerializeField] private Text diaTxt;


    [Header("CreateButton")]
    [SerializeField] private Text countTxt;
    [SerializeField] private Image swordFill;
    [SerializeField] private Slider swordSlider;

    public int sword;
    public JsonSave jsonSave;

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
        swordTxt.text = string.Format("{0}/{1}", Merge.Instance.currentSword(),jsonSave.GetDataClass().swordCurrent);
    }

    public void SetMakeSword() // 검 제작 최대치 보유
    {
        countTxt.text = string.Format("{0}/{1}", sword,jsonSave.GetDataClass().swordMax);
    }


    public void SetGold() // 골드
    {
        goldTxt.text = string.Format("{0}", jsonSave.GetDataClass().gold.ToString("n0"));
    }

     public void SetDia() // 다이아
    {
        diaTxt.text = string.Format("{0}", jsonSave.GetDataClass().dia.ToString("n0"));
    }












}
