using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spawner : MonoBehaviour
{
    public GameObject spawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetMouseButtonDown(0)) {
             RaycastHit hit;
             Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
             if (Physics.Raycast(ray, out hit)){
                 if (hit.transform != null) {
                     Debug.Log("Hit " + hit.transform.gameObject.tag);
                     if (hit.transform.gameObject.tag == "Extra" && hit.transform.gameObject == this.gameObject){
                         Instantiate(spawn, this.gameObject.transform.position, this.gameObject.transform.rotation);
                         Debug.DrawRay(transform.position, transform.forward, Color.green);
                     }
                 }
    }
    }
    }
}
