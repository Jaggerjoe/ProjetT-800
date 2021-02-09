using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movements : MonoBehaviour
{

    //Movements datas
    [SerializeField]
    private float m_Speed = 4;

    private float m_SpeedAccel = 0;

    [SerializeField]
    private float m_AccelTime = 5f;
    private float m_Timer = 0;
    private float m_AccelRatio = 0;

    private float m_JumpTimer = 0;

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

    private bool m_IsJumping = false;
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



    public bool Jumpbool { get { return m_IsJumping; } set { m_IsJumping = value; } }
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }
    
    // Update is called once per frame
    void Update()
    {
        Jump();
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
        Vector3 l_Deplacement = new Vector3(l_DesireDirection.x * m_SpeedAccel * Time.deltaTime, 0, l_DesireDirection.z * m_SpeedAccel * Time.deltaTime);
        transform.position += l_Deplacement;
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
        if (m_IsJumping)
        {

            if (m_JumpTimer <= 3)
            {
                m_JumpTimer += Time.deltaTime;
            }
            else
            {
                m_JumpTimer = 0;
            }
            Debug.Log(m_JumpTimer);
            Vector3 l_TargetPos = transform.position;
            Vector3 l_Player = transform.position;
            l_TargetPos.y = transform.position.y;
            //l_Player.y += l_TargetPos.y + m_JumpHeight.Evaluate(m_JumpTimer);
            transform.position = l_Player;
            //l_TragetPos += new Vector3(0, l_TragetPos.y + m_JumpHeight.Evaluate(Time.time) * m_JumpForce * Time.deltaTime, 0);


        }


    }

   
}
