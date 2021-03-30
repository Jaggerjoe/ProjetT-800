using UnityEngine;
using UnityEngine.Events;

public class Interaction_Button : InteractionMother
{
    //private Animator m_Anim = null;
    [SerializeField]
    private bool m_ButtonIsPressed = false;

    [SerializeField]
    private InteractionOpenDoor m_DoorOpen = null;

    private Animator m_Anim = null;

    private MeshCollider m_Collider = null;

    [System.Serializable]
    private class ButtonEvent
    {
        public UnityEvent m_OnPressButton = new UnityEvent();
    }

    [SerializeField]
    private ButtonEvent m_ButtonEvent = new ButtonEvent();

    public override void Start()
    {
        base.Start();
        m_Collider = GetComponent<MeshCollider>();
        m_Anim = GetComponent<Animator>();
    }
    private void Update()
    {
    }
    public override void Use()
    {
        Button();
    }

    public void Button()
    {
        m_ButtonIsPressed = true;
        m_Anim.SetBool("Push", true);
        m_Collider.enabled = false;
    }

    public void OpenDoor()
    {
        m_DoorOpen.OpenDoor();
        GlobalInteractionRef.UseObject = false;
        m_ButtonEvent.m_OnPressButton.Invoke();
    }

    public bool IsButtoned { get { return m_ButtonIsPressed; } }
}
