using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class CallAnimEvent : MonoBehaviour
{
    [SerializeField]
    private SO_PlayerController m_PlayerController = null;

    private CharacterController m_PlayerCharacterController = null;

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

    #region Subclass
    [System.Serializable]
    private class MovementEvents
    {
      public MovementInfosEvent m_OnMove = new MovementInfosEvent();
      public UnityEvent m_OnStopMove = new UnityEvent();
    }
    [SerializeField]
    private MovementEvents m_MovementEvent = new MovementEvents();

    #endregion
   
    public void OnMovement()
    {
      m_MovementEvent.m_OnMove.Invoke(new MovementInfo { entity = this.gameObject, currentPosition = transform.position, orientation = transform.position });
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
    public void EquipArm()
    {
        Collider[] l_Collide = Physics.OverlapSphere(transform.position, 5f, m_LayerDetection);
        foreach (var item in l_Collide)
        {
            if(m_InteractArmPacket == null)
            {
                m_InteractArmPacket = item.GetComponentInChildren<Interaction_ArmPacket>();
                m_InteractArmPacket.EquipeArm();
            }
        }
    }
    public GameObject Arm { get { return m_Arm; } }
}
