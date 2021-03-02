using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TPSScript : MonoBehaviour
{
    #region SerializableVariable
    //distance de la camera par rapport au player
    [SerializeField]
    private float m_currentDistance = 0;

    [SerializeField]
    private float smooth = 5;

    [SerializeField]
    private LayerMask layer;

    [SerializeField] // la ou regarde la camera
    private Transform m_Target = null;

    [SerializeField]
    private SO_PlayerController m_PlayerController;
    #endregion


    private float m_XAngleMin = -80.0f;

    private float m_XAngleMax = 5.0f;

    private Camera m_Cam = null;

    //sensibiliter
    private float m_SensivityX = 0.1f;

    private float m_SensivityY = 0.1f;

    private float m_RotationX = 0f;

    private float m_RotationY = 0f;

    private Vector3 m_OffsetCamera = Vector3.zero;

    private float m_TargetDistance=0;

    private float m_deltaTime=0;

    
    #region Rotation to aim
    [SerializeField]
    private float m_XAngleMinAim = -80.0f;
    [SerializeField]
    private float m_XAngleMaxAim = 0;
    private float m_RotationXAim = 0f;
    private float m_RotationYAim = 0f;
    [SerializeField]
    private float m_SensitivityYAim = 0.1f;

    [SerializeField]
    private BallisticTrajectoryRenderer m_Trajectory;

    [SerializeField]
    private Transform m_Player;

    #endregion

    private void Start()
    {
        //Récupération de la main camera
        //Mise en place de l'offset de la camera
        m_Cam = Camera.main;
        m_OffsetCamera = new Vector3(0.0f, -1.0f, 0.0f);
        //Cacher le curseur de la souris
        //Bloquer la souris
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        m_TargetDistance = m_currentDistance;
        m_OffsetCamera = m_Cam.transform.localPosition;
    }
    
    private void Update()
    {
        if (!m_Trajectory.Aiming)
        {
            RotationCamera(m_PlayerController.RotationVector);
        }

            
            
        else 
        {
            RotationAiming(m_PlayerController.RotationVector);
        }
       
        
        //m_deltaTime += Time.deltaTime * 5.0f;
        //if (m_currentDistance != m_TargetDistance)
        //{
        //    m_Cam.transform.localPosition = Vector3.Lerp(m_OffsetCamera, m_Target.localPosition, m_deltaTime);
        //}
        Collision();
    }

    public void RotationCamera(Vector3 p_PosCam)
    {
        //rotation par rapport a ma view, c'est ici qu'on peut gerer la sensibilité
        m_RotationX += p_PosCam.y * m_SensivityY;
        m_RotationY += p_PosCam.x * m_SensivityX;

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

    private void RotationAiming(Vector3 p_MouseAim)
    {
        

            m_RotationXAim += p_MouseAim.y * m_SensitivityYAim;
            m_RotationYAim += p_MouseAim.x * m_SensitivityYAim;
            
            Mathf.Clamp(m_RotationXAim, m_XAngleMinAim, m_XAngleMaxAim);
            Quaternion l_Rotation = Quaternion.Euler(m_RotationXAim, m_RotationYAim, transform.rotation.z);

            m_Player.rotation = l_Rotation;
            transform.rotation = l_Rotation;

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
        RaycastHit hit = new RaycastHit();
        if (Physics.Linecast(m_Target.position, transform.position, out hit, layer))
        {
            m_Cam.transform.position = Vector3.Lerp(m_Cam.transform.position, hit.point, Time.deltaTime * smooth);
        }
        else
        {
            m_Cam.transform.position = Vector3.Lerp(m_Cam.transform.position, this.transform.position, Time.deltaTime * smooth);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, m_Target.position);
    }
}
