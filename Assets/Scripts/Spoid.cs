using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spoid : MonoBehaviour
{
    [SerializeField] private GameObject colorObj;
    [SerializeField] private GameObject rightController;
    [SerializeField] private Transform minBound;

    private bool fixX;
    private bool fixY;
    Vector3 mpos;
    Ray ray;
    RaycastHit hit;
    [SerializeField] private Transform thumb;
    private bool dragging;

    private void Start()
    {
        dragging = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
        {
            dragging = false;
            ray = new Ray(rightController.transform.position, rightController.transform.forward);
            if(GetComponent<Collider>().Raycast(ray,out hit, 100))
            {
                dragging = true;
            }
        }
        else if (dragging) 
        {
            ray = new Ray(rightController.transform.position, rightController.transform.forward);
            if (GetComponent<Collider>().Raycast(ray, out hit, 100))
            {
                var point = hit.point;
                SetThumbPosition(point);
                SendMessage("OnDrag", Vector3.one - (thumb.localPosition - minBound.localPosition) / GetComponent<BoxCollider>().bounds.size.x);
            }
        }

    }

    public void SetDragPoint(Vector3 point)
    {
        point = (Vector3.one - point) * GetComponent<Collider>().bounds.size.x + GetComponent<Collider>().bounds.min;
        SetThumbPosition(point);
    }

    public void SetThumbPosition(Vector3 point)
    {
        Vector3 temp = thumb.localPosition;
        thumb.position = point;
        thumb.localPosition = new Vector3(fixX ? temp.x : point.x, fixY ? temp.y : point.y, thumb.localPosition.z - 1);
    }
/*    IEnumerator ScreenShotAndSpoid()
    {
        Texture2D tex = new Texture2D(Screen.width, )
    }*/
}
