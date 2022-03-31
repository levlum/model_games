using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisions : MonoBehaviour
{
    Transform all_cubes;
    public bool alive;
    Color deadColor;
    Color livingColor;

    mouseinteraction script;
    void Start()
    {
        all_cubes = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<mouseinteraction>().all_cubes;
        alive = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<mouseinteraction>().alive;
        deadColor = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<mouseinteraction>().deadColor;
        livingColor = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<mouseinteraction>().livingColor;
    }

    void OnCollisionEnter(Collision collision){
        
        if (collision.gameObject.tag != "Floor"){

            if (this.tag == "Happy"){
                alive = true;
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<mouseinteraction>().alive = true;

                foreach (Transform cube in all_cubes){
                cube.GetComponent<SpringJoint>().spring = 10;
                cube.GetComponent<Renderer>().material.color = Color.green;
                StartCoroutine(Happy(cube));
                }
                
            }

            if (this.tag == "Danger"){
                
                alive = false;
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<mouseinteraction>().alive = false;

                foreach (Transform cube in all_cubes){
                cube.GetComponent<SpringJoint>().spring = 0;
                cube.GetComponent<Renderer>().material.color = alive ? deadColor : Color.red;
                StartCoroutine(Danger(cube));
                } 

            }

            if (this.tag == "Extra"){

            }

            this.gameObject.GetComponent<MeshRenderer>().enabled = false;

        }

    }

    IEnumerator Happy(Transform cube){
        yield return new WaitForSeconds(1);
        cube.GetComponent<Renderer>().material.color = livingColor;
        Destroy(this.gameObject);
        
    }

    IEnumerator Danger(Transform cube){
        yield return new WaitForSeconds(1);
        cube.GetComponent<Renderer>().material.color = deadColor;
        Destroy(this.gameObject);
    }

    IEnumerator Extra(){
        yield return new WaitForSeconds(1);
    }
}
