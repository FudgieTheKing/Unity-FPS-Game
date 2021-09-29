using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouselookie : MonoBehaviour
{
    public float sensitivety = 100f;
    public Transform playerBod;
    public float xrotate = 0f;
    public float xlimit = 90f;
    //distance from the camera the item is carried
    public float dist = 2.5f;
    public float throwforce = 15f;

    //the object being held
    private GameObject curObject;
    private Rigidbody curBody;

    //the rotation of the curObject at pickup relative to the camera
    private Quaternion relRot;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float Mx = Input.GetAxis("Mouse X") * sensitivety *Time.deltaTime;
        float My = Input.GetAxis("Mouse Y") * sensitivety * Time.deltaTime;
        xrotate -= My;
        xrotate = Mathf.Clamp(xrotate, -90f, xlimit);
        playerBod.Rotate(Vector3.up * Mx);
        transform.localRotation = Quaternion.Euler(xrotate, 0f, 0f);

        //on mouse click, either pickup or drop an item
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (curObject == null)
            {
                
                PickupItem();
            }
            else
            {
                
                DropItem();
            }
        }


        if (Input.GetMouseButtonDown(0))
        {
            if (curObject != null)
            {
                xlimit = 90f;
                curBody.useGravity = true;
                curBody.AddForce(transform.forward * throwforce, ForceMode.Impulse);
                curBody = null;
                curObject = null;
            }
        }
    }

    // Use this for initialization

    void FixedUpdate()
    {
        if (curObject != null)
        {
            //keep the object in front of the camera
            ReposObject();
        }
    }

    //calculates the new rotation and position of the curObject
    void ReposObject()
    {
        //calculate the target position and rotation of the curbody
        Vector3 targetPos = transform.position + transform.forward * dist;
        Quaternion targetRot = transform.rotation * relRot;

        //interpolate to the target position using velocity
        curBody.velocity = (targetPos - curBody.position) * 10;

        //keep the relative rotation the same
        curBody.rotation = targetRot;

        //no spinning around
        curBody.angularVelocity = Vector3.zero;
    }

    //attempts to pick up an item straigth ahead
    void PickupItem()
    {
        
        //raycast to find an item  Physics.SphereCast()
        RaycastHit hitInfo;
        Physics.Raycast(transform.position, transform.forward, out hitInfo, 5f);

        if (hitInfo.rigidbody == null)
            return;
        xlimit = 35f;

        curBody = hitInfo.rigidbody;
        curBody.useGravity = false;


        curObject = hitInfo.rigidbody.gameObject;


        //hack w/ parenting & unparenting to get the relative rotation
        curObject.transform.parent = transform;
        relRot = curObject.transform.localRotation;
        curObject.transform.parent = null;



    }

    //drops the current item
    void DropItem()
    {
        xlimit = 90f;
        curBody.useGravity = true;
        curBody = null;
        curObject = null;
    }

}
