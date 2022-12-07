using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Craft
{
    public string craftName; // 이름
    public GameObject go_prefab; // 실제 설치 될 프리팹
    public GameObject go_PreviewPrefab; // 미리 보기 프리팹
}


public class CraftManual : MonoBehaviour
{
    private bool isActivated = false;  // CraftManual UI 활성 상태
    private bool isPreviewActivated = false; // 미리 보기 활성화 상태

    [SerializeField] private GameObject go_BaseUI; // 기본 베이스 UI

    [SerializeField] private Craft[] craft_fire;  // 🔥불 탭에 있는 슬롯들. 

    private GameObject go_Preview; // 미리 보기 프리팹을 담을 변수
    private GameObject go_Prefab; // 실제 생성될 프리팹을 담을 변수 

    [SerializeField] private Transform tf_Camera;  // 카메라 위치
    [SerializeField] private Transform tf_Player;  // 플레이어 위치

    private RaycastHit hitInfo;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float range;

    List<Vector3> linePoints;

    RaycastHit hit;
    [SerializeField] private float maxDistance = 0.2f;

    [SerializeField] private Material drawMaterial;
    [SerializeField] private Color drawColor;
    private GameObject newLine;
    private LineRenderer drawLine;
    [SerializeField] private float lineWidth;
    private Vector3 lastPosition;

    public void SlotClick(int _slotNumber)
    {
        if (!isPreviewActivated)
        {
            go_Preview = Instantiate(craft_fire[_slotNumber].go_PreviewPrefab, tf_Camera.position + tf_Camera.forward, Quaternion.identity);
            go_Prefab = craft_fire[_slotNumber].go_prefab;
            isPreviewActivated = true;
            go_BaseUI.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !isPreviewActivated)
            Window();

        if (isPreviewActivated)
            PreviewPositionUpdate();

        if (Input.GetButtonDown("Fire1"))
            Build();

        if (Input.GetKeyDown(KeyCode.Escape))
            Cancel();
    }

    private void PreviewPositionUpdate()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        Plane GroupPlane = new Plane(Vector3.up, Vector3.zero);

        float rayLength;

        if (GroupPlane.Raycast(cameraRay, out rayLength))

        {
            Vector3 pointTolook = cameraRay.GetPoint(rayLength);
            tf_Player.position.Set(pointTolook.x, pointTolook.y, pointTolook.z);
        }

/*        if (Physics.Raycast(tf_Camera.position, tf_Camera.forward, out hitInfo, range, layerMask))
        {
            if (hitInfo.transform != null)
            {
                Vector3 _location = hitInfo.point;
                Vector3 _locationPlayer = tf_Player.position;

                if (Input.GetKeyDown(KeyCode.Q))
                    go_Preview.transform.Rotate(0f, -90f, 0f);
                else if (Input.GetKeyDown(KeyCode.E))
                    go_Preview.transform.Rotate(0f, +90f, 0f);

                // 놓을 위치를 정수 단위로 조정
                //_location.Set(Mathf.Round(_location.x), Mathf.Round(_location.y / 0.1f) * 0.1f, Mathf.Round(_location.z / 0.1f));

                //_locationPlayer.Set(Mathf.Round(_locationPlayer.x), Mathf.Round(_locationPlayer.y / 0.1f) * 0.1f, Mathf.Round(_locationPlayer.z));

                                // 플레이어의 위치는 놓을 수 없다
*//*                                if (_location.x != _locationPlayer.x && _location.y != _locationPlayer.y)
                go_Preview.transform.position = _location;*//*
            }
        }*/
    }

    private void Build()
    {
        if (isPreviewActivated && go_Preview.GetComponent<PreviewObject>().isBuildable())
        {
            go_Prefab.GetComponent<Renderer>().material = ColorPicker.Instance.GetMaterial();
            Instantiate(go_Prefab, go_Preview.transform.position, go_Preview.transform.rotation);
            Destroy(go_Preview);
            isActivated = false;
            isPreviewActivated = false;
            go_Preview = null;
            go_Prefab = null;

        }
    }

    private void Window()
    {
        if (!isActivated)
            OpenWindow();
        else
            CloseWindow();
    }

    private void OpenWindow()
    {
        isActivated = true;
        go_BaseUI.SetActive(true);
    }

    private void CloseWindow()
    {
        isActivated = false;
        go_BaseUI.SetActive(false);
    }

    private void Cancel()
    {
        if (isPreviewActivated)
            Destroy(go_Preview);

        isActivated = false;
        isPreviewActivated = false;

        go_Preview = null;
        go_Prefab = null;

        go_BaseUI.SetActive(false);
    }
}
