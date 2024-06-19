using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    void LateUpdate()
    {
        transform.position = player.position + offset;
    }

    public void AdjustCameraPosition()
    {
        Debug.Log("Adjusting Camera Position");
        offset += new Vector3(0, 1, -1);
    }

}
