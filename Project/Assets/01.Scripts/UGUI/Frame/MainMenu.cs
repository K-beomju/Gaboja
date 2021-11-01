using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;



public class MainMenu : Singleton<MainMenu>
{
    private List<Button> buttonList = new List<Button>();
    private List<GameObject> panelList = new List<GameObject>();


    [Header("Mid Group")]
    [SerializeField] private GameObject pnGroup;
    [SerializeField] private Button exitBtn;
    [SerializeField] private GameObject emeraldObj;



    [Header("Low Group")]
    [SerializeField] private GameObject btnGroup;



    [Header("PanelGroup")]
    public PanelContent panelContent;





    protected override void Awake()
    {

        base.Awake();

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


    // Low UI - ButtonGroup

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
