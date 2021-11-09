using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;




public class NoficationPopup : MonoBehaviour
{
    [SerializeField] private Text itemActiveTxt;
    [SerializeField] private Button checkButton;
    [SerializeField] private MergeUi mergeUi;

    private Sequence mySequence;
    private int mergeKind;

    void Awake()
    {
        checkButton.onClick.AddListener(() =>
        {
            this.gameObject.SetActive(false);
            Test();
        });
    }


    void OnEnable()
    {
        mySequence = DOTween.Sequence().OnStart(() =>
        { transform.localScale = Vector3.zero; })
        .Append(transform.DOScale(1, 0.6f));




    }


    public void Init(int uiCategory)
    {
        switch (uiCategory)
        {
            case 0:
                if (mergeUi.isAutoMerge)
                {
                    itemActiveTxt.text = "자석을 끄시겠습니까?";
                }
                else
                {
                    itemActiveTxt.text = "자석을 켜시겠습니까?";
                }

                break;

            case 1:
                if (mergeUi.isAutoCreate)
                {
                    itemActiveTxt.text = "모루를 끄시겠습니까?";
                }
                else
                {
                    itemActiveTxt.text = "모루를 켜시겠습니까?";
                }
                break;
        }
        mergeKind = uiCategory;
    }



    public void Test()
    {
        switch (mergeKind)
        {
            case 0:
                if (mergeUi.isAutoMerge)
                {
                    mergeUi.isAutoMerge = false;
                    mergeUi.autoMergeTxt.text = "OFF";
                    mergeUi.autoMergeTxt.color = Color.red;
                }
                else
                {
                    mergeUi.isAutoMerge = true;
                    mergeUi.autoMergeTxt.text = "ON";
                    mergeUi.autoMergeTxt.color = Color.green;
                }
                break;


            case 1:
                if (mergeUi.isAutoCreate)
                {
                    mergeUi.isAutoCreate = false;
                    mergeUi.autoCreateTxt.text = "OFF";
                    mergeUi.autoCreateTxt.color = Color.red;
                }
                else
                {
                    mergeUi.isAutoCreate = true;
                    mergeUi.autoCreateTxt.text = "ON";
                    mergeUi.autoCreateTxt.color = Color.green;
                }
                break;
        }


    }

    public bool SearchBool(int a)
    {
        if (a == 0)
        {
            if (mergeUi.isAutoMerge)
            {
                return true;

            }
        }
        if(a == 1)
        {
             if (mergeUi.isAutoCreate)
            {
                return true;

            }
        }
        return false;



    }







}
