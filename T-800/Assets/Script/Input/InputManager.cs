using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset m_InputManage;
    Vector2 m_PosCamera = Vector2.zero;

    private void Awake()
    {
        InputActionMap playerMap = m_InputManage.FindActionMap("Player");
        InputAction rotationCamera = m_InputManage.FindAction("Look");
        rotationCamera.performed += (ctx) => { m_PosCamera = ctx.ReadValue<Vector2>(); };
        rotationCamera.canceled += (ctx) => { m_PosCamera = Vector2.zero; };
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
