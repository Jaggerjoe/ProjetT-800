using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_Box : InteractionMother
{
    public override void Start()
    {
        base.Start();
    }

    public override void RecuperationAniamtorOnPlayer()
    {
        base.RecuperationAniamtorOnPlayer();
        
    }
    public override void Use()
    {
        if(m_AnimPlayer == null)
        {
            RecuperationAniamtorOnPlayer();
            transform.parent = GlobalInteractionRef.transform;
            CharacterController.Speed = m_Speed / 2;
            m_AnimPlayer.SetTrigger("StartTakeBox");
        }
        else
        {
            transform.parent = GlobalInteractionRef.transform;
            CharacterController.Speed = m_Speed / 2;
            m_AnimPlayer.SetTrigger("StartTakeBox");
        }
    }

    public override void StopUse()
    {
        base.StopUse();
        if (GlobalInteractionRef.UseObject)
        {
            GlobalInteractionRef.UseObject = false;
            transform.parent = null;
            CharacterController.Speed = m_Speed;
            m_AnimPlayer.SetTrigger("StopTakeBox");
        }
    }
}
