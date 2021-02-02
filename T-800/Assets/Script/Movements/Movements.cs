using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Movements : MonoBehaviour
{

    //Movements datas
    [SerializeField]
    private float m_Speed = 4;

    [SerializeField]
    private float m_AccelTime = 5f;

    private float m_Timer, m_AccelRatio;

    [SerializeField]
    private AnimationCurve m_AccelCurve;

    //grounded datas
    [SerializeField]
    private Transform m_FeetPos;

    [SerializeField]
    private float m_CheckRadius;

    [SerializeField]
    private LayerMask m_IsItGround;

    private bool m_IsGrounded = true;

    //Jump Datas
    private bool m_IsJumping = false;

    private int m_JumpNumber = 1;

    [SerializeField]
    private float m_JumpTime = 3f;

    private float m_JumpTimer, m_JumpRatio;

    [SerializeField]
    private AnimationCurve m_JumpHeightCurve;

    [SerializeField]
    private float m_JumpForce;

    // Others components
    [SerializeField]
    private Rigidbody m_rb;

    //Inputs
    public Vector2 movementInput = Vector2.zero;

    
    public bool isJumping { get { return m_IsJumping; } set { m_IsJumping = value; } }
    
    // Start is called before the first frame update

    
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
       
    }
    
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        m_IsGrounded = Physics.Raycast(m_FeetPos.position, new Vector3(0, -1, 0), out hit,m_CheckRadius,m_IsItGround);

        Debug.Log("m_IsGrounded" + m_IsGrounded);
    }
    public void Move(Vector2 _Direction, float _DeltaTime)
    {
        if (movementInput != Vector2.zero)
        {
            m_Timer += Time.deltaTime;

            if (m_Timer > m_AccelTime)
            {
                m_Timer = m_AccelTime;
            }

             m_AccelRatio = m_Timer / m_AccelTime;
        }
        else
        {
            m_Timer = 0;
        }
    

        Vector3 l_CameraForward = Camera.main.transform.forward;
        Vector3 l_CameraRight = Camera.main.transform.right;

        l_CameraForward.y = 0f;
        l_CameraRight.y = 0f;
        l_CameraForward.Normalize();
        l_CameraRight.Normalize();

        Vector3 l_DesireDirection = _Direction.y * l_CameraForward + _Direction.x * l_CameraRight;
        l_DesireDirection.Normalize();

        if (l_DesireDirection != Vector3.zero)
        {
            transform.forward = l_DesireDirection;
           
        }
  
        transform.position += new Vector3(l_DesireDirection.x * m_AccelCurve.Evaluate(m_AccelRatio) * m_Speed * Time.deltaTime, 0, l_DesireDirection.z * m_AccelCurve.Evaluate(m_AccelRatio) * m_Speed  * Time.deltaTime);
    }

    public void Jump()
    {
        
        if (m_IsJumping)
        {
            m_JumpTimer += Time.deltaTime;
            m_JumpRatio = m_JumpTimer / m_JumpTime;


            if (m_JumpTimer > m_JumpTime)
            {
                m_IsJumping = false;
                m_JumpTimer = 0;
                //m_JumpTimer = m_JumpTime;
            }
            //m_JumpNumber -= 1;
        }
        else
        {
            m_JumpTimer = 0;
        }
       
        if(m_IsGrounded)
        {
            //    m_JumpNumber = 1;
            m_JumpTimer = 0;
        }

        //if (m_JumpNumber == 1)
        //{
            m_rb.velocity = Vector3.up *( m_JumpHeightCurve.Evaluate(m_AccelRatio) * m_JumpForce + 4);
          
        //}


       
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(m_FeetPos.position, new Vector3(0, -4, 0));
    }

}
