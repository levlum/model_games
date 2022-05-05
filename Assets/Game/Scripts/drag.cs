using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drag : MonoBehaviour
{
    private Vector3 mOffset;
    private float mZCoord;

    private Vector3 delta;

    void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseAsWorldPoint();

        // GameObject duplicate = Instantiate(rootObj);

        // Color newColor = new Color( Random.value, Random.value, Random.value, 1.0f );
        //      apply it on current object's material
        //      duplicate.material.color = newColor;
        // duplicate.material.SetColor("_Color", Color.red);
    }

    private Vector3 GetMouseAsWorldPoint()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag()
    {
        transform.position = GetMouseAsWorldPoint() + mOffset;
        delta = (GetMouseAsWorldPoint() + mOffset) - transform.position;

        GetComponent<Rigidbody>().AddForce(delta, ForceMode.Impulse);
    }
}
