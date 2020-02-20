using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{

    // todo lighting

    private Rigidbody m_rigidBody;
    private AudioSource m_audioSource;

    [SerializeField]
    private float m_rcsThrust = 100f;

    [SerializeField]
    private float m_mainThrust =10000f;

    enum State  {Alive, Dead, Transcending};

    private State m_state;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidBody = this.GetComponent<Rigidbody>();
        m_audioSource = GetComponent<AudioSource>();
        Application.targetFrameRate = 60;

        m_rigidBody.freezeRotation =true;
        m_state = State.Alive;

    }

    // Update is called once per frame
    void Update()
    {
        if(m_state == State.Alive)
        {
            ProcessInput();
        }
        Audio();
        
    }

    /**
    Processes the input given
    Should be able to handle input for cross platform
    */
    private void ProcessInput()
    {
        
        float rotationSpeed = m_rcsThrust*Time.deltaTime;
        float mainThrust = m_mainThrust*Time.deltaTime;
        //m_rigidBody.freezeRotation = true;  //Take manual control of the rotation

        //Handle the movement
        if (Input.GetKey(KeyCode.Space))
        {
            //m_rigidBody.AddRelativeForce(Vector3.up * mainThrust);
            m_rigidBody.AddRelativeForce(Vector3.up * m_mainThrust);
            
        }
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.forward*rotationSpeed);
            
        }
        else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            transform.Rotate(-Vector3.forward*rotationSpeed);
            //m_rigidBody.freezeRotation = true;  //Take manual control of the rotation
        }

        //m_rigidBody.freezeRotation = false; //Let  physics control the rotation.
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Collided with " + collision.gameObject);
                Debug.Log("Ok");
                break;
            case "Finish":
                m_state = State.Transcending;
                Invoke("LoadLevel",1f);
                break;
            default:
                m_state = State.Dead;
                Invoke("LoadLevel",0.5f);
                break;
        }
    }

    private void LoadLevel()
    {
        if( m_state == State.Transcending)
        {
            if(SceneManager.GetActiveScene().buildIndex != SceneManager.sceneCountInBuildSettings -1)
            {
                //Loading next level
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1 );
            }else
            {
                //Loading same scene
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex );
            }
            
        }else if( m_state == State.Dead)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
    }

    private void Audio()
    {
        /**
                Process sounds
                */
        //Start Playing the audio clip
        if(m_state == State.Alive)
        {
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
}
