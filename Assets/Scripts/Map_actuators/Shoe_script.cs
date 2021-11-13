using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoe_script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }


    public void DestroyShoe()
    {
        Destroy(transform.gameObject);
    }


}
