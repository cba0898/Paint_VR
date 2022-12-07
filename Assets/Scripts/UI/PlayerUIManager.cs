using TMPro;
using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] private GameObject paletteObject;

    // ---싱글톤으로 선언--- 
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
        // instance가 아닌 Instance 임을 주의!!
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
        // 오큘러스 퀘스트2 컨트롤러 B 버튼 클릭 시
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
