using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataController : MonoBehaviour
{
    //public StatusController statusController { get; private set; }
    // --- 게임 데이터 파일이름 설정 "/" 추가 ---
    public string GameDataFileName = "/VPetData.json";

    // "원하는 이름(영문).json

    // ---싱글톤으로 선언--- 
    #region instance
    private static DataController instance = null;
    public static DataController Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<DataController>();

                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }
    #endregion

    private void Awake()
    {
        // instance가 아닌 Instance 임을 주의!!
        if (this != Instance) Destroy(gameObject);

        //statusController =
        //LoadGameData();
        //SaveGameData();
    }

/*    private void Start() 
    {
        //if (statusController.petData.food.value <= 0) statusController.Init();    // 디버깅용. 배부름 수치가 0이되면 다시 100으로 리셋
    }*/

    // 저장된 게임 불러오기
    public void LoadGameData() 
    {
        string filePath = Application.persistentDataPath + GameDataFileName;
        
        // 저장된 게임이 있다면
        if (File.Exists(filePath))
        { 
            print("불러오기 성공"); 
            string FromJsonData = File.ReadAllText(filePath); 
        }

        // 저장된 게임이 없다면
        else 
        { 
            print("새로운 파일 생성");
        }
    } 
    
    // 게임 저장하기
    public void SaveData() 
    {
       // string ToJsonData = JsonUtility.ToJson(PetDataToPetJsonData()); 
        string filePath = Application.persistentDataPath + GameDataFileName;

        // 이미 저장된 파일이 있다면 덮어쓰기
       // File.WriteAllText(filePath, ToJsonData);
        
        // 올바르게 저장됐는지 확인
        print("저장완료");
    }

    // 이름에 맞는 스프라이트 디렉토리 정보 저장
    public Dictionary<string, T> SetDictionary<T>(string address) where T : Object
    {
        T[] objects = Resources.LoadAll<T>(address);
        Dictionary<string, T> dictionary = new Dictionary<string, T>();

        for (int i = 0; i < objects.Length; i++)
        {
            dictionary[objects[i].name] = objects[i];
        }
        return dictionary;
    }

    // 종료하면 자동저장되도록
    private void OnApplicationQuit()
    {
        SaveData(); 
    } 
} 