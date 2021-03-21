﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_ArmPacket : InteractionMother
{
    [SerializeField]
    private GameObject m_PrefabArm;

    [SerializeField]
    private GameObject m_SkeletArm;

    [SerializeField]
    private IntercationBodyPlayer m_Etat;

    [SerializeField]
    private GameObject m_Socket;

    public override void RecuperationAniamtorOnPlayer()
    {
        base.RecuperationAniamtorOnPlayer();
    }

    public override void Use()
    {
        Debug.Log("ArmPack");
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
        if (m_Etat.Etat == EtatDuPlayer.UnBras)
        {
            m_SkeletArm.SetActive(true);
        }

        //Collider[] l_HitColliders = Physics.OverlapSphere(transform.position, 10, m_LayerDetection);
        //foreach (var hitCollider in l_HitColliders)
        //{
        //    if(TryGetComponent(out m_Socket))
        //    {
        //        Instantiate(m_PrefabArm, hitCollider.transform.position, Quaternion.identity, m_Socket.transform.parent);
        //    }
        //}
    }
}
