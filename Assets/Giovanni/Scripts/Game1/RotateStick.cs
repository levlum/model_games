using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Giovanni { 
    public class RotateStick : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

            private bool isRotating = false;

            void Update()
            {
                // Check for left mouse button being held down
                if (Input.GetMouseButtonDown(0))
                {
                    isRotating = true;
                    StartCoroutine(RotateObjectCoroutine(-0.1f));
                }

                // Check for right mouse button being held down
                if (Input.GetMouseButtonDown(1))
                {
                    isRotating = true;
                    StartCoroutine(RotateObjectCoroutine(0.1f));
                }

                // Stop rotating when mouse button is released
                if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
                {
                    isRotating = false;
                }
            }

            IEnumerator RotateObjectCoroutine(float angle)
            {
                while (isRotating)
                {
                    // Rotate the object by the specified angle around the Y-axis
                    transform.Rotate(Vector3.up, angle);

                    // Wait for the next frame
                    yield return null;
                }
            }
        }
    }
