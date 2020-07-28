using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;

    public float jumpHeight = 1.0f;
    public float gravityValue = -9.81f;
    public float moveSpeed;
    public float backSpeedMultiplier;
    public float strafeSpeedMultiplier;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        applyMovement();
        applyJump();
    }

    private void applyMovement()
    {
        if (transform.position.y <= 0)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
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

        /* x-movement is strafing and is multiplied by the character's
         * right direction to move relative to the character
         * 
         * z-movement is forward and back and is multiplied by the character's
         * forward directions to move relative to the character
         * 
         * both are multiplied by Time.delta time to adjust for framerate 
         * and the moveSpeed set in the inspector
         */
        Vector3 move =
            (transform.right * xMove +
            transform.forward * zMove) *
            Time.deltaTime * moveSpeed;

        //applies the movement to character with the move Vector
        if (move != Vector3.zero)
        {
            controller.Move(move);
        }
    }

    public float jumpForce;
    public float jumpTimeMax;

    private float jumpTimeCounter;
    private bool isJumping;

    private void applyJump()
    {
        //checks if the player is grounded
        if (isGrounded)
        {
            playerVelocity.y = 0f;
        }

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
            jumpTimeCounter = jumpTimeMax;
            playerVelocity.y = jumpForce;
            //playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        if (Input.GetButton("Jump") && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                playerVelocity.y = jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
        else
        {
            isJumping = false;
        }


        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        //sets y-position to 0 so ther character never falls beneath the floor
        if (transform.position.y <= 0)
        {
            transform.position =
                new Vector3(transform.position.x, 0f, transform.position.z);
        }
    }
}
