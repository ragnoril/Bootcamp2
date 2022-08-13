using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemTypes
{
    Apple,
    Fries,
    Loaf,
    Pineapple,
    Wine,
    Taco,
    IceCream,
    Cocktail
};

public class SelectItem : MonoBehaviour
{

    private Rigidbody rigidbody;
    private Vector3 screenPoint;
    private Vector3 offset;

    public float yPos;

    public ItemTypes ItemType;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    private void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;

    }

    private void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        curPosition.y = yPos;
        transform.position = curPosition;
    }

    */
}
