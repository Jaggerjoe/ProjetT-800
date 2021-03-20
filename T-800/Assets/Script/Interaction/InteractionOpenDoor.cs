using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionOpenDoor : InteractionMother
{
    private Animator m_Anim = null;

    [SerializeField]
    private Interaction_Levier m_LeverInteract;

    [SerializeField]
    private Interaction_Button m_ButtonInteract;

    [SerializeField]
    HowManyInteract m_HowManyInteract;


    public override void Start()
    {
        base.Start();
        m_Anim = GetComponent<Animator>();
    }

    public override void Use()
    {
        OpenDoor();
    }
    public void OpenDoor()
    {
        if (m_HowManyInteract == HowManyInteract.One)
        {
            if (m_LeverInteract.IsLevered)
            {
                m_Anim.SetBool("Open", true);
            }
        }
        else
        if (m_HowManyInteract == HowManyInteract.Two)
        {
            if (m_LeverInteract.IsLevered  )
            {

                if (m_ButtonInteract.IsButtoned)
                {
                    Debug.Log("ok");
                    m_Anim.SetBool("Open", true);
                }
            }
        }
        
    }
}
public enum HowManyInteract
{
    One,
    Two
}

