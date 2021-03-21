using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_Button : InteractionMother
{
    //private Animator m_Anim = null;
    [SerializeField]
    private bool m_ButtonIsPressed = false;

    [SerializeField]
    private InteractionOpenDoor m_DoorOpen = null;

    private Animator m_Anim = null;

    private MeshCollider m_Collider = null;

  


    public override void Start()
    {
        base.Start();
        m_Collider = GetComponent<MeshCollider>();
        m_Anim = GetComponent<Animator>();
    }
    private void Update()
    {
        Debug.Log(m_ButtonIsPressed);
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
        Debug.Log(m_ButtonIsPressed);
        m_DoorOpen.OpenDoor();
        GlobalInteractionRef.UseObject = false;
    }

    public bool IsButtoned { get { return m_ButtonIsPressed; } }
}
