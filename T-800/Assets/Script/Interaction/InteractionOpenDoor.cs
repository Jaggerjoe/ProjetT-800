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
        //OpenDoor();
    }

    public override void Use()
    {
        //OpenDoor();
    }

    public void OpenDoor()
    {
        switch(m_HowManyInteract)
        {
            case HowManyInteract.One:
                OneInteraction();
                break;
            case HowManyInteract.Two:
                TwoInteraction();
                break;
            default:
                Debug.Log("je n'est rien");
                break;

        }
        //if (m_HowManyInteract == HowManyInteract.One)
        //{
        //    if (m_LeverInteract.IsLevered)
        //    {
        //        m_Anim.SetBool("Open", true);
        //    }
        //}

        //if (m_HowManyInteract == HowManyInteract.Two)
        //{
        //    Debug.Log("coucou");
        //    if (m_LeverInteract.IsLevered && m_ButtonInteract.IsButtoned)
        //    {
        //        m_Anim.SetBool("Open", true);
        //    }
        //}
    }

    void OneInteraction()
    {
        if (m_LeverInteract.IsLevered)
        {
            m_Anim.SetBool("Open", true);
        }
    }
    void TwoInteraction()
    {
        Debug.Log("coucou");
        if (m_LeverInteract.IsLevered && m_ButtonInteract.IsButtoned || m_ButtonInteract.IsButtoned && m_LeverInteract.IsLevered)
        {
            m_Anim.SetBool("Open", true);
        }
    }

}

public enum HowManyInteract
{
    One,
    Two,
    Default
}

