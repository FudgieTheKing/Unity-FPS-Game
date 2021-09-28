using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playermove : MonoBehaviour
{
    public ParticleSystem landing;
    public const int doublejump = 1;
    public int currentJump = 0;
    Vector3 vel;
    public Transform player;
    public Animator anima;
    public Camera myCamera;
    public Transform Cam;
    public float grav = -9.81f;
    public CharacterController control;
    public Animator animaP;
    public Transform groundcheck;
    public Transform headcheck;
    public Transform cubez;
    public float grounddist = .4f;
    public LayerMask groundmask;

    public bool isGrounded;
    public bool isCrouching;
    public float jumpHeight = 3f;

    public CapsuleCollider colli;
    // Start is called before the first frame update
    public float speed = 8f;
    void Start()
    {
        
    }
    void FixedUpdate()
    {
        if (player.position.y <= -20)
        {
            Restart();
        }
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundcheck.position, grounddist, groundmask);
        isCrouching = Physics.Raycast(transform.position,Vector3.up,2f);
        if (isGrounded)
        {
            currentJump = 0;
        }

        if((isGrounded && vel.y < 0))
        {
            vel.y = -2;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;


        if (Input.GetKey(KeyCode.LeftControl) )
        {
                speed = 2f;
                animaP.SetBool("crouch", true);
   
                control.height = 1.3f;


        }
        else if (!Input.GetKey(KeyCode.LeftControl) && !isCrouching)
        {

            animaP.SetBool("crouch", false);
            control.center = new Vector3(control.center.x, 0f, control.center.z);
            control.height = 3.0f;

        }


        if (Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftControl))
        {
            anima.SetBool("running", true);
            myCamera.fieldOfView = 62f;
            speed = 14f;
        }
        else
        {
            anima.SetBool("running", false);
            myCamera.fieldOfView = 60f;
            if( Input.GetKey(KeyCode.LeftControl)){
                speed = 2f;
            }else{
                speed = 8f;
            }
        }
        control.Move(move *speed*Time.deltaTime);

        if (Input.GetButtonDown("Jump") && !isCrouching && (isGrounded|| doublejump > currentJump))
        {
            landing.Play();
            vel.y = Mathf.Sqrt(jumpHeight*-2f*grav);
            currentJump++;
        }

        vel.y += grav*Time.deltaTime;
        control.Move(vel * Time.deltaTime);

    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }



}
