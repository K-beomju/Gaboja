using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UIType
{
    Upgrade,
    Item,
    Relic
}

public class MainMenu : Menu<MainMenu>
{
    private List<Button> buttonList = new List<Button>();
    private List<GameObject> panelList = new List<GameObject>();

    [Header("Etc")]
    [SerializeField]
    private RectTransform backGround;

    [Header("Mid Group")]
    [SerializeField]
    private GameObject pnGroup = null;
    [SerializeField]
    private Button exitBtn = null;
    [SerializeField]
    private Image emeraldImage = null;


    [Header("Low Group")]
    [SerializeField]
    private GameObject btnGroup = null;
    [SerializeField]
    private Button createBtn;



    [Header("Merge")]
    [SerializeField]
    private Merge merge = null;


    [Header("Observer")]
    //Shop은 데이터가 고정. 따라서 제외
    public Observer upgradeUI;
    public Observer itemUI;
    public Observer RelicUI;

    Subject subject = new Subject();


    protected override void Awake()
    {
        base.Awake();
        for (int i = 0; i < 4; i++)
        {
            int temp = i;
            Button button = btnGroup.transform.GetChild(i).GetComponent<Button>();
           // button.onClick.AddListener(() => MainButton(temp));
            button.onClick.AddListener(() => OnClickOpen(temp));

            buttonList.Add(button);

            panelList.Add(pnGroup.transform.GetChild(i).gameObject);

            exitBtn.onClick.AddListener(() => ExitButton(temp));
        }
        createBtn.onClick.AddListener(() => merge.ItemCreate(0));



    }

    private void Start()
    {

        panelList.ForEach(x => x.gameObject.SetActive(false));
        exitBtn.gameObject.SetActive(false);
        emeraldImage.gameObject.SetActive(false);

    }


    public void MainButton(int i)
    {
        emeraldImage.gameObject.SetActive(true);
        exitBtn.gameObject.SetActive(true);

        backGround.anchoredPosition = new Vector3(0,660f);
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

        backGround.anchoredPosition = new Vector3(0,451);
        panelList[i].gameObject.SetActive(false);
        buttonList[i].interactable = true;
        exitBtn.gameObject.SetActive(false);
        emeraldImage.gameObject.SetActive(false);

    }


    public void OnClickOpen(int type)
    {
        switch ((UIType)type)
        {
            case UIType.Upgrade:
                // 강화창
                subject.Message(Event.OnUpgrade);
                upgradeUI.gameObject.SetActive(true);
                subject.AddObserver(upgradeUI);
                break;
            case UIType.Item:
                // 아이템창
                itemUI.gameObject.SetActive(true);
                subject.AddObserver(itemUI); break;
            case UIType.Relic:
                // 유물창
                RelicUI.gameObject.SetActive(true);
                subject.AddObserver(RelicUI); break;
        }
    }


    public void OnClickClose(int type)
    {
        switch ((UIType)type)
        {
            case UIType.Upgrade:
                upgradeUI.gameObject.SetActive(false); subject.RemoveObserver(upgradeUI);
                break;
            case UIType.Item:
             itemUI.gameObject.SetActive(false); subject.RemoveObserver(itemUI);
              break;
            case UIType.Relic:
             RelicUI.gameObject.SetActive(false); subject.RemoveObserver(RelicUI);
              break;
        }
    }


}
