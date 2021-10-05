using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class MenuManager : MonoBehaviour
{
    private static MenuManager _instance;
    public static MenuManager Instance { get { return _instance; } }

    // �޴����� ����� �θ� Ʈ������
    [SerializeField]
    private Transform _menuParent;


    // �޴� �Ŵ������� ������ ���� �޴� Ŭ������
    public MainMenu mainMenuPrefab;
   // public CreditMenu creditMenuPrefab;
   // public OptionMenu optionMenuPrefab;

    // �������� ĵ���� �޴����� ����
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

        // ���÷����� ����Ͽ� �Լ�Ÿ���� ���ͼ� ���ս�Ŵ
        System.Type myType = this.GetType();
        BindingFlags myflags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly;
        FieldInfo[] fields = myType.GetFields(myflags);

       

        //�����
        for (int i = 0; i < fields.Length; i++)
        {
            print("�ʵ��Լ��� : " + fields[i]);
        }

       // Menu[] menuPrefabs = { mainMenuPrefab, creditMenuPrefab, optionMenuPrefab };
        foreach(FieldInfo field in fields)
        {
            Menu prefab = field.GetValue(this) as Menu;
            if(prefab != null)
            {
                Menu menuInstance = Instantiate(prefab, _menuParent);

                // ó���� �����ϴ� �޴��� ���θ޴��� �ϰڴ�
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
            Debug.Log("�޴� ���� ����");
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
            Debug.LogWarning("�޴� �ݱ� ����");
            return;
        }

        Menu topMenu = _menuStack.Pop();
        topMenu.gameObject.SetActive(false);

        if(_menuStack.Count > 0)
        {
            // �� ���� �޴��� ������ Ȱ��ȭ ������ (���Ŵ� ����) 
            Menu nextMenu = _menuStack.Peek();
            nextMenu.gameObject.SetActive(true);
        }
    }



}