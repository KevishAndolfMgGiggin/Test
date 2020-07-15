using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcoMan : MonoBehaviour
{
    private CharacterController _controller;

    public float forwardSpeed;
    public float backSpeed;
    public float turnSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            _controller.Move(transform.forward * Time.deltaTime * forwardSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            _controller.Move(transform.forward * -1 * Time.deltaTime * backSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            _controller.transform.Rotate(Vector3.down * turnSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            _controller.transform.Rotate(Vector3.up * turnSpeed);
        }
    }
}
