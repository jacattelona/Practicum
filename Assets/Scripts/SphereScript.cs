using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SphereScript : MonoBehaviour
{

    CharacterController controller;
    public float speed = 10f;
    public float jumpSpeed = 16f;
    public float jumpMult = 1.2f;
    public float gravity = 40f;
    public Vector3 moveDirection = Vector3.zero;
    int jumpCount = 0;

    float dashTime = 0;
    float dashSpeed = 18.0f;
    float dashMult = 1.0f;
    bool dashing = false;

    public TrackMutes tracks;

    private bool dashEnabled = true;
    private bool dJumpEnabled;


    public Toggle toggle1;
    public Toggle toggle2;
    public Toggle toggle3;

    // Start is called before the first frame update
    void Start()
    {
        //print ("Testing...");
        controller = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        UpdateTracks();

        Jump();
        Walk();
        Dash();

        //Terminal Velocity
        if (moveDirection.y > -25.0f)
            moveDirection.y -= gravity * Time.deltaTime;

        //Move character based on previous function calls
        controller.Move(moveDirection * Time.deltaTime);
    }

    //Dash portion of update script, only affects x component of moveDirection
    void Dash()
    {
        //If we just pressed shift, and not currently dashing, and dashing is enabled
        if (Input.GetKeyDown(KeyCode.LeftShift) && !dashing && dashEnabled)
        {
            //set dash to true, give half a second of dash time
            dashing = true;
            dashTime = .5f;

            if (tracks.WithinRange(8))
            {
                print("super dash");
                if (dashMult > 0)
                    dashMult = 1.6f;
                else if (dashMult < 0)
                    dashMult = -1.6f;
            }

            else
            {
                if (dashMult > 0)
                    dashMult = 1.0f;
                else if (dashMult < 0)
                    dashMult = -1.0f;
            }
        }

        //if dashing
        if (dashing)
        {
            //boost current movement direction
            moveDirection.x += (dashTime * dashSpeed * dashMult);

            //tick down dash time
            dashTime -= Time.deltaTime;
        }

        //if we finished dashing
        if (dashTime <= 0)
        {
            //set dashTime to 0, set dashing to false
            dashTime = 0;
            dashing = false;
        }
    }

    //Basic left+right movement portion of update script, only affects x component of moveDirection
    void Walk()
    {
        //Get axis, multiiply by speed
        moveDirection.x = Input.GetAxis("Horizontal");
        moveDirection.x *= speed;

        //set the proper direction for dashing
        if (moveDirection.x > 0) dashMult = 1.0f;
        else  if (moveDirection.x < 0) dashMult = -1.0f;
    }

    //Jump portion of update script (only affects y component of moveDirection)
    void Jump()
    {
        //if grounded
        if (controller.isGrounded)
        {
            //reset jump count
            jumpCount = 0;

            //if jump button is pressed
            if (Input.GetButtonDown("Jump"))
            {
                //get jump height
                float jump = jumpSpeed;

                //extra jump height is given if timed properly with music
                if (tracks.WithinRange(8))
                {
                    jump *= jumpMult;
                    print("Super Jump");
                }

                //set moveDirection y to the jump value
                moveDirection.y = jump;

                //set jumpcount to 1
                jumpCount = 1;
            }
        }

        //if in the air
        if (!controller.isGrounded)
        {
            //make sure we have 1 jump if we haven't used it already
            if (jumpCount == 0)
                jumpCount = 1;

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

        }
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
            dashEnabled = true;
        }
        else
        {
            dashEnabled = false;
        }

        //if (tracks.IsActive(3))
        //{

        //}
        //else
        //{

        //}
    }

}
