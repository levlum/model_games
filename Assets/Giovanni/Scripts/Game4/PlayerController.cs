using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float torque = 10f;
    public float scaleIncrease = 1.0f;
    public float breakJoint = 1000f;
    public CameraFollow cameraFollow;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddTorque(new Vector3(moveVertical, 0.0f, -moveHorizontal) * torque);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Collectible") && collision.gameObject.transform.parent != transform)
        {
            // Attach the collectible to the player using a FixedJoint
            FixedJoint joint = collision.gameObject.AddComponent<FixedJoint>();
            joint.connectedBody = rb;

            // Set the break force and torque from the public variable
            joint.breakForce = breakJoint;
            joint.breakTorque = breakJoint;

            // Set the parent of the collectible to be the player
            collision.gameObject.transform.SetParent(transform);

            // Increase the player size
            IncreasePlayerSize();

            // Notify the camera to adjust its position
            if (cameraFollow != null)
            {
                cameraFollow.AdjustCameraPosition();
            }
        }
    }

    void IncreasePlayerSize()
    {
        // Increase the scale of the player
        transform.localScale += new Vector3(scaleIncrease, scaleIncrease, scaleIncrease);

        // Adjust the position of all children (attached collectibles) to maintain visual consistency
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Collectible"))
            {
                child.localPosition *= (1 + scaleIncrease / transform.localScale.x);
            }
        }
    }
}
