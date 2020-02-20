using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsMovement : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private float m_speed = 5;
    [SerializeField]
    private bool m_verticalMov = true;
    [SerializeField]
    private float m_movRange = 20;
    private Vector3 m_position;
    private bool m_forward=true;
    void Start()
    { 
        m_position = this.transform.position; 
    }

    // Update is called once per frame
    void Update()
    {
        if(m_verticalMov)
        {
            if(m_forward)
            {
                this.transform.position += (new Vector3(m_speed,0,0))*Time.deltaTime;
            }else this.transform.position -= (new Vector3(m_speed,0,0))*Time.deltaTime;

            
            if(this.transform.position.x > m_position.x + m_movRange) m_forward =false;
            if( this.transform.position.x < m_position.x) m_forward =true;
            
            
        }else
        {
            if(m_forward )
            {
                this.transform.position += (new Vector3(0,m_speed,0))*Time.deltaTime;
            }else this.transform.position -= (new Vector3(0,m_speed,0))*Time.deltaTime;

            
            if(this.transform.position.y > m_position.y + m_movRange) m_forward =false;
            if( this.transform.position.y < m_position.y) m_forward =true;
        }
        
    }

    private bool Toggle(bool p)
    {
        if( p) return false;
        return true;
    }
}
