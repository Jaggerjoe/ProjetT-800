using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    [SerializeField]
    private LayerMask m_Layer;

    [SerializeField]
    private float m_Radius = 2;

    [SerializeField]
    private float m_Angle = 0;

    [SerializeField]
    private IntercationBodyPlayer m_RefInteraction;

    private IT_Levier m_LevierInteract;
    [SerializeField]
    private SO_PlayerController m_PLayerController;
    private void Update()
    {
        Action();
    }
    public void Action()
    {
        if(m_PLayerController.Interact)
        {
            Collider[] hitCollier = Physics.OverlapSphere(transform.position, m_Radius, m_Layer);
            foreach (var hit in hitCollier)
            {
                Debug.Log("target : " + hit.gameObject.name);
                Vector3 toOther = hit.gameObject.transform.position - transform.position;
                Debug.DrawRay(transform.position, transform.forward * 20, Color.red);
                if (Vector3.Dot(transform.forward, toOther) > 0)
                {
                    if (Vector3.Angle(transform.forward, toOther) <= m_Angle / 2)
                    {
                        transform.LookAt(hit.gameObject.transform.position, Vector3.down);
                        ActionLevier();
                    }
                }
            }
        }
    }

    public void ActionLevier()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward, Color.cyan);
        if (Physics.Raycast(transform.position, transform.forward, out hit, 10, m_Layer))
        {
            if (m_RefInteraction.Etat == EtatDuPlayer.DeuxBras)
            {
                if(TryGetComponent<IT_Levier>(out m_LevierInteract))
                {
                    return;
                }
                else
                {
                    m_LevierInteract = hit.collider.gameObject.GetComponent<IT_Levier>();
                    m_LevierInteract.Levier();
                }
            }
        }
    }
}

