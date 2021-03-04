using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_Button : InteractionMother
{
    private Animator m_Anim = null;
    public override void Use()
    {
        if (TryGetComponent(out m_Anim))
        {
            Debug.Log("coucou");
            m_Anim.SetBool("Push", true);
        }
        else
        {
            Debug.Log("je suis le bouton sans l'animation");
        }
    }
}
