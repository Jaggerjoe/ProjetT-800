using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractionMother : MonoBehaviour
{
    private GameObject m_MyParents = null;
    private Global_Interaction m_PlayerInteraction = null;
    private CharacterController m_PlayerCharacterController = null;
    private SO_PlayerController m_SOCharacterController = null;
    public virtual void Use()
    {

    }

    public virtual void StopUse() { }

    public Global_Interaction GlobalInteractionRef
    {
        get { return m_PlayerInteraction; }
        set { m_PlayerInteraction = value; }
    }

    public CharacterController CharacterController
    {
        get { return m_PlayerCharacterController; }
        set { m_PlayerCharacterController = value; }
    }

    public SO_PlayerController PlayerControllerSO
    {
        get { return m_SOCharacterController; }
        set { m_SOCharacterController = value; }
    }
}
