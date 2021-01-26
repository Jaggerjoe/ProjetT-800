using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TPSScript : MonoBehaviour
{
    [SerializeField]
    private InputManager m_InputManage = null;

    private float m_XAngleMin = -80.0f;
    private float m_XAngleMax = -5.0f;

    [SerializeField] // la ou regarde la camera
    private Transform m_Target = null;

    private Camera m_Cam = null;

    //distance de la camera par rapport au player
    [SerializeField]
    private float m_currentDistance = 0;

    //sensibiliter
    private float m_SensivityX = 0.1f;
    private float m_SensivityY = 0.1f;


    private float m_RotationX = 0f;
    private float m_RotationY = 0f;

    private Vector3 m_OffsetCamera = Vector3.zero;
 
    private float m_InitialDistance = 0;
    private float m_TargetDistance=0;
    private float m_deltaTime=0;
    private Vector3 m_TeleportOffset = new Vector3(1.5f, 1.5f, 0.0f);
    private Vector3 m_TargetOffset = Vector3.zero;
    private Vector3 m_OriginalOffset = Vector3.zero;


    public float minDist = 1;
    public float maxDist = 4;
    Vector3 dollyDIr = Vector3.zero;
    [SerializeField]
    float smooth = 5;
    public LayerMask layer;


    private void Awake()
    {
        dollyDIr = transform.localPosition.normalized;
    }
    private void Start()
    {
        m_Cam = Camera.main;
        m_OffsetCamera = new Vector3(0.0f, -1.0f, 0.0f);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        m_TargetDistance = m_currentDistance;
        m_OffsetCamera = m_Cam.transform.localPosition;
    }
    
    private void Update()
    {
        m_deltaTime += Time.deltaTime * 5.0f;
        if (m_currentDistance != m_TargetDistance)
        {
            m_Cam.transform.localPosition = Vector3.Lerp(m_OriginalOffset, m_TargetOffset, m_deltaTime);
            m_currentDistance = Mathf.Lerp(m_InitialDistance, m_TargetDistance, m_deltaTime);
        }
        Collision();
    }

    //pour ne pas gener les move 
    private void LateUpdate()
    {
        RotationCamera();
    }

    private void RotationCamera()
    {
        //rotation par rapport a ma view, c'est ici qu'on peut gerer la sensibilité
        m_RotationX += m_InputManage.CameraPos.y * m_SensivityY;
        m_RotationY += m_InputManage.CameraPos.x * m_SensivityX;

        //m_RotationY = Mathf.Clamp(m_RotationY, m_Y_Angle_Min, m_Y_Angle_Max);
        m_RotationX = Mathf.Clamp(m_RotationX, m_XAngleMin, m_XAngleMax);
       
        Quaternion l_Rotation = Quaternion.Euler(m_RotationX, m_RotationY, 0);

        //on prend le forward du monde qui est le z est on fait y fait = la new position on multipli par la distance et la roation (rotation du quaternion qu'on va appliquer)
        //tout ça par rapport a l'input.
        Vector3 l_NextPosition = l_Rotation * Vector3.forward * m_currentDistance;
        Vector3 l_CameraPosition = m_Target.position + m_OffsetCamera;
        //va gere la postion de la camera
        transform.position = l_NextPosition + l_CameraPosition;
        transform.LookAt(m_Target);
    }

    void Collision()
    {
        Vector3 desireCameraPos = transform.TransformPoint(m_OffsetCamera * maxDist);

        RaycastHit hit;
        if (Physics.Linecast(transform.position, m_Target.position, out hit, layer))
        {
            m_Cam.transform.position = Vector3.Lerp(m_Cam.transform.position, hit.point, Time.deltaTime * smooth);
        }
        else
        {
            m_Cam.transform.position = Vector3.Lerp(m_Cam.transform.position, this.transform.position, Time.deltaTime * smooth);
        }
        //m_Cam.transform.position = Vector3.Lerp(m_Cam.transform.position, m_OffsetCamera * m_currentDistance, Time.deltaTime * smooth);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, m_Target.position);
    }
}
