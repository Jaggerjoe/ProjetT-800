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
    private void Update()
    {
        OpenDoor();
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
            if (m_LeverInteract.IsLevered)
            {

                if (m_ButtonInteract.IsButtoned)
                {
                    m_Anim.SetBool("Open", true);
                }
            }
        }
    }

    // creation d'un fct qui check les bool si les deux sont valide je lance l'animation.
    void OpenDoorWithDeuxAction()
    {
        if(m_LeverInteract.IsLevered && m_ButtonInteract.IsButtoned)
        {
            m_Anim.SetBool("Open", true);
        }
    }
}
public enum HowManyInteract
{
    One,
    Two
}

