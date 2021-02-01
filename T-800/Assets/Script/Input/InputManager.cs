using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset m_InputManage = null;

    private Movements m_Movements = null;

    private Interaction m_Interaction = null;



    Vector2 m_PosCamera = Vector2.zero;

    private void Awake()
    {
        m_Movements = FindObjectOfType<Movements>();
        m_Interaction = FindObjectOfType<Interaction>();

        InputActionMap playerMap = m_InputManage.FindActionMap("Player");
        InputAction rotationCamera = m_InputManage.FindAction("Look");
        rotationCamera.performed += (ctx) => { m_PosCamera = ctx.ReadValue<Vector2>(); };
        rotationCamera.canceled += (ctx) => { m_PosCamera = Vector2.zero; };

        InputAction moveAction = playerMap.FindAction("Movements");
        moveAction.performed += (ctx) => { m_Movements.movementInput = ctx.ReadValue<Vector2>();  };
        moveAction.canceled += (ctx) => { m_Movements.movementInput = Vector2.zero; };

        InputAction interactinAction = playerMap.FindAction("Interaction");
        interactinAction.started += (ctx) => m_Interaction.ActionLevier();
    }

    private void Update()
    {
        m_Movements.Move(m_Movements.movementInput, Time.deltaTime);

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


