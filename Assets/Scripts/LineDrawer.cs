using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    List<Vector3> linePoints;

    RaycastHit hit;
    [SerializeField] private float maxDistance = 0.2f;
    [SerializeField] private LayerMask layerMask;

    [SerializeField] private Material drawMaterial;
    [SerializeField] private Color drawColor;
    private GameObject newLine;
    private LineRenderer drawLine;
    [SerializeField] private float lineWidth;
    private Vector3 lastPosition;

    // Start is called before the first frame update
    void Start()
    {
        linePoints = new List<Vector3>();
        drawColor = ColorPicker.Instance.selectedColor;
        lastPosition = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // 처음 버튼 클릭 시 라인 랜더러 오브젝트 생성
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance, layerMask))
            {
                if (lastPosition != hit.point)
                {
                    SetLineRenderer();
                    AddPoint();
                }
            }
        }
        // 드래그하는 동안 계속해서 라인랜더러에 좌표 추가
        if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance, layerMask))
            {
                if (lastPosition != hit.point)
                {
                    AddPoint();
                    DrawLine();
                }
            }
        }
        // 버튼에서 손을 떼면 해당 객체 분리(배열 초기화)
        else if (linePoints.Count > 1)
        {
            SetLineRenderer();
            DrawLine();

            linePoints.Clear();
        }
    }
    private void AddPoint()
    {
        linePoints.Add(hit.point + new Vector3(0, 0, -0.01f));
        lastPosition = hit.point;
    }

    private void DrawLine()
    {
        drawLine.positionCount = linePoints.Count;
        drawLine.SetPositions(linePoints.ToArray());
    }

    private void SetLineRenderer()
    {
        newLine = new GameObject();
        drawLine = newLine.AddComponent<LineRenderer>();
        drawLine.material = drawMaterial;//new Material(Shader.Find("Sprites/Default"));
        drawColor = ColorPicker.Instance.selectedColor;
        drawLine.startColor = drawColor;
        drawLine.endColor = drawColor;
        drawLine.startWidth = lineWidth;
        drawLine.endWidth = lineWidth;
        newLine.tag = "line";
        newLine.AddComponent<MeshCollider>();
    }

    public void OnChangeThickness(float value)
    {
        lineWidth = Mathf.Max(0.001f, value * 0.1f);
    }
}
