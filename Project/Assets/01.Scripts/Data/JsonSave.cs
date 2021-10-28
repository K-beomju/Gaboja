using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;




public class JsonSave : MonoBehaviour
{
    const string saveFileName = "jsonFile.sav"; // 절대 바뀌지 않을 이름 const or readOnly


    [SerializeField]
    public DataClass data;

    string getFilePath(string fileName)
    {
        return Application.persistentDataPath + "/" + fileName;
    }

    void Start()
    {
        Load();
    }

    void Update()
    {
        //Save
        if (Input.GetKeyDown(KeyCode.S))
        {
            print("Save to : " + getFilePath(saveFileName));

            string jsonString = JsonUtility.ToJson(data); // jsonString에 통으로 저장됌

            StreamWriter sw = new StreamWriter(getFilePath(saveFileName));

            sw.WriteLine(jsonString);
            sw.Close();
        }
    }

    public void Load()
    {
         print("Load to : " + getFilePath(saveFileName));

            string fileStr = getFilePath(saveFileName);
            if (File.Exists(fileStr)) // 현재 이 경로에 세이브 파일이 존재하냐
            {
                StreamReader sr = new StreamReader(fileStr);
                string jsonString = sr.ReadToEnd();
                sr.Close();

                JsonUtility.FromJsonOverwrite(jsonString,data); // 덮어쓰기

                print(jsonString);
            }
            else
            {
                print("no File");
            }
    }

}

