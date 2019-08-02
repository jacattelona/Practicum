using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereScript : MonoBehaviour
{

    CharacterController controller;
    public float speed = 10f;
    public float jumpSpeed = 16f;
    public float jumpMult = 1.2f;
    public float gravity = 40f;
    public Vector3 moveDirection = Vector3.zero;
    int jumpCount = 0;

    public TrackMutes tracks;
    public charDash dash;

    private bool dJumpEnabled;
    

    // Start is called before the first frame update
    void Start()
    {
        print ("Testing...");
        controller = GetComponent<CharacterController>();
        dash = GetComponent<charDash>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTracks();

        //print(controller.isGrounded);
        if (controller.isGrounded)
        {
            jumpCount = 0;
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
            moveDirection *= speed;

            if (Input.GetButtonDown("Jump"))
            {
                float jump = jumpSpeed;
                if (tracks.WithinRange(8))
                {
                    jump *= jumpMult;
                    print("Super Jump");
                }
                moveDirection.y = jump;
                jumpCount = jumpCount + 1;
                //print(jumpCount);
            }
        }
        
        if (!controller.isGrounded)
        {

            if ((Input.GetButtonDown("Jump")) && (jumpCount == 1) && dJumpEnabled)
            {
                float jump = jumpSpeed;
                if (tracks.WithinRange(8))
                {
                    jump *= jumpMult;
                    print("Super Jump");
                }
                moveDirection.y = jump;
                jumpCount = jumpCount + 1;
                //print(jumpCount);
            }

            if (Input.GetKeyDown("a") || Input.GetKeyDown("left"))
            {
                moveDirection.x = -speed;
            }
            if (Input.GetKeyDown("d") || Input.GetKeyDown("right"))
            {
                moveDirection.x = speed;
            }

        }

            //transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * 5, Input.GetAxis("Jump") * Time.deltaTime * 5, 0f);
            //transform.Translate(Input.SetAxis())

            moveDirection.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection * Time.deltaTime);
    }

    void UpdateTracks()
    {
        if (tracks.IsActive(0))
        {
            dJumpEnabled = true;
        }
        else
        {
            dJumpEnabled = false;
        }

        if (tracks.IsActive(1))
        {
            jumpMult = 1.2f;
        }
        else
        {
            jumpMult = 1.0f;
        }

        if (tracks.IsActive(2))
        {
            dash.SetActive(true);
        }
        else
        {
            dash.SetActive(false);
        }

        //if (tracks.IsActive(3))
        //{

        //}
        //else
        //{

        //}
    }
}
