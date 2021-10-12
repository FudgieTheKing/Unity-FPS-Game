using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonRaycast : MonoBehaviour
{

    public Camera FPScam;
    public float range = 5f;
    public Transform button;
    public float buttonTime = 2.5f;
    Vector3 OGpos;
    bool isPressed;
    // Start is called before the first frame update
    void Start()
    {
        OGpos = button.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPressed)
        {
            StartCoroutine(ExecuteAfterTime(buttonTime));
        }
        if (Input.GetKeyDown(KeyCode.E) && !isPressed)
        {
            rays();
        }
    }


    void rays()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPScam.transform.position, FPScam.transform.forward, out hit, range) && hit.transform.tag == "button")
        {
           print("lol");
           isPressed = true;
           Vector3 changepos = button.position;
           changepos.y -= .2f;
           button.position = changepos;

        }
    }


    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        ting();


    }


    void ting()
    {
        isPressed = false;
        button.position = OGpos;
    }
}
