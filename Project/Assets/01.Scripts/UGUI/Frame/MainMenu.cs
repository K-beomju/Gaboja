using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;



public class MainMenu : MonoBehaviour
{
    private List<Button> buttonList = new List<Button>();
    private List<GameObject> panelList = new List<GameObject>();

    [Header("Etc")]
    [SerializeField]
    private Transform backGround; // 3.8 2.7
    private Vector2 curPos;

    [Header("Mid Group")]
    [SerializeField]
    private GameObject pnGroup = null;
    [SerializeField]
    private Button exitBtn = null;
    [SerializeField]
    private GameObject emeraldObj = null;


    [Header("Low Group")]
    [SerializeField]
    private GameObject btnGroup = null;

    [Header("PanelGroup")]
    public PanelContent panelContent;



    private void Awake()
    {


        //        curPos = backGround.transform.position;
        for (int i = 0; i < 4; i++)
        {
            int temp = i;
            Button button = btnGroup.transform.GetChild(i).GetComponent<Button>();
            button.onClick.AddListener(() => MainButton(temp));


            buttonList.Add(button);

            panelList.Add(pnGroup.transform.GetChild(i).gameObject);

            exitBtn.onClick.AddListener(() => ExitButton(temp));
        }




    }

    private void Start()
    {

        panelList.ForEach(x => x.gameObject.SetActive(false));
        exitBtn.gameObject.SetActive(false);
        emeraldObj.gameObject.SetActive(false);

    }


    public void MainButton(int i)
    {
        Debug.Log("패널 그룹 On");
        if (NullCheck())
        {
            panelContent.ButtonEvent();
            GameObject a =  panelContent.panelList.Find(x => x.gameObject.activeSelf);
            panelContent.scrollRect.content = a.GetComponent<RectTransform>();

            emeraldObj.gameObject.SetActive(true);
            exitBtn.gameObject.SetActive(true);

            for (int temp = 0; temp < 4; temp++)
            {
                buttonList[temp].interactable = true;
                panelList[temp].gameObject.SetActive(false);
            }
            buttonList[i].interactable = false;
            panelList[i].gameObject.SetActive(true);
        }


    }

    public void ExitButton(int i)
    {
        if (NullCheck())
        {
            // backGround.transform.position = curPos;
            panelList[i].gameObject.SetActive(false);
            buttonList[i].interactable = true;
            exitBtn.gameObject.SetActive(false);
            emeraldObj.gameObject.SetActive(false);
        }

    }

    public bool NullCheck()
    {
        return buttonList != null && panelList != null;
    }




}
