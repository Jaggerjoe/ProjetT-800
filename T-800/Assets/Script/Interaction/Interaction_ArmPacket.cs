using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_ArmPacket : InteractionMother
{
    [SerializeField]
    private GameObject m_PrefabArm;

    [SerializeField]
    private LayerMask m_LayerDetection;
    private GameObject m_Socket;
    private Animator m_Anim = null;
    public override void Use()
    {
        if(TryGetComponent(out m_Anim))
        {
            //Add animation
        }
        else
        {
            Debug.Log("Je suis un tas de bras sans animation");
            EquipeArm();
        }
    }

    private void EquipeArm()
    {
        Collider[] l_HitColliders = Physics.OverlapSphere(transform.position, 10, m_LayerDetection);
        foreach (var hitCollider in l_HitColliders)
        {
            if(TryGetComponent(out m_Socket))
            {
                Instantiate(m_PrefabArm, hitCollider.transform.position, Quaternion.identity, m_Socket.transform.parent);
            }
        }
    }
}
