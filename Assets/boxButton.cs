using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxButton : MonoBehaviour
{
    public Transform button;
    Vector3 OGpos;
    bool isPressed;



    // Start is called before the first frame update
    void Start()
    {
        OGpos = button.position;
    }



    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "prop"  && !isPressed)
        {
            print("lol");
            isPressed = true;
            Vector3 changepos = button.position;
            changepos.y -= .2f;
            button.position = changepos;
            
        }

    }

    void OnTriggerExit(Collider other)
    {
        button.position = OGpos;
        isPressed = false;

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
