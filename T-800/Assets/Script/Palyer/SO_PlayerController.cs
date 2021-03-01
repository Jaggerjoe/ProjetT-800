using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "NewPlayerController", menuName = "GAME/Player Controller")]
public class SO_PlayerController : ScriptableObject
{
    [SerializeField]
    private InputActionAsset m_InputAsset = null;

    private Vector2 m_MoveVector = Vector2.zero;

    private Vector2 m_PosCamera = Vector2.zero;

    private bool m_IsAiming = false;
    private bool m_IsGrabAndThrow = false;
    private bool m_IsJumping = false;
    private bool m_IsInteract = false;
    private VoidEvent m_OnJump = new VoidEvent();
    private VoidEvent m_OnInteract = new VoidEvent();

    private void OnEnable()
    {
        BindInputs(true);
    }

    private void OnDisable()
    {
        BindInputs(false);
    }

    private void BindInputs(bool p_AreEnabled)
    {
        if (m_InputAsset == null)
        {
            return;
        }
        if (p_AreEnabled)
        {
            m_InputAsset.FindAction("Player/Movements").performed += Move;
            m_InputAsset.FindAction("Player/Movements").canceled += Move;

            m_InputAsset.FindAction("Player/Jump").performed += Jump;
            m_InputAsset.FindAction("Player/Jump").canceled += StopJump;

            m_InputAsset.FindAction("Player/Look").performed += RotationCamera;
            m_InputAsset.FindAction("Player/Look").canceled += RotationCamera;

            m_InputAsset.FindAction("Player/Interaction").started += Interaction;
            m_InputAsset.FindAction("Player/Interaction").canceled += StopInteraction;

            m_InputAsset.FindAction("Player/Aiming").performed += Aim;
            m_InputAsset.FindAction("Player/Aiming").canceled += StopAim;

            m_InputAsset.FindAction("Player/GrabAndThrow").performed += GAT;
            m_InputAsset.FindAction("Player/GrabAndThrow").canceled += StopGAT;


            m_InputAsset.Enable();
        }
        else
        {
            m_InputAsset.FindAction("Player/Movements").performed -= Move;
            m_InputAsset.FindAction("Player/Movements").canceled -= Move;

            m_InputAsset.FindAction("Player/Jump").performed -= Jump;
            m_InputAsset.FindAction("Player/Jump").canceled -= StopJump;

            m_InputAsset.FindAction("Player/Look").performed -= RotationCamera;
            m_InputAsset.FindAction("Player/Look").canceled -= RotationCamera;

            m_InputAsset.FindAction("Player/Interaction").started -= Interaction;
            m_InputAsset.FindAction("Player/Interaction").canceled -= StopInteraction;

            m_InputAsset.FindAction("Player/Aiming").performed -= Aim;
            m_InputAsset.FindAction("Player/Aiming").canceled -= StopAim;

            m_InputAsset.FindAction("Player/GrabAndThrow").performed -= GAT;
            m_InputAsset.FindAction("Player/GrabAndThrow").canceled -= StopGAT;



            m_InputAsset.Disable();
        }
        Debug.Log("l'etat de mon input est : " + p_AreEnabled);
    }

    private void Interaction(InputAction.CallbackContext p_Context)
    {
        m_OnInteract.Invoke();
        if(!m_IsInteract)
        {
            m_IsInteract = true;
        }
    }

    private void StopInteraction(InputAction.CallbackContext p_Context)
    {
        m_IsInteract = false;

    }
    private void RotationCamera(InputAction.CallbackContext p_Context)
    {
        m_PosCamera = p_Context.ReadValue<Vector2>();
    }
    private void Move(InputAction.CallbackContext p_Context)
    {
        m_MoveVector = p_Context.ReadValue<Vector2>();
        m_MoveVector = Vector3.ClampMagnitude(m_MoveVector, 1f);
    }

    private void Jump(InputAction.CallbackContext p_Context)
    {
        if (!m_IsJumping)
        {
            //m_OnJump.Invoke();
            m_IsJumping = true;
        }
    }




    private void StopJump(InputAction.CallbackContext p_Context)
    {
        m_IsJumping = false;
    }
    private void GAT(InputAction.CallbackContext p_Context)
    {
        if (!m_IsGrabAndThrow)
        {

           m_IsGrabAndThrow = true;
        }
    }
    private void StopGAT(InputAction.CallbackContext p_Context)
    {
        m_IsGrabAndThrow = false;
    }
    private void Aim(InputAction.CallbackContext p_Context)
    {
        if (!m_IsAiming)
        {
           
            m_IsAiming = true;
        }
    }
    private void StopAim(InputAction.CallbackContext p_Context)
    {
        m_IsAiming = false;
    }

    public VoidEvent onJump => m_OnJump;
    public bool Jumping => m_IsJumping;
    public bool Interact => m_IsInteract;
    public Vector2 MoveVector => m_MoveVector;
    public Vector2 RotationVector => m_PosCamera;
    public VoidEvent InteractionObj => m_OnInteract;
    public bool Aiming => m_IsAiming;

    public bool GrabAndThrow => m_IsGrabAndThrow;
}

   

