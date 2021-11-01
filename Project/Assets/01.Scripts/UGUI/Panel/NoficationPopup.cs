using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NoficationPopup : MonoBehaviour
{
    [SerializeField] private Text itemActiveTxt;

    private Sequence mySequence;


     void OnEnable()
    {
        mySequence = DOTween.Sequence()
     .OnStart(() =>
     {
         transform.localScale = Vector3.zero;
     })
     .Append(transform.DOScale(1, 0.6f));

    }

    public void Init(string name, string on)
    {
        itemActiveTxt.text = string.Format("{0}을 {1}시겠습니까?", name, on);
        gameObject.SetActive(true);

    }

}
