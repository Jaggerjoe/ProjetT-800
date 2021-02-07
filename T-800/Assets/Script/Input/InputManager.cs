using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset m_InputManage = null;

    [SerializeField]
    private Movements m_Movements = null;

    [SerializeField]
    private Interaction m_Interaction = null;

    [SerializeField]
    private TPSScript m_RefCamera = null;

    Vector2 m_PosCamera = Vector2.zero;

    Vector2 m_Movement = Vector2.zero;

    private void Awake()
    {
        InputActionMap playerMap = m_InputManage.FindActionMap("Player");

        InputAction rotationCamera = m_InputManage.FindAction("Look");
        rotationCamera.performed += (ctx) => { m_PosCamera = ctx.ReadValue<Vector2>(); };
        rotationCamera.canceled += (ctx) => { m_PosCamera = Vector2.zero; };

        InputAction moveAction = playerMap.FindAction("Movements");
        moveAction.performed += (ctx) => { m_Movement = ctx.ReadValue<Vector2>(); };
        moveAction.canceled += (ctx) => { m_Movement = Vector2.zero; };

        InputAction interactinAction = playerMap.FindAction("Interaction");
        interactinAction.started += (ctx) => m_Interaction.Action();

        InputAction jumpAction = playerMap.FindAction("Jump");
        jumpAction.performed += (ctx) => { m_Movements.Jumpbool = true; };
        jumpAction.canceled += (ctx) => { m_Movements.Jumpbool = false; };
    }

    private void Update()
    {
        m_Movements.Move(m_Movement, Time.deltaTime);
        m_RefCamera.RotationCamera(m_PosCamera);
        
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


