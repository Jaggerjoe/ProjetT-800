using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interaction_Levier : InteractionMother
{
    #region SeriazeField
    [SerializeField]
    private GameObject m_Levier;

    [SerializeField]
    private InteractionOpenDoor m_DoorOpen;

    [SerializeField]
    private bool m_LeverIsActive = false;
    #endregion
    private Animator m_AnimLevier;

    private MeshCollider m_Collider = null;

    [System.Serializable]
    private class LeverEvent
    {
        public LeverInfoEvent m_ActivateLever = new LeverInfoEvent();
    }

    [SerializeField]
    private LeverEvent m_LeverEvent = new LeverEvent();

    public override void Start() 
    {
        base.Start();
        m_Collider = GetComponent<MeshCollider>();
    }

    public override void RecuperationAniamtorOnPlayer()
    {
        base.RecuperationAniamtorOnPlayer();
    }
    public override void Use()
    {
        if(TryGetComponent(out m_AnimLevier))
        {
            Levier();
        }
        else
        {
            Debug.Log("je suis un levier mais sans mon aniamtion");
            return;
        }
    }

    public void Levier()
    {
        if (m_AnimPlayer == null)
            RecuperationAniamtorOnPlayer();
        m_LeverIsActive = true;
        m_AnimLevier.SetBool("Interact", true);
        m_Collider.enabled = false;
        m_AnimPlayer.SetTrigger("StartUse");
    }
    public void EndAnimationLevier()
    {
        m_AnimPlayer.SetTrigger("StopUse");
        m_LeverEvent.m_ActivateLever.Invoke( new LeverInfos { Origin = transform.position, OriginRotation = transform.parent.rotation });
    }

    public void OpenDoor()
    {
        m_DoorOpen.OpenDoor();
        GlobalInteractionRef.UseObject = false;
    }

    public bool IsLevered { get { return m_LeverIsActive; } }
}
