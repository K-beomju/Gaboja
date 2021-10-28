using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NewSwordPanel : MonoBehaviour
{
    public Image swordImage;
    public Image lightImage;
    public Button checkBtn;

    public float speed;
    public Merge merge;
    private Sequence mySequence;

    public Text swordNameTxt;
    public Text swordAttackTxt;


    void Awake()
    {
        checkBtn.onClick.AddListener(() => OffPanel());
    }



    public void Init()
    {
        swordImage.sprite =  merge.itemdata[1].itemImg;
    }

    void OnEnable()
    {
        mySequence = DOTween.Sequence()
     .OnStart(() =>
     {
         transform.localScale = Vector3.zero;
     })
     .Append(transform.DOScale(1, 0.5f));

    }

    public void Rotate()
    {
        lightImage.rectTransform.Rotate(0, 0, speed * Time.deltaTime);
    }

    public void OffPanel()
    {
        this.gameObject.SetActive(false);
    }

    void Update()
    {

        Rotate();
    }




}
