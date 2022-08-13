using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSelector : MonoBehaviour
{
    public GameObject SelectedObject;

    public Vector3 offset;
    public Vector3 screenPoint;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(mouseRay, out hitInfo))
            {
                SelectItem item = hitInfo.collider.gameObject.GetComponent<SelectItem>();
                if (item != null)
                {
                    SelectedObject = hitInfo.collider.gameObject;
                    screenPoint = Camera.main.WorldToScreenPoint(SelectedObject.transform.position);
                    offset = SelectedObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
                }

            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            SelectedObject = null;
        }

        if (Input.GetMouseButton(0) && SelectedObject != null)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            curPosition.y = 5f;
            SelectedObject.transform.position = curPosition;
        }
    }
}
