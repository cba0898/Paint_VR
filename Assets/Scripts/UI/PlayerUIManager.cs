using TMPro;
using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] private GameObject paletteObject;

    // ---�̱������� ����--- 
    #region instance
    private static PlayerUIManager instance = null;
    public static PlayerUIManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<PlayerUIManager>();

                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }

    private void Awake()
    {
        // instance�� �ƴ� Instance ���� ����!!
        if (this != Instance) Destroy(gameObject);
    }
    #endregion

    //private Timer UITimer;
    private float defaultTime;
    private void Start()
    {
        defaultTime = 3.0f;
        //UITimer.SetTimer(defaultTime);
        //UITimer.StartTimer();
        paletteObject.SetActive(false);
    }

    private void Update()
    {
        // ��ŧ���� ����Ʈ2 ��Ʈ�ѷ� B ��ư Ŭ�� ��
        if (OVRInput.GetDown(OVRInput.Button.Two) || Input.GetKey(KeyCode.K))
        {
            ToggleObject(paletteObject);
        }
    }

    private void ToggleObject(GameObject gameObject)
    {
        gameObject.SetActive(gameObject.activeSelf ? true : false);
    }
}
