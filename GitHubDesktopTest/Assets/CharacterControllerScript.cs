using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    private CharacterController _controller;

    public float moveSpeed;

    public float backSpeedMultiplier;
    public float strafeSpeedMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        applyMovement();
    }

    private void applyMovement()
    {
        //xAxis and zAxis from left thumbstick
        float xAxis = Input.GetAxis("LeftStick_Horizontal");
        float zAxis = Input.GetAxis("LeftStick_Vertical");
        //x movement adjusted for strafe speed
        float xMove = xAxis *= strafeSpeedMultiplier;

        //back movement adjusted for back speed
        float zMove = zAxis;
        if (zAxis < 1)
        {
            zMove *= backSpeedMultiplier;
        }

        /* x movement is strafing
         * 
         * z-movement is forward and back
         */
        Vector3 move =
            new Vector3(
                xMove,
                0f,
                zMove);



        //applies movement to character
        //_controller.Move(move * Time.deltaTime * moveSpeed);

        
        if (xAxis != 0)
        {
            _controller.Move(transform.right * xMove * Time.deltaTime * moveSpeed);
        }
        if (zAxis != 0)
        {
            _controller.Move(transform.forward * zMove * Time.deltaTime * moveSpeed);
        }



    }
}
