using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InteractionOpenDoor : InteractionMother
{
    private Animator m_Anim = null;

    [SerializeField]
    private Interaction_Levier m_LeverInteract;

    [SerializeField]
    private Interaction_Button m_ButtonInteract;

    [SerializeField]
    HowManyInteract m_HowManyInteract;

    [System.Serializable]
    private class DoorEvent
    {
        public DoorInfosEvent m_OpenDoorEvent = new DoorInfosEvent();
    }

    [SerializeField]
    private DoorEvent m_DoorEvent = new DoorEvent();

    public override void Start()
    {
        base.Start();
        m_Anim = GetComponent<Animator>();
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
    }

    void OneInteraction()
    {
        if (m_LeverInteract.IsLevered)
        {
            m_Anim.SetBool("Open", true);
            m_DoorEvent.m_OpenDoorEvent.Invoke(new DoorInfos { Origin = transform.position, Rotation = transform.parent.rotation});
        }
    }
    void TwoInteraction()
    {
        Debug.Log("coucou");
        if (m_LeverInteract.IsLevered && m_ButtonInteract.IsButtoned || m_ButtonInteract.IsButtoned && m_LeverInteract.IsLevered)
        {
            m_Anim.SetBool("Open", true);
            m_DoorEvent.m_OpenDoorEvent.Invoke(new DoorInfos { Origin = transform.position, Rotation = transform.parent.rotation });
        }
    }

}

public enum HowManyInteract
{
    One,
    Two,
    Default
}

