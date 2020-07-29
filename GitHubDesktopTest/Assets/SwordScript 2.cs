using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    public float throwForce;

    private Rigidbody rigidBody;
    private CharacterControllerScript ccs;

    // Start is called before the first frame update

    void Start()
    {
        //rigidBody = GetComponent<Rigidbody>();

        //rigidBody.AddForce((Vector3.forward + Vector3.up) * 20);

        GetComponent<Rigidbody>().velocity =
            (transform.forward + (0.25f) * transform.up) * throwForce;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(transform.position.y);

        if (transform.position.y < -1)
        {
            GameObject character = GameObject.Find("Character");
            ccs = (CharacterControllerScript)character.GetComponent(
                    typeof(CharacterControllerScript));
            ccs.teleportToSword(transform.position + new Vector3(0f, 1f, 0f), transform.rotation);
            Destroy(gameObject);
        }
    }

    private void LateUpdate()
    {
        //Debug.Log(transform.position.y);

        //if (transform.position.y < -1)
        //{
        //    GameObject character = GameObject.Find("Character");
        //    ccs = (CharacterControllerScript)character.GetComponent(
        //            typeof(CharacterControllerScript));
        //    ccs.teleportToSword(transform.position + new Vector3(0f, 1f, 0f), transform.rotation);
        //    Destroy(gameObject);
        //}
    }
}
