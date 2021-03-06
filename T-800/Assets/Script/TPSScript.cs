﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TPSScript : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset m_PlyerControl = null;
    Vector2 m_PosCamera = Vector2.zero;

    private const float m_XAngleMin = -80.0f;
    private const float m_XAngleMax = -5.0f;

    [SerializeField] // la ou regarde la camera
    private Transform m_Target = null;

    private Camera m_Cam = null;

    //distance de la camera par rapport au player
    [SerializeField]
    private float m_currentDistance;

    //sensibiliter
    private float m_SensivityX = 0.1f;
    private float m_SensivityY = 0.1f;


    private float m_RotationX = 0f;
    private float m_RotationY = 0f;

    private Vector3 m_OffsetCamera;
 

    private bool m_HoldTeleport = false;
    private float m_InitialDistance;
    private float m_TargetDistance;
    private float m_deltaTime;
    private Vector3 m_TeleportOffset = new Vector3(1.5f, 1.5f, 0.0f);
    private Vector3 m_TargetOffset;
    private Vector3 m_OriginalOffset;


    public float minDist = 1;
    public float maxDist = 4;
    Vector3 dollyDIr;
    float smooth = 5;
    public LayerMask layer;

    private void Awake()
    {
        InputActionMap playerMap = m_PlyerControl.FindActionMap("Player");
        InputAction rotationCamera = m_PlyerControl.FindAction("Look");
        rotationCamera.performed += (ctx) => { m_PosCamera = ctx.ReadValue<Vector2>(); };
        rotationCamera.canceled += (ctx) => { m_PosCamera = Vector2.zero; };

        dollyDIr = transform.localPosition.normalized;

        playerMap.Enable();
    }
    private void Start()
    {
        m_Cam = Camera.main;
        m_OffsetCamera = new Vector3(0.0f, -1.0f, 0.0f);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        //m_TargetDistance = m_currentDistance;
        m_OffsetCamera = m_Cam.transform.localPosition;
    }
    
    private void Update()
    {
        //m_deltaTime += Time.deltaTime * 5.0f;
        //if (m_currentDistance != m_TargetDistance)
        //{
        //    m_Cam.transform.localPosition = Vector3.Lerp(m_OriginalOffset, m_TargetOffset, m_deltaTime);
        //    m_currentDistance = Mathf.Lerp(m_InitialDistance, m_TargetDistance, m_deltaTime);
        //}
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
        m_RotationX += m_PosCamera.y * m_SensivityY;
        m_RotationY += m_PosCamera.x * m_SensivityX;

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
            m_currentDistance = Mathf.Clamp(hit.distance, minDist, maxDist);
            Debug.Log("je touche quelque chose");
        }
        else
        {
            m_currentDistance = maxDist;
        }
        m_Cam.transform.position = Vector3.Lerp(transform.localPosition, m_OffsetCamera * m_currentDistance, Time.deltaTime * smooth);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.TransformPoint(m_OffsetCamera * maxDist));
    }
}
