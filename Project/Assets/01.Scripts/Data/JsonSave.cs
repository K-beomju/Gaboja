using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;




public class JsonSave : MonoBehaviour
{
    private static JsonSave _instance;
    public static JsonSave instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(JsonSave)) as JsonSave;
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.hideFlags = HideFlags.HideAndDontSave;
                    _instance = obj.AddComponent<JsonSave>();
                }
            }

            return _instance;
        }
    }

    private const string saveFileName = "jsonFile.sav"; // 절대 바뀌지 않을 이름 const or readOnly

    private DataClass dataClass;

    public void generate()
    {
        Debug.LogWarning("-------------------");
        Load();
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {

            Destroy(gameObject);
        }

        dataClass = new DataClass();


    }


    public EnemyDataInfo GetEnemyClass()
    {
        return dataClass._enemyData;
    }

    public DataClass GetDataClass()
    {
        return dataClass;
    }

    public PlayerDataInfo GetPlayerClass()
    {
        return dataClass._playerData;
    }

    public Upgrade GetUpgradeClass()
    {
        return dataClass._upgrades;
    }






    public void Save()
    {

        print("Save to : " + getFilePath(saveFileName));

        string jsonString = JsonUtility.ToJson(dataClass); // jsonString에 통으로 저장됌

        StreamWriter sw = new StreamWriter(getFilePath(saveFileName));

        sw.WriteLine(jsonString);
        sw.Close();

    }

    public void Load()
    {
        //dataClass = new DataClass();

        print("Load to : " + getFilePath(saveFileName));

        string fileStr = getFilePath(saveFileName);
        if (File.Exists(fileStr)) // 현재 이 경로에 세이브 파일이 존재하냐
        {
            StreamReader sr = new StreamReader(fileStr);
            string jsonString = sr.ReadToEnd();
            sr.Close();

            JsonUtility.FromJsonOverwrite(jsonString, dataClass); // 덮어쓰기

            print(jsonString);

        }
        else
        {
            print("no File");
        }
    }




    string getFilePath(string fileName)
    {
        return Application.persistentDataPath + "/" + fileName;
    }




}

