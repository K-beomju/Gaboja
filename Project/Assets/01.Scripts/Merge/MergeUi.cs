using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MergeUi : MonoBehaviour
{
    [Header("UI")]
    public Button autoSortBtn;
    public Button autoMergeBtn;
    public Button createSwordBtn;

    public Slider autoSortSlider;
    public Slider autoMergeSlider;

    public float sortTime;
    public float mergeTime;

    public Merge merge;


    void Awake()
    {
        createSwordBtn.onClick.AddListener(() => merge.ItemCreate(17));
        autoSortBtn.onClick.AddListener(() =>{
             merge.SortSword();
             StartCoroutine(CoolTimes(autoSortSlider, autoSortBtn, sortTime));
        });
        autoMergeBtn.onClick.AddListener(() => {
            merge.AutoMerge(0);
            StartCoroutine(CoolTimes(autoMergeSlider, autoMergeBtn, mergeTime));
        });

    }

    public void CoolTime(Slider slider, Button button, float coolTime)
    {
        StartCoroutine(CoolTimes(slider,button,coolTime));
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
