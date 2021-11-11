using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public enum MergeUICategory
{
    AutoMerge = 0,
    AutoCreate = 1
}

public class MergeUi : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Button createSwordBtn;
    [SerializeField] private Button sortBtn;
    [SerializeField] private Button autoMergeBtn;
    [SerializeField] private Button autoCreateBtn;


    [Header("CreateButton")]
    [SerializeField] private Text countTxt;
    [SerializeField] private Image swordFill;

    [SerializeField] private Slider swordSlider;
    [SerializeField] private Slider sortSlider;
    [SerializeField] private Slider autoMergeSlider;
    [SerializeField] private Slider autoCreateSlider;
    [SerializeField] private Slider autoDiaMineSlider;
    [SerializeField] private Slider autoEmeraldMinSlider;


    [SerializeField] private Merge merge;
    [SerializeField] private NoficationPopup noficationPopup;
    [SerializeField] private Image diaImage;
    [SerializeField] private Image emeraldImage;

    public Text autoMergeTxt;
    public Text autoCreateTxt;



    public bool isCreateReload;
    public bool isAutoMerge;
    public bool isAutoCreate;


    [SerializeField] private JsonSave json;
    [SerializeField] private UiManager uiManager;



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
            StartCoroutine(SortCO(sortSlider, sortBtn, json.GetDataClass().sortTime));
        });

        autoMergeBtn.onClick.AddListener(() =>
        {
            noficationPopup.gameObject.SetActive(true);
            noficationPopup.Init((int)MergeUICategory.AutoMerge);

        });

         autoCreateBtn.onClick.AddListener(() =>
        {
            noficationPopup.gameObject.SetActive(true);
               noficationPopup.Init((int)MergeUICategory.AutoCreate);

        });

    }

    void Start()
    {
        isAutoCreate = true;
        isAutoMerge = true;
        isCreateReload = true;


        StartCoroutine(SortCO(sortSlider, sortBtn,  json.GetDataClass().sortTime));

        AutoSystem("자석");
        AutoSystem("모루");
        AutoSystem("다이아채굴");
        AutoSystem("에메랄드채굴");


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

    public void AutoSystem(string i)
    {
        switch (i)
        {
            case "자석":
                StartCoroutine(AutoSystemCO(autoMergeSlider,  json.GetDataClass().autoMergeTime,
                merge.AutoMerge, isAutoMerge, MergeUICategory.AutoMerge));
            break;

            case "모루":
                StartCoroutine(AutoSystemCO(autoCreateSlider,  json.GetDataClass().autoCreateTime,
                () => { merge.SystemItemCreate(0); } , isAutoCreate,  MergeUICategory.AutoCreate));
            break;

            case "다이아채굴":
                StartCoroutine(AutoMine(autoDiaMineSlider,  json.GetDataClass().autoDiaMineTime , diaImage));
            break;

             case "에메랄드채굴":
                StartCoroutine(AutoMine(autoEmeraldMinSlider,  json.GetDataClass().autoDiaMineTime , emeraldImage));
            break;

        }

    }

    public void ReloadCo()
    {
        StartCoroutine(Reload());

    }

    public IEnumerator AutoSystemCO(Slider slider, float coolTime, Action func, bool isStart, MergeUICategory mergeUICategory)
    {

        slider.value = 0;
        if (slider.value != 1)
        {
            while (slider.value < 1) // 슬라이더의 값이 1 미만일때까지 실행
            {
                slider.value += 1 * Time.smoothDeltaTime / coolTime;
                yield return null;
            }

        }



           isStart = noficationPopup.SearchBool((int)mergeUICategory);
        while(!isStart) // isStart가 false때 실행
        {
             isStart = noficationPopup.SearchBool((int)mergeUICategory);
            yield return Yields.WaitSeconds(3f);
            yield return null;
        }
        func();
        yield break;
    }




    public IEnumerator Reload() // 재충전 , SwordCreateBtn
    {
        if (uiManager.sword != json.GetDataClass().swordMax &&isCreateReload)
        {
            swordFill.fillAmount = 1;
            swordSlider.value = 0;
            isCreateReload = false;
            while (swordFill.fillAmount > 0)
            {
                swordFill.fillAmount -= 1 * Time.smoothDeltaTime /  json.GetDataClass().createSwordTime;
                swordSlider.value += 1 * Time.smoothDeltaTime /  json.GetDataClass().createSwordTime;
                yield return null;
            }
           uiManager.sword++;
           uiManager.SetMakeSword();
            isCreateReload = true;

            if (uiManager.sword != json.GetDataClass().swordMax)
            {
                StartCoroutine(Reload());
            }

            yield break;
        }

    }


     public IEnumerator AutoMine(Slider slider, float coolTime , Image image)
    {
        slider.value = 0;
        while (slider.value < 1)
        {
            slider.value += 1 * Time.smoothDeltaTime / coolTime;

            yield return null;
        }
        image.gameObject.SetActive(true);
        yield break;
    }




}
