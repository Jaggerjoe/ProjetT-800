using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_ArmPacket : InteractionMother
{
    [SerializeField]
    private GameObject m_PrefabArm;

    [SerializeField]
    private GameObject m_SkeletArm;

    //[SerializeField]
    //private GameObject m_Socket;

    public override void RecuperationAniamtorOnPlayer()
    {
        base.RecuperationAniamtorOnPlayer();
    }

    public override void Use()
    {
        GlobalInteractionRef.UseObject = false;
    }

    public override void UseWithOneArm()
    {
        if(m_AnimPlayer == null)
        {
            RecuperationAniamtorOnPlayer();
            m_AnimPlayer.SetTrigger("StartUsePackArm");
        }
        else
        {
            m_AnimPlayer.SetTrigger("StartUsePackArm");
        }
    }

    public void EquipeArm()
    {
        m_SkeletArm.SetActive(true);
    }
}
