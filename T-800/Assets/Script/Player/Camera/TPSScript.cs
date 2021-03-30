using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;

public class TPSScript : MonoBehaviour
{
    #region SerializableVariable
    //distance de la camera par rapport au player
    [SerializeField]
    private float m_currentDistance = 0;

    [SerializeField]
    private float m_currentDistanceAim = 0;

    [SerializeField]
    private float m_Smooth = 5;

    [SerializeField]
    private LayerMask m_Layer;

    [SerializeField] // la ou regarde la camera
    private Transform m_Target = null;

    [SerializeField] // la ou regarde la camera
    private Transform m_Slot = null;

    [SerializeField]
    private SO_PlayerController m_PlayerController;
    #endregion

    [SerializeField]
    private float m_XAngleMin = -80.0f;
    [SerializeField]
    private float m_XAngleMax = -15.0f;

    private Camera m_Cam = null;

    //sensibiliter
    private float m_SensivityX = 0.1f;

    private float m_SensivityY = 0.1f;

    private float m_RotationX = 0f;

    private float m_RotationY = 0f;

    private Vector3 m_OffsetCamera = Vector3.zero;
    [SerializeField]
    private Vector3 m_OffsetCameraAim =new Vector3 (0,5,0);

    private float m_TargetDistance=0;

    private float m_deltaTime=0;

    #region Smooth Switch
   
    private Vector3 m_CameraPosition = Vector3.zero;
    
    private Vector3 m_AimCameraPosition = Vector3.zero;

    #endregion


    #region Rotation to aim

    [SerializeField]
    private float m_XAngleMinAim = -80.0f;
    [SerializeField]
    private float m_XAngleMaxAim = -15.0f;
    [SerializeField]
    private float m_XAngleMinAimCam = -80.0f;
    [SerializeField]
    private float m_XAngleMaxAimCam = -15.0f;
    private float m_RotationXAim = 0f;
    private float m_RotationXAimCam = 0f;
    private float m_RotationYAim = 0f;
    [SerializeField]
    private float m_SensitivityYAim = 0.1f;

    [SerializeField]
    private BallisticTrajectoryRenderer m_Trajectory;

    #endregion

    private void Start()
    {
        //Récupération de la main camera
        //Mise en place de l'offset de la camera
        m_Cam = Camera.main;
        m_OffsetCamera = new Vector3(0.0f, -1.0f, 0.0f);
        //Cacher le curseur de la souris
        //Bloquer la souris
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
        m_TargetDistance = m_currentDistance;
        // m_OffsetCamera = m_Cam.transform.localPosition;
    }
    
    private void Update()
    {

        if (m_Trajectory.Aiming)
        {
            RotationAiming(m_PlayerController.RotationVector);
            
        }
        else
        {

            RotationCamera(m_PlayerController.RotationVector);
        }

        Collision();

        //        m_deltaTime += Time.deltaTime * 5.0f;
        //        if (m_currentDistance != m_TargetDistance)
        //        {
        //           m_Cam.transform.localPosition = Vector3.Lerp(m_OffsetCamera, m_Target.localPosition, m_deltaTime);
        //        }
        //    }
        //private void LateUpdate() {
        //            RotationCamera(m_PlayerController.RotationVector);

    }
        public void RotationCamera(Vector3 p_PosCam)
    {
        //rotation par rapport a ma view, c'est ici qu'on peut gerer la sensibilité
        m_RotationX += p_PosCam.y * m_SensivityY;
        m_RotationY += p_PosCam.x * m_SensivityX;

        // m_RotationY = Mathf.Clamp(m_RotationY, m_Y_Angle_Min, m_Y_Angle_Max);
        m_RotationX = Mathf.Clamp(m_RotationX, m_XAngleMin, m_XAngleMax);
       
        Quaternion l_Rotation = Quaternion.Euler(m_RotationX, m_RotationY, 0);
        Quaternion l_RotationTarget = Quaternion.Euler(0, m_RotationY, 0);


        //Debug.Log("fbn" + m_Target.rotation);
        //m_Target.rotation = l_RotationTarget;

        //on prend le forward du monde qui est le z est on fait y fait = la new position on multipli par la distance et la roation (rotation du quaternion qu'on va appliquer)
        //tout ça par rapport a l'input.
        Vector3 l_NextPosition = l_Rotation * Vector3.forward * m_currentDistance;
        Vector3 l_CameraPosition = m_Target.position + m_OffsetCamera;
        //va gere la postion de la camera
        m_CameraPosition = l_NextPosition + l_CameraPosition;
        // transform.position = Vector3.Lerp(m_AimCameraPosition, m_CameraPosition, 1);
        transform.position = m_CameraPosition;
        transform.LookAt(m_Target);

        //if (transform.position != m_CameraPosition)
        //{
        //    transform.DOMove(m_CameraPosition, 1f, false);
        //}

    }

    private void RotationAiming(Vector3 p_MouseAim)
    {
        

            m_RotationXAim += p_MouseAim.y * m_SensitivityYAim;
            m_RotationXAimCam += p_MouseAim.y * m_SensitivityYAim;
            m_RotationYAim += p_MouseAim.x * m_SensitivityYAim;

            m_RotationXAim = Mathf.Clamp(m_RotationXAim, m_XAngleMinAim, m_XAngleMaxAim);
            m_RotationXAimCam = Mathf.Clamp(m_RotationXAimCam, m_XAngleMinAimCam, m_XAngleMaxAimCam);
            Quaternion l_Rotation = Quaternion.Euler(0, m_RotationYAim,0);
            Quaternion l_SlotRotation = Quaternion.Euler(m_RotationXAim, m_RotationYAim, 0);
            Quaternion l_CamRotation = Quaternion.Euler(m_RotationXAimCam, m_RotationYAim, 0);

        m_Target.rotation = l_Rotation;
             m_Slot.rotation = l_SlotRotation;
            //m_Cam.transform.rotation = l_Rotation;
            transform.rotation = l_CamRotation;
     

            Vector3 l_NextPosition = l_Rotation * Vector3.back * m_currentDistanceAim;
            Vector3 l_CameraPosition = m_Target.position + m_OffsetCameraAim;



            m_AimCameraPosition = l_NextPosition + l_CameraPosition;
            //transform.position = Vector3.Lerp(m_CameraPosition, m_AimCameraPosition, 1);
            transform.position = m_AimCameraPosition;
        transform.LookAt(m_Target);


        //if (transform.position != m_AimCameraPosition)
        //{

        //    transform.DOMove(m_AimCameraPosition, 1f, false);
        //}


    }

    void Collision()
    {
        RaycastHit hit = new RaycastHit();
        if (Physics.Linecast(m_Target.position, transform.position, out hit, m_Layer))
        {
            m_Cam.transform.position = Vector3.Lerp(m_Cam.transform.position, hit.point, Time.deltaTime * m_Smooth);
        }
        else
        {
            m_Cam.transform.position = Vector3.Lerp(m_Cam.transform.position, this.transform.position, Time.deltaTime * m_Smooth);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, m_Target.position);
    }
}
