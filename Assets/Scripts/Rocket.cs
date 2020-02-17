using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = this.GetComponent<Rigidbody>();
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    /**
    Processes the input given
    Should be able to handle input for cross platform
    */
    private void ProcessInput()
    {
        if( Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up*100000);
        }
        if(Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.forward * Time.deltaTime*90);
        }else if(Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            transform.Rotate(-Vector3.forward * Time.deltaTime*90);
        }
    }
}
