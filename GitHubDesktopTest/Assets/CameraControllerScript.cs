using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerScript : MonoBehaviour
{

    public float rotationSpeed;
    public Transform Target, Player;

    private float xAxis;
    private float yAxis;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        CamControl();
    }

    void CamControl()
    {

        xAxis -=
            Input.GetAxis("RightStick_Vertical") *
            rotationSpeed;
        xAxis %= 360;

        yAxis +=
            Input.GetAxis("RightStick_Horizontal") *
            rotationSpeed;
        yAxis %= 360;

        xAxis = Mathf.Clamp(xAxis, -22.5f, 45f);

        transform.LookAt(Target);

        Target.rotation = Quaternion.Euler(xAxis, yAxis, 0f);
        Player.rotation = Quaternion.Euler(0f, yAxis, 0f);

    } 
}
