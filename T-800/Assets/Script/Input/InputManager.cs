using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset m_InputManage = null;

    private Movements m_Movements = null;



    Vector2 m_PosCamera = Vector2.zero;

    private void Awake()
    {
        m_Movements = FindObjectOfType<Movements>();
        InputActionMap playerMap = m_InputManage.FindActionMap("Player");
        InputAction rotationCamera = m_InputManage.FindAction("Look");
        rotationCamera.performed += (ctx) => { m_PosCamera = ctx.ReadValue<Vector2>(); };
        rotationCamera.canceled += (ctx) => { m_PosCamera = Vector2.zero; };

        InputAction moveAction = playerMap.FindAction("Movements");
        moveAction.performed += (ctx) => { m_Movements.movementInput = ctx.ReadValue<Vector2>();  };
        moveAction.canceled += (ctx) => { m_Movements.movementInput = Vector2.zero; };

        InputAction jumpAction = playerMap.FindAction("Jump");
        jumpAction.performed += (ctx) => { m_Movements.Jumping(); } ;
        jumpAction.canceled += (ctx) => {  };
    }

    private void Update()
    {
        m_Movements.Move(m_Movements.movementInput, Time.deltaTime);

        //if (m_Movements.isJumping)
        //{
        //    m_Movements.Jumping();
        //}
       
    }
    private void OnEnable()
    {
        m_InputManage.Enable();
    }

    private void OnDisable()
    {
        
    }

    public Vector3 CameraPos
    {
        get { return m_PosCamera; }
    }
}
//m_ForwardSpeed += m_Acceleration * Time.deltaTime;
//m_ForwardSpeed = Mathf.Clamp(m_ForwardSpeed, 0f, m_MaxSpeed);


