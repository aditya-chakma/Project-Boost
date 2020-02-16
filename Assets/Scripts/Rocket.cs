using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
            Debug.Log("Boost!");
        }
        if(Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            Debug.Log("Rotating left!");
        }else if(Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            Debug.Log("Rotating right!");
        }
    }
}
