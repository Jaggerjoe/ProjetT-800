using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionOpenDoor : InteractionMother
{
    private Animator m_Anim = null;
 
    private void Start()
    {
        m_Anim = GetComponent<Animator>();
    }

    public override void Use()
    {
        OpenDoor();
    }
    public void OpenDoor()
    {
        m_Anim.SetBool("Open", true);
    }
}
