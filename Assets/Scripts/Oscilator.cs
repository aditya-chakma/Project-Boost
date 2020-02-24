using UnityEngine;

[DisallowMultipleComponent]
public class Oscilator : MonoBehaviour
{
    // Start is called before the first frame update
    [Range(0,1)]
    [SerializeField]
    private float movementFactor;
    [SerializeField]
    private float m_Period;
    [SerializeField]
    Transform m_TargetPosition;
    private Vector3 m_startingPosition,moveDirection;
    bool m_forward;
    private float m_signFact;

    void Start()
    {

        m_signFact =0.0f;
        m_forward = true;
        m_startingPosition = this.gameObject.transform.position;
        moveDirection = m_TargetPosition.position - m_startingPosition;
        //this.gameObject.transform.position = m_startingPosition + moveDirection*movementFactor;
    }

    // Update is called once per frame
    void Update()
    {
        if( m_Period <= Mathf.Epsilon) return;
        this.transform.position = m_startingPosition + moveDirection*movementFactor*Mathf.Sin(m_signFact);
        m_signFact+=2*Mathf.PI*Time.deltaTime/m_Period;
        
        
    }

    private bool Toogle(bool p)
    {
        if(p) return false;
        else return true;
    }
}
