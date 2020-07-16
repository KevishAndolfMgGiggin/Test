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

    }
}
