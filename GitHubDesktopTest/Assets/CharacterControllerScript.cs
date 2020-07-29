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
        //isGrounded is only true when at or below the ground level of y=0
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
    public float jumpAgainMaxHeight;

    private float jumpTimeCounter;
    private bool isJumping;
    private bool jumpAgain;

    private void applyJump()
    {
        //checks if the player is grounded
        if (isGrounded)
        {
            playerVelocity.y = 0f;
        }

        /* If the character is on the ground and jump is pressed
         * or if jump was pressed within the buffer distance when falling
         * to the ground: the the character will jump, reset the jump time
         * counter, apply the jump force, and set jump again to false;
         */
        if ((Input.GetButtonDown("Jump") || jumpAgain) && isGrounded)
        {
            isJumping = true;
            jumpTimeCounter = jumpTimeMax;
            playerVelocity.y = jumpForce;
            jumpAgain = false;
            //playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        /* if player is still holding jump button since jumping then they can
         * continue applying the jump force and making the character's jump
         * higher until the jump time counter runs out in which case the jump
         * ends and isJumping becomes false
         * 
         * else if the player is jumping but has released the jump button then
         * the jump ends and isJumping becomes false
         */
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
        //if(Input.GetButtonUp("Jump") && isJumping)
        {
            isJumping = false;
        }

        /* if the player presses the jump button while falling and is between
         * the ground and the jumpAgainMaxHeight set in the inspector then 
         * jumpAgain is set to true and the player will jump again instantly
         * upon landing
        */
        if ((transform.position.y < jumpAgainMaxHeight) &&
            playerVelocity.y < 0 &&
            Input.GetButtonDown("Jump"))
        {
            jumpAgain = true;
        }

        // applys gravity if the character is not on the ground
        if (!isGrounded)
        {
            playerVelocity.y += gravityValue * Time.deltaTime;
        }
        // applys y-velocity if it has a value
        if (Mathf.Abs(playerVelocity.y) > Mathf.Epsilon)
        {
            controller.Move(playerVelocity * Time.deltaTime);
        }

        // sets y-position to 0 if negative so the character never falls beneath
        // the floor
        if (transform.position.y < 0f)
        {
            transform.position =
                new Vector3(transform.position.x, 0f, transform.position.z);
        }
    }
}
