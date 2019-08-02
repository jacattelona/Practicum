using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charDash : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 moveDirection;
    public float speed;
    public float dashSpeed;
    public float dashTime;
    public float startDashTime;
    int direction;
    bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        GameObject sphere = GameObject.Find("Sphere");
        SphereScript sphereScript = sphere.GetComponent<SphereScript>();
        rb = GetComponent<Rigidbody>();
        moveDirection = sphereScript.moveDirection;
        speed = sphereScript.speed;

    }

    // Update is called once per frame
    void Update()
    {
        if (direction == 0)
        {
            if (Input.GetKeyDown("a") || Input.GetKeyDown("left"))
            {
                direction = 1;
            }
            else if (Input.GetKeyDown("d") || Input.GetKeyDown("right"))
            {
                direction = 2;
            }
        } else
        {
            if(dashTime <= 0)
            {
                direction = 0;
                dashTime = startDashTime;
                rb.velocity = Vector3.zero;
            } else
                {
                dashTime -= Time.deltaTime;

                if ((moveDirection.x == -speed) && Input.GetKeyDown("left shift"))
                {
                    rb.velocity = Vector3.left * dashSpeed;

                } else if ((moveDirection.x == speed) && Input.GetKeyDown("left shift"))
                {
                     rb.velocity = Vector3.right * dashSpeed;
                    }
            }
        }
    }

    public void SetActive(bool active)
    {
        isActive = active;
    }
}
