using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomMovement : MonoBehaviour
{
    int min = 0;
    int max = 6;
    Vector3 myVector;
    
    // Update is called once per frame
    void Update()
    {
         myVector = new Vector3(UnityEngine.Random.Range(min, max), UnityEngine.Random.Range(min, 20), UnityEngine.Random.Range(min, max));
         if(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<mouseinteraction>().alive){
             this.gameObject.GetComponent<Rigidbody>().AddForce(myVector, ForceMode.Force);
         }
        
    }
}
