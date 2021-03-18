using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_Levier : InteractionMother
{
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
        if(TryGetComponent(out m_Anim))
        {
            PlayerControllerSO.BindInputs(false);
            Levier();
        }
        else
        {
            Debug.Log("je suis un levier mais sans mon aniamtion");
            return;
        }
    }

    public void Levier()
    {
        m_Anim.SetBool("Interact",true);
        m_Collider.enabled = false;
    }

    public void OpenDoor()
    {
        m_DoorOpen.OpenDoor();
        GlobalInteractionRef.UseObject = false;
        PlayerControllerSO.BindInputs(true);
    }
}
