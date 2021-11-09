using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;


public class AutoDiaMine : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private MergeUi mergeUi;
    [SerializeField] private RectTransform rect;
    [SerializeField] private RectTransform lightImage;
    [SerializeField] private float speed;

    private Image image;
    private RectTransform rectTransform;
    private Vector3 startPos;

    void Awake()
    {
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
        startPos = transform.position;
    }

    void OnEnable()
    {
        lightImage.gameObject.SetActive(true);
    }

    void Update()
    {
        Rotate();
    }

    public void Rotate()
    {
        lightImage.Rotate(0, 0, speed * Time.deltaTime);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        image.DOFade(0, 2).OnComplete(() => image.color = new Color(255f/255f,255f/255f,255f/255f,255f/255f));

        rectTransform.DOMove(rect.position, 1)
        .OnComplete(() =>
        {
            gameObject.SetActive(false);
            this.transform.position = startPos;

            JsonSave.instance.GetDataClass().dia++;
            UiManager.Instance.SetDia();
        });

        mergeUi.AutoSystem("다이아채굴");
        lightImage.gameObject.SetActive(false);


    }


}
