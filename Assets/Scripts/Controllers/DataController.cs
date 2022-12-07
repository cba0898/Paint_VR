using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataController : MonoBehaviour
{
    //public StatusController statusController { get; private set; }
    // --- ���� ������ �����̸� ���� "/" �߰� ---
    public string GameDataFileName = "/VPetData.json";

    // "���ϴ� �̸�(����).json

    // ---�̱������� ����--- 
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
        // instance�� �ƴ� Instance ���� ����!!
        if (this != Instance) Destroy(gameObject);

        //statusController =
        //LoadGameData();
        //SaveGameData();
    }

/*    private void Start() 
    {
        //if (statusController.petData.food.value <= 0) statusController.Init();    // ������. ��θ� ��ġ�� 0�̵Ǹ� �ٽ� 100���� ����
    }*/

    // ����� ���� �ҷ�����
    public void LoadGameData() 
    {
        string filePath = Application.persistentDataPath + GameDataFileName;
        
        // ����� ������ �ִٸ�
        if (File.Exists(filePath))
        { 
            print("�ҷ����� ����"); 
            string FromJsonData = File.ReadAllText(filePath); 
        }

        // ����� ������ ���ٸ�
        else 
        { 
            print("���ο� ���� ����");
        }
    } 
    
    // ���� �����ϱ�
    public void SaveData() 
    {
       // string ToJsonData = JsonUtility.ToJson(PetDataToPetJsonData()); 
        string filePath = Application.persistentDataPath + GameDataFileName;

        // �̹� ����� ������ �ִٸ� �����
       // File.WriteAllText(filePath, ToJsonData);
        
        // �ùٸ��� ����ƴ��� Ȯ��
        print("����Ϸ�");
    }

    // �̸��� �´� ��������Ʈ ���丮 ���� ����
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

    // �����ϸ� �ڵ�����ǵ���
    private void OnApplicationQuit()
    {
        SaveData(); 
    } 
} 