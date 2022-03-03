using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse_Interaction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        
        var ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit) && hit.rigidbody){
            hit.rigidbody.AddForce(new Vector3(0, 2, 0), ForceMode.Impulse);
        }

    }
}
