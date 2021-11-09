using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StartMenu : Menu<StartMenu>
{
    private List<RectTransform> imageList = new List<RectTransform>();
    private Sequence sequence;
    [SerializeField]
    private Button startBtn;
    private float speed = 0.3f;

    // protected override void Awake()
    // {
    //     base.Awake();

    //     for (int i = 0; i < 4; i++)
    //     {
    //         imageList.Add(this.gameObject.transform.GetChild(i).GetComponent<RectTransform>());
    //     }
    //     startBtn.onClick.AddListener(() => ClickStart());
    // }
    // public void ClickStart()
    // {
    //     LoadMenu.Open();
    //     LoadMenu.Instance.Active();
    //     //   sequence = DOTween.Sequence().SetAutoKill(false)
    //     //   .Append(imageList[0].DOAnchorPosX(-810f, speed))
    //     //   .Join(imageList[1].DOAnchorPosX(810f, speed))
    //     //  .Append(imageList[2].DOAnchorPosX(810f,speed))
    //     //   .Join(imageList[3].DOAnchorPosX(-810f, speed))
    //     //   .AppendCallback(() =>gameObject.SetActive(false));
    // }




}
