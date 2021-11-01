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
    [SerializeField] private Text cashTxt;


    [Header("CreateButton")]
    [SerializeField] private Text countTxt;
    [SerializeField] private Image swordFill;
    [SerializeField] private Slider swordSlider;


    public DataClass dataClass;

    protected override void Awake()
    {
        base.Awake();

    }



    void Start()
    {
        SetGold();
    }


    public void SetGold() // 골드
    {
        goldTxt.text = string.Format("{0}", dataClass.gold.ToString("n0"));

    }

    public void SetSword(int sword) // 현재 갯수 / 최대 갯수
    {
        countTxt.text = string.Format("{0}/{1}", sword, dataClass.swordMax);
        swordTxt.text = string.Format("{0}/{1}", sword, dataClass.swordMax);
    }











}
