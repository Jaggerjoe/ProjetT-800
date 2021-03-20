using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractionMother : MonoBehaviour
{
    [SerializeField]
    protected LayerMask m_LayerDetection;
    private GameObject m_MyParents = null;
    private Global_Interaction m_PlayerInteraction = null;
    private CharacterController m_PlayerCharacterController = null;
    private SO_PlayerController m_SOCharacterController = null;
    protected Animator m_AnimPlayer = null;
    protected float m_Speed = 0;
    public virtual void Start() 
    {
        m_PlayerCharacterController = FindObjectOfType<CharacterController>();
        m_PlayerInteraction = FindObjectOfType<Global_Interaction>(); 
        m_Speed = CharacterController.Speed;
    }
    public virtual void Use()
    {

    }

    public virtual void RecuperationAniamtorOnPlayer()
    {
        Collider[] l_Collide = Physics.OverlapSphere(transform.position, 10f, m_LayerDetection);
        foreach (var item in l_Collide)
        {
            m_AnimPlayer = item.GetComponentInChildren<Animator>();
        }
    }

    public virtual void StopUse() {
        m_SOCharacterController.InputAsset.FindAction("Player/Jump").Enable();
    }

    public Global_Interaction GlobalInteractionRef
    {
        get { return m_PlayerInteraction; }
        set { m_PlayerInteraction = value; }
    }

    public CharacterController CharacterController
    {
        get { return m_PlayerCharacterController; }
        set { m_PlayerCharacterController = value; }
    }

    public SO_PlayerController PlayerControllerSO
    {
        get { return m_SOCharacterController; }
        set { m_SOCharacterController = value; }
    }
}
