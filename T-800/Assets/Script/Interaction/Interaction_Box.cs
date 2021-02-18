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
            return;
        }
    }
}
