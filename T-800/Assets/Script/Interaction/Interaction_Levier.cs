using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_Levier : InteractionMother
{
    [SerializeField]
    private GameObject m_Levier;
    private Animator m_Anim;
    private void Start()
    {
 
    }
    // Start is called before the first frame update

    public override void Use()
    {
        if(TryGetComponent(out m_Anim))
        {
            m_Anim.SetTrigger("Interact");
        }
        else
        {
            Debug.Log("je suis un levier mais sans mon aniamtion");
            return;
        }
    }
    public void Levier()
    {
        m_Anim.SetTrigger("Interact");
    }
}
