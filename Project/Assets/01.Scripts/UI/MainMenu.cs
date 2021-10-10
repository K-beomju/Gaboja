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
    private Button createBtn;



    protected override void Awake()
    {
        base.Awake();
        // 패널 , 버튼 그룹 리스트 추가 
        for (int i = 0; i < 4; i++)
        {
            int temp = i;
            Button button = btnGroup.transform.GetChild(i).GetComponent<Button>();
            button.onClick.AddListener(() => MainButton(temp));
            buttonList.Add(button);

            panelList.Add(pnGroup.transform.GetChild(i).gameObject);

            exitBtn.onClick.AddListener(() => ExitButton(temp));
        }
        // 제작 버튼 메서드 등록 
       // createBtn.onClick.AddListener(() => CreateButton());
    }

    private void Start()
    {
        // 시작할때 닫기 
        panelList.ForEach(x => x.gameObject.SetActive(false));
        exitBtn.gameObject.SetActive(false);
    }

    //패널 여는 버튼 
    public void MainButton(int i)
    {
        exitBtn.gameObject.SetActive(true);

        for (int temp = 0; temp < 4; temp++)
        {
            buttonList[temp].interactable = true;
            panelList[temp].gameObject.SetActive(false);
        }
        buttonList[i].interactable = false;
        panelList[i].gameObject.SetActive(true);
    }
    //패널 닫기 버튼 
    public void ExitButton(int i)
    {
        panelList[i].gameObject.SetActive(false);
        buttonList[i].interactable = true;
        exitBtn.gameObject.SetActive(false);
    }
   



    public override void OnBackPressed()
    {
        Application.Quit();  
    }

}
