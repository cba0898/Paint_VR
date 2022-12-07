using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eraser : MonoBehaviour
{
    List<Vector3> linePoints;
    float timer;
    public float timerDelay;

    RaycastHit hit;
    [SerializeField] private float maxDistance = 0.2f;
/*    [SerializeField] private LayerMask layerMask;

    [SerializeField] private Material drawMaterial;*/
/*    [SerializeField] private Color drawColor;
    private GameObject newLine;
    private LineRenderer drawLine;*/
   // public float lineWidth;

    // Start is called before the first frame update
    void Start()
    {
        //linePoints = new List<Vector3>();
        timer = timerDelay;
        //drawColor = ColorPicker.Instance.selectedColor;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && Input.GetMouseButtonDown(0))
        {
            timer = timerDelay;
            if (Physics.Raycast(transform.position, transform.up, out hit, maxDistance))
            {
                Debug.Log(hit.collider.name);
                if (hit.collider.tag == "line")
                {
                    hit.collider.gameObject.SetActive(false);
                }
                //linePoints.Add(hit.point);
                Debug.DrawRay(transform.position, transform.up * maxDistance, Color.red);
            }
        }
    }

/*    private void OnCollisionEnter(Collision collision)
    {
        
    }*/
}
