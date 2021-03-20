using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_Button : InteractionMother
{
    //private Animator m_Anim = null;
    private bool m_IsButtoned = false;
   
    [SerializeField]
    private GameObject m_Levier;

    [SerializeField]
    private InteractionOpenDoor m_DoorOpen;

    private Animator m_Anim;

    private MeshCollider m_Collider = null;

  


    public override void Start()
    {
        base.Start();
        m_Collider = GetComponent<MeshCollider>();
    }
    public override void Use()
    {
        Button();
        //if (TryGetComponent(out m_Anim))
        //{
        //    PlayerControllerSO.BindInputs(false);

        //}
        //else
        //{
        //    Debug.Log("je suis un levier mais sans mon aniamtion");
        //    return;
        //}
    }

    public void Button()
    {
        m_IsButtoned = true;
        Debug.Log("Levered:" + m_IsButtoned);
        m_Anim.SetBool("Interact", true);
        m_Collider.enabled = false;
    }

    public void OpenDoor()
    {
        m_DoorOpen.OpenDoor();
        GlobalInteractionRef.UseObject = false;
        PlayerControllerSO.BindInputs(true);
    }

    public bool IsButton { get { return m_IsButtoned; } }
    //[SerializeField]
    //private InteractionOpenDoor m_DoorOpen;

    //public override void Use()
    //{
    //    m_DoorOpen.OpenDoor();
    //    m_IsButtoned = true;
    //    Debug.Log("buttoned :" + m_IsButtoned);

    //    //if (TryGetComponent(out m_Anim))
    //    //{
    //    //    Debug.Log("coucou");
    //    //    m_Anim.SetBool("Push", true);
    //    //}
    //    //else
    //    //{


    //    //    Debug.Log("je suis le bouton sans l'animation");
    ////}


    //}

    public bool IsButtoned { get { return m_IsButtoned; } }
}
