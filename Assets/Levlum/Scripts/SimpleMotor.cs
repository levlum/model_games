using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Levlum {
    public class SimpleMotor : MonoBehaviour
    {
        private float x = 0f;
        private float y = 0f;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            float t = Time.realtimeSinceStartup;
            x = Mathf.Sin(t);
            y = Mathf.Cos(t);
            GetComponent<Rigidbody>().AddForce(new Vector3(x, Random.Range(0f,0.5f), y) * 10f);
        }
    }
}