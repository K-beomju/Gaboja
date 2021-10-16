using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class MenuManager : MonoBehaviour
{
    private static MenuManager _instance;
    public static MenuManager Instance { get { return _instance; } }

    // 메뉴들을 담아줄 부모 트랜스폼
    [SerializeField]
    private Transform _menuParent;


    // 메뉴 매니져에서 관리할 개별 메뉴 클래스들
    public MainMenu mainMenuPrefab;
   // public OptionMenu optionMenuPrefab;

    // 스택으로 캔버스 메뉴들을 관리
    private Stack<Menu> _menuStack = new Stack<Menu>();



    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            Init();
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        if (!_instance == this)
        {
            _instance = null;
        }
    }

    private void Init()
    {
        if (_menuParent == null)
        {
            GameObject menuParnet = new GameObject("Menus");
            _menuParent = menuParnet.transform;
        }
        DontDestroyOnLoad(_menuParent.gameObject);

        // 리플렉션을 사용하여 함수타입을 얻어와서 통합시킴
        System.Type myType = this.GetType();
        BindingFlags myflags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly;
        FieldInfo[] fields = myType.GetFields(myflags);

       

        //디버깅
        for (int i = 0; i < fields.Length; i++)
        {
            print("필드함수들 : " + fields[i]);
        }

    
        foreach(FieldInfo field in fields)
        {
            Menu prefab = field.GetValue(this) as Menu;
           
            if(prefab != null)
            {
                Menu menuInstance = Instantiate(prefab, _menuParent);

                // 처음에 오픈하는 메뉴는 메인메뉴로 하겠다
                if (prefab != mainMenuPrefab)
                {
                    menuInstance.gameObject.SetActive(false);
                }
                else
                {
                    OpenMenu(menuInstance);
                }
            }
        }
    }


    public void OpenMenu(Menu menuInstance)
    {
        if (menuInstance == null)
        {
            Debug.Log("메뉴 오픈 에러");
            return;
        }

        if (_menuStack.Count > 0)
        {
            foreach (Menu menu in _menuStack)
            {
                menu.gameObject.SetActive(false);
            }
        }

        menuInstance.gameObject.SetActive(true);
        _menuStack.Push(menuInstance);
    }

    public void CloseMenu()
    {
        if(_menuStack.Count == 0)
        {
            Debug.LogWarning("메뉴 닫기 에러");
            return;
        }

        Menu topMenu = _menuStack.Pop();
        topMenu.gameObject.SetActive(false);

        if(_menuStack.Count > 0)
        {
            // 그 다음 메뉴를 꺼내서 활성화 시켜줌 (제거는 안함) 
            Menu nextMenu = _menuStack.Peek();
            nextMenu.gameObject.SetActive(true);
        }
    }



}
