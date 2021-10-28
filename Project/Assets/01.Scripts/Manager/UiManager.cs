using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class UiManager : Singleton<UiManager>
{

    [Header("Data Group")]
    public Text swordTxt;
    public Text goldTxt;
    public Text cashTxt;

    private DataClass data;

    [Header("CreateButton")]
    public Text countTxt;

    public Image swordFill;
    public Slider swordSlider;

    private bool isReload = false;
    public float coolTime;
    private float currentCoolTime; //남은 쿨타임을 추적 할 변수
    public bool bRelad = false;
    private event Action UpdateReload;


    protected override void Awake()
    {
        base.Awake();
        data = GetComponent<DataClass>();
        UpdateReload += () => StartCoroutine(Reload());


    }

    void Start()
    {
        SetGold();
        SetSword();
    }


    public void SetGold() // 골드
    {
        goldTxt.text = string.Format("{0}", data.gold.ToString("n0"));
    }

    public void SetSword() // 현재 갯수 / 최대 갯수
    {
        countTxt.text = string.Format("{0}/{1}", data.sword, data.swordMax);
        swordTxt.text = string.Format("{0}/{1}", data.sword, data.swordMax);
    }

    public IEnumerator Reload() // 재충전
    {
        swordFill.fillAmount = 1;
        swordSlider.value = 0;
        bRelad = true;
        while (swordFill.fillAmount > 0)
        {
            swordFill.fillAmount -= 1 * Time.smoothDeltaTime / coolTime;
            swordSlider.value += 1 * Time.smoothDeltaTime / coolTime;
            yield return null;
        }
        data.sword++;
        SetSword();
        bRelad = false;

        if (data.sword != data.swordMax)
        {
            StartCoroutine(Reload());
        }

        yield break;

    }

    public void ReloadSword()
    {
        UpdateReload();
    }










}
