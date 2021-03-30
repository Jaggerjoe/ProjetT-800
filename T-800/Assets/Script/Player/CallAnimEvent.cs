using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class CallAnimEvent : MonoBehaviour
{
    [SerializeField]
    private SO_PlayerController m_PlayerController = null;

    [SerializeField]
    private LayerMask m_LayerDetection = 0;

    Interaction_ArmPacket m_InteractArmPacket = null;

    [SerializeField]
    private GameObject m_SkeletArm;

    [SerializeField]
    private GameObject m_ArmPrefab;

    [SerializeField]
    private Transform m_ArmPos;

    private GameObject m_Arm;

    private Animator m_Anim = null;

    private IntercationBodyPlayer m_EtatPlayer = null;

    private Global_Interaction m_GlobalInteractPlayer = null;


    #region Subclass
    [System.Serializable]
    private class MovementEvents
    {
      public MovementInfosEvent m_OnMoveFootRight = new MovementInfosEvent();
      public MovementInfosEvent m_OnMoveFootLeft = new MovementInfosEvent();
      public UnityEvent m_OnStopMove = new UnityEvent();
    }
    [SerializeField]
    private MovementEvents m_MovementEvent = new MovementEvents();

    [System.Serializable]
    private class JumpEvents
    {
        public JumpInfoEvent m_OnStopJump = new JumpInfoEvent();
    }
    [SerializeField]
    JumpEvents m_JumpEvent = new JumpEvents();

    [System.Serializable]
    private class SnatchArmEvent
    {
        public UnityEvent m_OnSnatchArm = new UnityEvent();

        public UnityEvent m_OnSnatchArmFx = new UnityEvent();
    }
    [SerializeField]
    private SnatchArmEvent m_SntachEvent = new SnatchArmEvent();

    [System.Serializable]
    private class SeachInArmPack
    {
        public UnityEvent m_OnSearch = new UnityEvent();
    }
    [SerializeField]
    private SeachInArmPack m_SearchInArmPacket = new SeachInArmPack();
    #endregion
    private void Start()
    {
        m_GlobalInteractPlayer = GetComponentInParent<Global_Interaction>();
        m_EtatPlayer = GetComponentInParent<IntercationBodyPlayer>();
        m_Anim = GetComponent<Animator>();
    }
    public void OnMovementFootRight()
    {
        m_MovementEvent.m_OnMoveFootRight.Invoke(new MovementInfo { entity = this.gameObject, currentPosition = transform.position, orientation = transform.position });
    }

    public void OnMovementFootLeft()
    {
        m_MovementEvent.m_OnMoveFootLeft.Invoke(new MovementInfo { entity = this.gameObject, currentPosition = transform.position, orientation = transform.position });
    }

    public void SetArmThrow()
    {
        m_SkeletArm.SetActive(false);
        m_Arm = Instantiate(m_ArmPrefab, m_ArmPos);
    }

    public void FreezeMovement()
    {
        m_PlayerController.InputAsset.FindAction("Player/Movements").Disable();
    }

    public void StartMovement()
    {
        m_PlayerController.InputAsset.FindAction("Player/Movements").Enable();
    }

    public void RemoveJump()
    {
        m_PlayerController.InputAsset.FindAction("Player/Jump").Disable();
    }

    public void AddJump()
    {
        m_PlayerController.InputAsset.FindAction("Player/Jump").Enable();
    }

    public void StopJump()
    {
        m_JumpEvent.m_OnStopJump.Invoke(new JumpInfo { JumpOrigin = transform.position +new Vector3(0,.3f,0) });
    }

    public void SnatchArm()
    {
        m_SntachEvent.m_OnSnatchArm.Invoke();
    }

    public void SnatchArmFX()
    {
        m_SntachEvent.m_OnSnatchArmFx.Invoke();
    }
    public void SearchInArmPacket()
    {
        m_SearchInArmPacket.m_OnSearch.Invoke();
    }
    public void EquipArm()
    {
        Collider[] l_Collide = Physics.OverlapSphere(transform.position, 5f, m_LayerDetection);
        foreach (var item in l_Collide)
        {
            if(m_InteractArmPacket == null)
            {
                m_InteractArmPacket = item.GetComponentInChildren<Interaction_ArmPacket>();
                m_InteractArmPacket.EquipeArm();
                m_Anim.SetTrigger("StopUsePackArm");
                m_GlobalInteractPlayer.UseObject = false;
                m_EtatPlayer.Etat = EtatDuPlayer.DeuxBras;
            }
            else
            {
                m_InteractArmPacket.EquipeArm();
                m_Anim.SetTrigger("StopUsePackArm");
                m_GlobalInteractPlayer.UseObject = false;
                m_EtatPlayer.Etat = EtatDuPlayer.DeuxBras;
            }
        }
    }



    public GameObject Arm { get { return m_Arm; } }
}
