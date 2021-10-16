using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : Menu<MainMenu>
{
    private List<Button> buttonList = new List<Button>();
    private List<GameObject> panelList = new List<GameObject>();

    [SerializeField]
    private GameObject btnGroup;
    [SerializeField]
    private GameObject pnGroup;
    [SerializeField]
    private Button exitBtn;
    [SerializeField]
    private RectTransform highTr;




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
    }


    public void MainButton(int i)
    {
        exitBtn.gameObject.SetActive(true);
        highTr.anchoredPosition = new Vector3(0,660f);
        for (int temp = 0; temp < 4; temp++)
        {
            buttonList[temp].interactable = true;
            panelList[temp].gameObject.SetActive(false);
        }
        buttonList[i].interactable = false;
        panelList[i].gameObject.SetActive(true);
    }

    public void ExitButton(int i)
    {
        highTr.anchoredPosition = new Vector3(0,451);
        panelList[i].gameObject.SetActive(false);
        buttonList[i].interactable = true;
        exitBtn.gameObject.SetActive(false);
    }



    public override void OnBackPressed()
    {
        Application.Quit();
    }

    public void Active(bool On)
    {

    }
}
