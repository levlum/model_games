using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class repel : MonoBehaviour
{

    public Rigidbody alien;
    // Update is called once per frame
    void Update()
    {
        Vector3 delta = this.transform.position - alien.transform.position;
        Vector3 force = delta.normalized * 10/delta.magnitude;

        /*
        if (delta.magnitude > 40 && alien.velocity.magnitude > 0){
            alien.AddForce(delta.normalized, ForceMode.Force);
        }

        else{
            alien.AddForce(-force, ForceMode.Force);
        }
        */
        if(delta.magnitude > 10){
            alien.AddForce(-delta, ForceMode.Force);
        }
        
    }
}
