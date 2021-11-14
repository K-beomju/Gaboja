using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


public class PanelContent : MonoBehaviour
{
    [Header("Upgrade Panel")]
    [SerializeField] private List<Button> buttonList = new List<Button>();
    [SerializeField] private List<Image> imageList = new List<Image>();

    [SerializeField] private  GameObject parentObj;
    [SerializeField] private GameObject pnGroup ;

    [SerializeField] private Sprite sprite;
    private Sprite mySprite;

    public List<GameObject> panelList = new List<GameObject>();
    public ScrollRect scrollRect;
    public RectTransform rectContent;


    void Awake()
    {


        for (int i = 0; i < 3; i++)
        {
            int temp = i;
            Button button = parentObj.transform.GetChild(i).GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                MainButton(temp);
                ChangeSprite(temp);
            });


            buttonList.Add(button);
            panelList.Add(pnGroup.transform.GetChild(i).gameObject);
            imageList.Add(parentObj.transform.GetChild(i).GetComponent<Image>());
        }
        mySprite = imageList[0].sprite;



    }


    // 공통된 함수는 따로 빼서 만들기
    public void MainButton(int i)
    {
        for (int temp = 0; temp < 3; temp++)
        {
            buttonList[temp].interactable = true;
            panelList[temp].gameObject.SetActive(false);
        }
        buttonList[i].interactable = false;
        panelList[i].gameObject.SetActive(true);

        GameObject a =  panelList.Find(x => x.gameObject.activeSelf);
            scrollRect.content = a.GetComponent<RectTransform>();
    }

    public void ChangeSprite(int i)
    {
        for (int temp = 0; temp < 3; temp++)
        {
            imageList[temp].sprite = mySprite;
        }
        imageList[i].sprite = sprite;

    }

    public void ButtonEvent()
    {
        for (int i = 0; i < 3; i++)
        {
            switch (panelList[i].activeSelf)
            {
                case true:
                    imageList[i].sprite = sprite;

                break;
                case false:
                    imageList[i].sprite = mySprite;
                break;
            }
        }
    }



}
