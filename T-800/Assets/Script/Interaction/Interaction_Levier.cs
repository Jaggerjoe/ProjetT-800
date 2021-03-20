using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_Levier : InteractionMother
{
    [SerializeField]
    private GameObject m_Levier;

    [SerializeField]
    private InteractionOpenDoor m_DoorOpen;

    private Animator m_AnimLevier;

    private MeshCollider m_Collider = null;

    private bool m_isLevered = false;


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
        m_Anim.SetBool("Interact",true);
        m_isLevered = true;
        Debug.Log("Levered:" + m_isLevered);
        m_Collider.enabled = false;
        m_AnimPlayer.SetTrigger("StartUse");
    }
    public void EndAnimationLevier()
    {
        m_AnimPlayer.SetTrigger("StopUse");
    }

    public void OpenDoor()
    {
        m_DoorOpen.OpenDoor();
        GlobalInteractionRef.UseObject = false;
    }

    public bool IsLevered { get { return m_isLevered; } }
}
