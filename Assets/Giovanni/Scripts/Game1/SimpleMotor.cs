using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Giovanni { 
    public class SimpleMOtor : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(2, 0.2f, 1) * 10);
        }
    }
}