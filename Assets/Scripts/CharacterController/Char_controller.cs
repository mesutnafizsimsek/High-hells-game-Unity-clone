using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;


public class Char_controller : MonoBehaviour
{


    public GameObject hell_prefab_to_instantiate;

    public GameObject left_point_to_instantiate;
    public GameObject right_point_to_instantiate;





    private List<GameObject> right_hell_list = new List<GameObject>();
    private List<GameObject> left_hell_list = new List<GameObject>();


    public GameObject platform_to_increase_height;



    GameObject camera_gameobject;


    Animator charAnimator;
    bool endGameActionsStart = false;



    [SerializeField]
    private int hellCount = 0;


    private float horizontal, vertical;
    Vector3 movement_vector;
    public int speed = 3;




    private Touch touch;
    private float speedModifier = 0.08f;


    void Start()
    {
        endGameActionsStart = false;
        charAnimator = gameObject.GetComponent<Animator>();
        camera_gameobject = GameObject.FindGameObjectWithTag("MainCamera");

    }



    void Update()
    {


        if (endGameActionsStart == false) {
            if (camera_gameobject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                horizontal = 0;

                if(Input.touchCount > 0)
                {
                   /* Debug.Log("Touch");
                    Debug.Log("touch.phase " + touch.phase);
                    Debug.Log("Input.touches[0]   " + Input.touches[0].deltaPosition);*/
               
                        touch = Input.touches[0];
                        //horizontal = Input.GetAxis("Horizontal");


                        horizontal = touch.deltaPosition.x  * speedModifier;

                        vertical = Input.GetAxis("Vertical");

                        
                        
                        //transform.parent.position = movement_vector;
                        
                    

                }




#if UNITY_EDITOR
                /* horizontal = Input.GetAxis("Horizontal");

                 vertical = Input.GetAxis("Vertical");

                */

#endif

                movement_vector = new Vector3(-1f, 0, horizontal);
                transform.parent.position += movement_vector * Time.deltaTime;


            }

        }


    }



    public void removeHells(int boxNumber)
    {
       // Debug.Log("hells count " + hellCount);
       // Debug.Log("Box number" + boxNumber);



        if (hellCount >= 1 && boxNumber <= hellCount)
        {
            

            for (int i = hellCount-1; i >= hellCount-boxNumber; i--) {

                /*
                Debug.Log("Removing " + boxNumber);
                Debug.Log("hellCount-1 - boxNumber " + (hellCount - 1 - boxNumber));
                Debug.Log("right_hell_list.count " + right_hell_list.Count);
                 Debug.Log("box number "+boxNumber);
                 Debug.Log("hellCount   " + hellCount);
                 Debug.Log("right_hell_list[i]   " + right_hell_list.Count);*/








                right_hell_list[i].gameObject.GetComponent<FixedJoint>().connectedBody = null;
                //Destroy();    

                Destroy(right_hell_list[i].gameObject.GetComponent<FixedJoint>());

                right_hell_list[i].transform.parent = null;
                right_hell_list[i].gameObject.GetComponent<BoxCollider>().isTrigger = false;
                right_hell_list[i].gameObject.GetComponent<Rigidbody>().useGravity = true;
                right_hell_list.Remove(right_hell_list[i]);

               
                left_hell_list[i].gameObject.GetComponent<FixedJoint>().connectedBody = null;

                Destroy(left_hell_list[i].gameObject.GetComponent<FixedJoint>());


                left_hell_list[i].transform.parent = null;
                left_hell_list[i].gameObject.GetComponent<BoxCollider>().isTrigger = false;
                left_hell_list[i].gameObject.GetComponent<Rigidbody>().useGravity = true;
                left_hell_list.Remove(left_hell_list[i]);

                

            }

            hellCount-=boxNumber;
        }

        else
        {
            
            dieScene();
            
        }
        /*Debug.Log("hells count " + hellCount);
        Debug.Log("Box number " + boxNumber);*/

    }


    public void decreaseCharHeight_onExitTrigger(int boxNumber)
    {
        for(int i = 0; i < boxNumber; i++)
        {
            if(transform.parent.position.y >= 1.5f && platform_to_increase_height.transform.localScale.y >= 1f)
            {
                transform.parent.position += new Vector3(0, -0.5f, 0);
                platform_to_increase_height.transform.localScale -= new Vector3(0, 1.3f, 0);
            }

        }
    }


    void dieScene()
    {

        endGameActionsStart = true;
        charAnimator.SetBool("isDied", true);
        platform_to_increase_height.GetComponentInChildren<BoxCollider>().enabled = false;

        gameObject.GetComponent<BoxCollider>().isTrigger = false;

        //var platformSize = platform_to_increase_height.GetComponentInChildren<BoxCollider>().size;

        //platform_to_increase_height.GetComponentInChildren<BoxCollider>().size = new Vector3(platformSize.x, 0.2f, platformSize.z);

        var size = gameObject.GetComponent<BoxCollider>().size;
        gameObject.GetComponent<BoxCollider>().size = new Vector3(size.x, 0.2f, size.z);


      

    }


    void addHells()
    {
        transform.parent.position += new Vector3(0, 1f, 0);
        //transform.position = transform.position + new Vector3(0, 0.5f, 0);


        platform_to_increase_height.transform.localScale += new Vector3(0, 1.3f, 0);

        if (hellCount == 0)
        {

            hellCount++;

            var left_hell = Instantiate(original: hell_prefab_to_instantiate, position: left_point_to_instantiate.gameObject.transform.position,
            rotation: left_point_to_instantiate.gameObject.transform.rotation, parent: left_point_to_instantiate.gameObject.transform);

            left_hell.GetComponent<FixedJoint>().connectedBody = left_point_to_instantiate.gameObject.GetComponent<Rigidbody>();
            left_hell_list.Add(left_hell);


            var right_hell = Instantiate(original: hell_prefab_to_instantiate, position: right_point_to_instantiate.gameObject.transform.position,
             rotation: right_point_to_instantiate.gameObject.transform.rotation, parent: right_point_to_instantiate.gameObject.transform);


            right_hell.GetComponent<FixedJoint>().connectedBody = right_point_to_instantiate.gameObject.GetComponent<Rigidbody>();
            right_hell_list.Add(right_hell);





        }
        else {
            hellCount++;

            var right_others = Instantiate(original: hell_prefab_to_instantiate, position: right_hell_list[right_hell_list.Count - 1].gameObject.transform.GetChild(1).gameObject.transform.position,
            rotation: right_hell_list[right_hell_list.Count - 1].gameObject.transform.GetChild(1).gameObject.transform.rotation, parent: right_point_to_instantiate.gameObject.transform);
            right_others.GetComponent<FixedJoint>().connectedBody = right_hell_list[right_hell_list.Count - 1].gameObject.GetComponent<Rigidbody>();
            right_hell_list.Add(right_others);


            var left_others = Instantiate(original: hell_prefab_to_instantiate, position: left_hell_list[left_hell_list.Count - 1].gameObject.transform.GetChild(1).gameObject.transform.position,
            rotation: left_hell_list[left_hell_list.Count - 1].gameObject.transform.GetChild(1).gameObject.transform.rotation, parent: left_point_to_instantiate.gameObject.transform);

            left_others.GetComponent<FixedJoint>().connectedBody = left_hell_list[left_hell_list.Count - 1].gameObject.GetComponent<Rigidbody>();
            left_hell_list.Add(left_others);



        }

    }


    public void getShoes(GameObject shoeGameObject)

    {
        Destroy(shoeGameObject);
        addHells();
    }


    public void endGame()
    {
        endGameActionsStart = true;
        charAnimator.SetBool("isLevelFinished", true);
    }


}
