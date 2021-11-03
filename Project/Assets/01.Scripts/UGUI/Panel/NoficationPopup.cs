using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NoficationPopup : MonoBehaviour
{
    [SerializeField] private Text itemActiveTxt;
    [SerializeField] private Button checkButton;
    [SerializeField] private MergeUi mergeUi;

    private Sequence mySequence;

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
        mySequence = DOTween.Sequence()
     .OnStart(() =>
     {
         transform.localScale = Vector3.zero;
     })
     .Append(transform.DOScale(1, 0.6f));




    }
    public void Test()
    {
        if (mergeUi.isAutoMerge)
        {
            itemActiveTxt.text = "자석을 끄시겠습니까?";
            mergeUi.isAutoMerge = false;
            mergeUi.autoMergeTxt.text = "ON";
            mergeUi.autoMergeTxt.color = Color.green;

        }
        else
        {
            itemActiveTxt.text = "자석을 켜시겠습니까?";
            mergeUi.isAutoMerge = true;
            mergeUi.autoMergeTxt.text = "OFF";
            mergeUi.autoMergeTxt.color = Color.red;


        }
    }





}
