using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box_controller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        //Check if the parent of the object is not any part of integrated boxes on platform, let its tag one_box_tag
        //however if the parent have another property, set the one boxes' tag to untagged to avoid confusion on ontrigger functions of hell_controller.cs
        if(transform.parent.transform.gameObject.tag != "Untagged")
        {
            transform.gameObject.tag = "Untagged";
            transform.gameObject.GetComponent<Collider>().enabled = false;
        }


        //Check if the parent of the objects parent is final platform, 

        if (transform.parent.transform.parent.gameObject.tag == "final_platform_tag") {

            transform.gameObject.tag = "Untagged";
        }


    }

    // Update is called once per framea
    void Update()
    {
        
    }
}
