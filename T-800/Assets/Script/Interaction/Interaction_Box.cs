using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_Box : InteractionMother
{
    private Animator m_Anim;

    public override void Use()
    {
        if (TryGetComponent(out m_Anim))
        {
            m_Anim.SetTrigger("Interact");
        }
        else
        {
            Debug.Log("je suis une caisse mais sans mon aniamtion");
            transform.parent = GlobalInteractionRef.transform;
            CharacterController.Speed = 5f;
            return;
        }
    }

    public override void StopUse()
    {
        if (GlobalInteractionRef.UseObject)
        {
            GlobalInteractionRef.UseObject = false;
            transform.parent = null;
            CharacterController.Speed = 12;
        }
    }
}
