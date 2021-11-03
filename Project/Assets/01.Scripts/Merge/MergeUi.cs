using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class MergeUi : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Button sortBtn;
    [SerializeField] private Button autoMergeBtn;
    [SerializeField] private Button createSwordBtn;

    [Header("CreateButton")]
    [SerializeField] private Text countTxt;
    [SerializeField] private Image swordFill;

    [SerializeField] private Slider swordSlider;
    [SerializeField] private Slider sortSlider;
    [SerializeField] private Slider autoMergeSlider;
    [SerializeField] private Slider autoCreateSlider;


    [SerializeField] private float sortTime;
    [SerializeField] private float mergeTime;
    [SerializeField] private float createTime;

    [SerializeField] private DataClass dataClass;
    [SerializeField] private Merge merge;
    [SerializeField] private NoficationPopup noficationPopup;

    public Text autoMergeTxt;


    public int sword;
    public bool isCreateReload;
    public bool isAutoMerge = true;


    public delegate void MergeDel();
    public event MergeDel mergeDel;


    void Awake()
    {
        createSwordBtn.onClick.AddListener(() =>
        {
            merge.ItemCreate(0);
            StartCoroutine(Reload());
        });

        sortBtn.onClick.AddListener(() =>
        {
            merge.SortSword();
            StartCoroutine(SortCO(sortSlider, sortBtn, sortTime));
        });

        autoMergeBtn.onClick.AddListener(() =>
        {
            noficationPopup.gameObject.SetActive(true);

        });

    }

    void Start()
    {
        isCreateReload = true;
        sword = dataClass.swordMax;
        UiManager.Instance.SetSword(sword);
        StartCoroutine(SortCO(sortSlider, sortBtn, sortTime));
        AutoSystem(0);
        // AutoSystem(1);

    }


    public IEnumerator SortCO(Slider slider, Button button, float coolTime)
    {
        slider.value = 0;
        button.interactable = false;
        while (slider.value < 1)
        {
            slider.value += 1 * Time.smoothDeltaTime / coolTime;

            yield return null;
        }
        button.interactable = true;
        yield break;
    }

    public void AutoSystem(int i)
    {
        switch (i)
        {
            case 0:
                StartCoroutine(AutoSystemCO(autoMergeSlider, mergeTime, merge.AutoMerge));
                break;

            case 1:
                StartCoroutine(AutoSystemCO(autoCreateSlider, createTime, () => { merge.ItemCreate(0); }));
                break;

        }

    }

    public IEnumerator AutoSystemCO(Slider slider, float coolTime, Action func)
    {

        slider.value = 0;
        if (slider.value != 1)
        {
            while (slider.value < 1)
            {
                slider.value += 1 * Time.smoothDeltaTime / coolTime;
                yield return null;
            }

        }

        while(isAutoMerge)
        {
            yield return Yields.WaitSeconds(3f);
            Debug.Log("대기중");
            yield return null;
        }
        func();
        yield break;
    }



    public IEnumerator Reload() // 재충전 , SwordCreateBtn
    {
        if (sword < dataClass.swordMax && isCreateReload)
        {
            Debug.Log("Reload 실행");
            swordFill.fillAmount = 1;
            swordSlider.value = 0;
            isCreateReload = false;
            while (swordFill.fillAmount > 0)
            {
                swordFill.fillAmount -= 1 * Time.smoothDeltaTime / createTime;
                swordSlider.value += 1 * Time.smoothDeltaTime / createTime;
                yield return null;
            }
            sword++;
            UiManager.Instance.SetSword(sword);
            isCreateReload = true;

            if (sword != dataClass.swordMax)
            {
                StartCoroutine(Reload());
            }

            yield break;
        }
    }




}
