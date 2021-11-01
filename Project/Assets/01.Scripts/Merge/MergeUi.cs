using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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

    [SerializeField] private float sortTime;
    [SerializeField] private float mergeTime;
    [SerializeField] private float createTime;

    [SerializeField] private DataClass dataClass;
    [SerializeField] private Merge merge;
    [SerializeField] private NoficationPopup noficationPopup;


    public int sword;
    public bool bCreateReload = false;
    public bool bAutoMerge = false;

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
            StartCoroutine(CoolTimes(sortSlider, sortBtn, sortTime));
        });

        autoMergeBtn.onClick.AddListener(() =>
        {
            merge.AutoMerge(0);
        //    StartCoroutine(CoolTimes(autoMergeSlider, autoMergeBtn, mergeTime));


        });

    }

    void Start()
    {
        sword = dataClass.swordMax;
        UiManager.Instance.SetSword(sword);
        StartCoroutine(CoolTimes(autoMergeSlider, autoMergeBtn, mergeTime));

    }

    public void CoolTime(Slider slider, Button button, float coolTime)
    {
        StartCoroutine(CoolTimes(slider, button, coolTime));
    }

    public IEnumerator CoolTimes(Slider slider, Button button, float coolTime)
    {
        slider.value = 0;

        while (slider.value < 1)
        {
            slider.value += 1 * Time.smoothDeltaTime / coolTime;

            yield return null;
        }


            merge.AutoMerge(0);




        yield break;
    }



    public IEnumerator Reload() // 재충전 , SwordCreateBtn
    {
        if (sword < dataClass.swordMax && !bCreateReload)
        {
            swordFill.fillAmount = 1;
            swordSlider.value = 0;
            bCreateReload = true;
            while (swordFill.fillAmount > 0)
            {
                swordFill.fillAmount -= 1 * Time.smoothDeltaTime / createTime;
                swordSlider.value += 1 * Time.smoothDeltaTime / createTime;
                yield return null;
            }
            sword++;
            UiManager.Instance.SetSword(sword);
            bCreateReload = false;

            if (sword != dataClass.swordMax)
            {
                StartCoroutine(Reload());
            }

            yield break;
        }
    }




}
