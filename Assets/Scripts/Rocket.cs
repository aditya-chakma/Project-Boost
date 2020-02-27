using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{

    // todo lighting

    [SerializeField]
    private float m_rcsThrust = 100f;
    [SerializeField]
    private float m_mainThrust =10000f;
    [SerializeField]
    private AudioClip m_mainEngineAudio, m_successAudio, m_deathAudio ;
    [SerializeField] ParticleSystem m_mainEngineParticle, m_successParticle, m_deathParticle;

    private Rigidbody m_rigidBody;
    private AudioSource m_audioSource;

    enum State  {Alive, Dead, Transcending};
    private State m_state;
    private bool m_Collision;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidBody = this.GetComponent<Rigidbody>();
        m_audioSource = GetComponent<AudioSource>();
        Application.targetFrameRate = 60;

        m_state = State.Alive;
        m_Collision = true;

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
        if(Debug.isDebugBuild)
        {
            ProcessDebugInput();
        }
        //Handle the movement
        ProcessThrustInput();
        ProcessRotationInput();
        m_rigidBody.freezeRotation = false; //Let  physics control the rotation.
    }

    private void ProcessDebugInput()
    {
        if(Input.GetKey(KeyCode.L))
        {
            m_state = State.Transcending;
            LoadLevel();
        }
        if(Input.GetKey(KeyCode.C))
        {
            m_Collision = !m_Collision;
        }
    }

    private void ProcessThrustInput()
    {
        float mainThrust = m_mainThrust*Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            //m_rigidBody.AddRelativeForce(Vector3.up * mainThrust);
            m_rigidBody.AddRelativeForce(Vector3.up * mainThrust);
            m_rigidBody.freezeRotation =true;
        }
    }

    private void ProcessRotationInput()
    {
        float rotationSpeed = m_rcsThrust*Time.deltaTime;
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.forward*rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            transform.Rotate(-Vector3.forward*rotationSpeed);
            //m_rigidBody.freezeRotation = true;  //Take manual control of the rotation
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(!m_Collision) return;
        if(m_state != State.Alive)
        {
            return;
        }
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Collided with " + collision.gameObject);
                Debug.Log("Ok");
                break;
            case "Finish":
                m_state = State.Transcending;
                m_audioSource.PlayOneShot(m_successAudio);
                m_successParticle.Play();
                Invoke("LoadLevel",1f);
                break;
            default:
                m_state = State.Dead;
                m_audioSource.PlayOneShot(m_deathAudio);
                m_deathParticle.Play();
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
                // todo Load winning scene 
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
                m_audioSource.PlayOneShot(m_mainEngineAudio);
                m_mainEngineParticle.Play();
            }
            //Stops playing the sound
            if (Input.GetKeyUp(KeyCode.Space))
            {
                m_audioSource.Stop();
                m_mainEngineParticle.Stop();
            }
        }
        
    }

    private bool Toogle(bool p)
    {
        if(p) return false;
        return true;
    }
}
