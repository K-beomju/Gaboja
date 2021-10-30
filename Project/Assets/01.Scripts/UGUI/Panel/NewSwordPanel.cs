using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NewSwordPanel : MonoBehaviour
{
    [SerializeField] private Image swordImage;
    [SerializeField] private Image lightImage;
    [SerializeField] private Button checkBtn;
    [SerializeField] private float speed;
    [SerializeField] private Merge merge;


    [SerializeField] private Text swordNameTxt;
    [SerializeField] private Text swordPowerTxt;

    private Sequence mySequence;
    private string test;

    void Awake()
    {
        this.transform.localScale = Vector3.zero;
        this.gameObject.SetActive(false);
        checkBtn.onClick.AddListener(() => OffPanel());
    }



    public void Init()
    {

        swordImage.sprite = merge.itemdata[Merge.Instance.newSwordIndex].itemImg;
        swordNameTxt.text = string.Format("{0}.{1}", merge.itemdata[Merge.Instance.newSwordIndex].itemType + 1, merge.itemdata[Merge.Instance.newSwordIndex].swordName);
        swordPowerTxt.text = string.Format("공격력 : {0}", merge.itemdata[Merge.Instance.newSwordIndex].swordPower.ToString());

    }

    void OnEnable()
    {
        mySequence = DOTween.Sequence()
     .OnStart(() =>
     {
         transform.localScale = Vector3.zero;
     })
     .Append(transform.DOScale(0.5f, 0.5f));

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
