using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hell_controller : MonoBehaviour
{

    private GameObject characterGameObject;



    public float time1 = 0;
    public float time2 = 0;

    private float timerLimit = 0.5f;
    Char_controller script;


    
    // Start is called before the first frame update
    void Start()
    {

        //characterGameObject = GameObject.FindGameObjectWithTag("main_character_tag");
        characterGameObject = GameObject.Find("Catwalk Walking");
        script = characterGameObject.GetComponent<Char_controller>();
       
    }

    // Update is called once per frame
    void Update()
    {

    }






    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.tag);



        if (other.gameObject.tag == "shoe_tag")
        {

            script.getShoes(other.gameObject);
        }




        if (other.gameObject.tag == "hells_tag")
        {
           // Debug.Log("Main char to hells collider ignored.");
            Physics.IgnoreCollision(other, gameObject.GetComponent<BoxCollider>());
        }

        //Final platform settings 
        try
        {
            if (other.gameObject.transform.parent.transform.parent.gameObject != null)
            {
                if(other.gameObject.tag != "end_level_trigger_tag")
                {
                    if (other.gameObject.transform.parent.transform.parent.gameObject.tag == "final_platform_tag")
                    {
                        other.gameObject.GetComponent<BoxCollider>().isTrigger = false;
                        script.gameObject.transform.parent.position += new Vector3(0, 1f, 0);

                        script.removeHells(1);
                        script.decreaseCharHeight_onExitTrigger(1);
                    }
                }
                else
                {
                    script.endGame();
                }

            }
          
        }catch(System.Exception e)
        {
            
        }







      


        if (other.gameObject.tag == "one_box_border_tag")
        {
          
            if ((Time.time - time1) > timerLimit)
            {
                Debug.Log("OnTriggerEnter Collision Timer : " + (Time.time - time1) + "timer limit " + timerLimit);
                script.removeHells(1);

            }
            time1 = Time.time;

        }

        if (other.gameObject.tag == "two_box_border_tag")
        {
            //Debug.Log("Collision Timer : " + (Time.time - time1));
            if (Time.time - time1 > timerLimit)
            {
               
                script.removeHells(2);
            }
            time1 = Time.time;

        }


        if (other.gameObject.tag == "three_box_border_tag")
        {
            //Debug.Log("Collision Timer : " + (Time.time - time1));
            if (Time.time - time1 > timerLimit)
            {
                 script.removeHells(3);
            }
            time1 = Time.time;
        }
        if (other.gameObject.tag == "four_box_border_tag")
        {
            //Debug.Log("Collision Timer : " + (Time.time - time1));
            if (Time.time - time1 > timerLimit)
            {
               
                script.removeHells(4);
            }
            time1 = Time.time;
        }
               
        





    }



    private void OnTriggerExit(Collider other)
    {


        /*
         
        Aynı anda birden çok boxa tıklayarak çıktığında bir birim yukarıda kalıyor.
         
         */





        if (other.gameObject.tag == "Untagged")
        {


        }

        else { 






        if (other.gameObject.tag == "one_box_border_tag")
        {
                
                if ((Time.time - time2) > timerLimit)
            {
                    Debug.Log("OnTriggerExit Collision Timer : " + (Time.time - time2));

                    script.decreaseCharHeight_onExitTrigger(1);
            }
                time2 = Time.time;

        }





        if (other.gameObject.tag == "two_box_border_tag")
        {
            if (Time.time - time2 > timerLimit)
            {
               
            script.decreaseCharHeight_onExitTrigger(2);

            }
                time2 = Time.time;
        }







        if (other.gameObject.tag == "three_box_border_tag")
        {
            if (Time.time - time2 > timerLimit)
            {
                
            script.decreaseCharHeight_onExitTrigger(3);
            }
                time2 = Time.time;
        }
        if (other.gameObject.tag == "four_box_border_tag")
        {
            if (Time.time - time2 > timerLimit)
            {
                
            script.decreaseCharHeight_onExitTrigger(4);
            }
                time2 = Time.time;
        }

        }

    }

}
