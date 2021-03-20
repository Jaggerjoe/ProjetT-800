using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_ArmPacket : InteractionMother
{
    [SerializeField]
    private GameObject m_PrefabArm;

    private GameObject m_Socket;

    public override void RecuperationAniamtorOnPlayer()
    {
        base.RecuperationAniamtorOnPlayer();
    }

    public override void Use()
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
        m_AnimPlayer.SetTrigger("StopUsePackArm");
        GlobalInteractionRef.UseObject = false;

        Collider[] l_HitColliders = Physics.OverlapSphere(transform.position, 10, m_LayerDetection);
        foreach (var hitCollider in l_HitColliders)
        {
            if(TryGetComponent(out m_Socket))
            {
                Instantiate(m_PrefabArm, hitCollider.transform.position, Quaternion.identity, m_Socket.transform.parent);
            }
        }
    }
}
