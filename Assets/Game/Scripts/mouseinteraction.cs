using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseinteraction : MonoBehaviour
{
    public Transform all_cubes;
    public Rigidbody sphere;
    public bool alive = true;
    public Color deadColor;
    public Color livingColor;

    // Start is called before the first frame update
    void Start()
    {
        //bool huhu = true;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        var ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit) && hit.rigidbody != null) {
            if (hit.rigidbody.GetComponent<drag>() == null){

                if (alive) hit.rigidbody.AddForce(new Vector3(0, 2, 0), ForceMode.Impulse);
                if (Input.GetMouseButtonDown(0)){
                    /*
                    alive = !alive;
                    

                    foreach (Transform cube in all_cubes){
                        cube.GetComponent<SpringJoint>().spring = alive ? 10 : 0;
                        cube.GetComponent<Renderer>().material.color = alive ? livingColor : deadColor;
                    }
                    */

                }
            }
        }


    }
}
