using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_Box : InteractionMother
{
    private Animator m_Anim;

    public override void Start()
    {
        base.Start();
    }
    public override void Use()
    {
        Debug.Log("je compile ou pas");
        // if(CharacterController == null)
        // {
        //     Debug.Log("je passe ici");
        //     CharacterController = FindObjectOfType<CharacterController>();
        // }
        if (TryGetComponent(out m_Anim))
        {
            m_Anim.SetTrigger("Interact");
        }
        else
        {
            Debug.Log("je suis une caisse mais sans mon aniamtion");
            transform.parent = GlobalInteractionRef.transform;
            CharacterController.Speed = m_Speed/2;
            return;
        }
    }

    public override void StopUse()
    {
        if (GlobalInteractionRef.UseObject)
        {
            GlobalInteractionRef.UseObject = false;
            transform.parent = null;
            CharacterController.Speed = m_Speed;
        }
    }
}
