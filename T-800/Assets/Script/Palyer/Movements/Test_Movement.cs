using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Movement : MonoBehaviour
{
    [SerializeField]
    float m_MaxSpeed = 12;

    [SerializeField]
    private float m_TimeZeroToMax;
    [SerializeField]
    private float m_TimeMaxToZero;

    [SerializeField]
    float m_SpeedAccel = 0;

    [SerializeField]
    Vector3 m_Velocity;

    float m_AccRatePerSec;
    float m_DecRatePerSec;
    //Vector3 m_DesireVelocity;

    // Start is called before the first frame update
    void Start()
    {
        m_AccRatePerSec = m_MaxSpeed / m_TimeZeroToMax;
        m_DecRatePerSec = m_MaxSpeed / m_TimeMaxToZero;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Move(Vector3 _Direction, float p_DeltaTime)
    {
        Vector3 l_CameraForward = Camera.main.transform.forward;
        Vector3 l_CameraRight = Camera.main.transform.right;

        l_CameraForward.y = 0f;
        l_CameraRight.y = 0f;
        l_CameraForward.Normalize();
        l_CameraRight.Normalize();

        Vector3 l_DesireDirection = _Direction.y * l_CameraForward + _Direction.x * l_CameraRight;
        l_DesireDirection.Normalize();

        if (_Direction != Vector3.zero)
        {
            m_SpeedAccel += m_AccRatePerSec * Time.deltaTime;

            m_Velocity = new Vector3(l_DesireDirection.x * m_SpeedAccel * p_DeltaTime, 0, l_DesireDirection.z * m_SpeedAccel * p_DeltaTime);
            m_SpeedAccel = Mathf.Min(m_SpeedAccel, m_MaxSpeed);
            transform.position += m_Velocity;
        }
        else
        {
            Vector3 m_DesireVelocity = m_Velocity - _Direction;
            m_DesireVelocity.Normalize();

            m_SpeedAccel -= m_DecRatePerSec * Time.deltaTime;
            m_SpeedAccel = Mathf.Max(m_SpeedAccel, 0);

            Vector3 steering = m_DesireVelocity - m_Velocity;
            m_Velocity = new Vector3(steering.x * m_SpeedAccel * p_DeltaTime, 0, steering.z * m_SpeedAccel * p_DeltaTime);

            transform.position += m_Velocity;
        }
    }
}
