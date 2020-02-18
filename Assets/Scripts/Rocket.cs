using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private Rigidbody m_rigidBody;
    private AudioSource m_audioSource;
    // Start is called before the first frame update
    void Start()
    {
        m_rigidBody = this.GetComponent<Rigidbody>();
        m_audioSource = GetComponent<AudioSource>();
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
        Audio();
    }

    /**
    Processes the input given
    Should be able to handle input for cross platform
    */
    private void ProcessInput()
    {
        
        //Handle the movement
        if (Input.GetKey(KeyCode.Space))
        {
            //rigidBody.AddRelativeForce(Vector3.up*100000);
            m_rigidBody.AddRelativeForce(Vector3.up);
        }
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.forward);
            m_rigidBody.freezeRotation = true;  //Take manual control of the rotation

        }
        else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            transform.Rotate(-Vector3.forward);
            m_rigidBody.freezeRotation = true;  //Take manual control of the rotation

        }

        m_rigidBody.freezeRotation = false; //Let  physics control the rotation.

    }

    private void Audio()
    {
        /**
                Process sounds
                */
        //Start Playing the audio clip
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_audioSource.Play();
        }
        //Stops playing the sound
        if (Input.GetKeyUp(KeyCode.Space))
        {
            m_audioSource.Stop();
        }
    }
}
