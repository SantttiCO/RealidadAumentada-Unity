using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshCollider))]
public class ClickDrag : MonoBehaviour
{
    private Vector3 screemPoint;
    private Vector3 offset;
    // Start is called before the first frame update

    private void OnMouseDown()
    {
        Vector3 pos = gameObject.transform.position;
        screemPoint = Camera.main.WorldToScreenPoint(pos);
        Debug.Log("Touched " + gameObject.name + " al " + screemPoint);

        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screemPoint.z);

        offset = pos - Camera.main.ScreenToWorldPoint(mousePos);


    }

    private void OnMouseDrag()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screemPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(mousePos) + offset;
        transform.position = curPosition;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}