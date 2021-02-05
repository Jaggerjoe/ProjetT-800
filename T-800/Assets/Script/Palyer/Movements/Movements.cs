using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Movements : MonoBehaviour
{

    //Movements datas
    [SerializeField]
    private float m_Speed = 4;

    private float m_SpeedAccel = 0;

    [SerializeField]
    private float m_AccelTime = 5f;

    private float m_Timer, m_AccelRatio;

    [SerializeField]
    private AnimationCurve m_AccelCurve;

    [SerializeField]
    private AnimationCurve m_Deseleration;

    private float m_TimerDese = 0;

    [SerializeReference]
    private AnimationCurve m_JumpHeight;

    [SerializeField]
    private float m_CheckRadius;

    [SerializeField]
    private LayerMask m_IsItGround;

    private bool m_IsGrounded = true;


    [SerializeField]
    private float m_JumpForce;

    // Others components
    [SerializeField]
    private Rigidbody m_rb;

    [SerializeField]
    private float m_FallMultiplier = 2.5f;

    [SerializeField]
    private float m_LowJumpMultiplier = 2f;

    // Start is called before the first frame update


    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }
    
    // Update is called once per frame
    void Update()
    {
        //AddJump();
    }
    public void Move(Vector2 _Direction, float _DeltaTime)
    {
        if (_Direction != Vector2.zero)
        {
            m_Timer += Time.deltaTime;

            m_SpeedAccel = m_Speed * m_AccelCurve.Evaluate(m_Timer);
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
        
        transform.position += new Vector3(l_DesireDirection.x * m_SpeedAccel * Time.deltaTime, 0, l_DesireDirection.z * m_SpeedAccel * Time.deltaTime);

        //Vector3 l_NewVec = l_DesireDirection;

        //if (_Direction == Vector2.zero)
        //{
        //    m_TimerDese += Time.deltaTime;

        //    m_SpeedAccel = m_Speed * m_Deseleration.Evaluate(m_TimerDese);

        //    transform.position += new Vector3(l_NewVec.x * m_SpeedAccel * Time.deltaTime , 0, l_NewVec.z * m_SpeedAccel * Time.deltaTime);
        //}
        //else
        //{
        //    m_TimerDese = 0;
        //}
    }

    void Deceleration()
    {

      //transform.position += new Vector3(l_DesireDirection.x * m_AccelCurve.Evaluate(m_AccelRatio) * m_Speed * Time.deltaTime, 0, l_DesireDirection.z * m_AccelCurve.Evaluate(m_AccelRatio) * m_Speed * Time.deltaTime);

    }

    public void Jumping()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, m_CheckRadius, m_IsItGround))
        {
            m_rb.velocity = Vector3.up * m_JumpForce;
        }
    }

    public void Jump()
    {
        //float l_Timer = 0;
        //if(l_Timer <= 3)
        //{
        //    l_Timer += Time.deltaTime;
        //}
        //else
        //{
        //    l_Timer = 3;
        //}

        Vector3 l_TragetPos = transform.position;
        l_TragetPos.y = transform.position.y;
        l_TragetPos += new Vector3(0, l_TragetPos.y + m_JumpHeight.Evaluate(Time.time) * m_JumpForce * Time.deltaTime, 0);
        transform.position = l_TragetPos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, new Vector3(0, -4, 0));
    }
}
