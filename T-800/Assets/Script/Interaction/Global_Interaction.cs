using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global_Interaction : InteractionMother
{
    [SerializeField]
    private LayerMask m_Layer;

    [SerializeField]
    private float m_Radius = 2;

    [SerializeField]
    private float m_Angle = 0;

    [SerializeField]
    private IntercationBodyPlayer m_RefInteraction;

    [SerializeField]
    private SO_PlayerController m_PLayerController;

    private InteractionMother m_InteractObj;

    private void OnEnable()
    {
        m_PLayerController.InteractionObj.AddListener(DetectionInteraction);
    }

    private void OnDisable()
    {
        m_PLayerController.InteractionObj.RemoveListener(DetectionInteraction);
    }

    public void DetectionInteraction()
    {
        Collider[] hitCollier = Physics.OverlapSphere(transform.position, m_Radius, m_Layer);
        foreach (var hit in hitCollier)
        {
            Vector3 toOther = hit.gameObject.transform.position - this.transform.position;
            Debug.DrawRay(transform.position, transform.forward * 20, Color.red);
            if (Vector3.Dot(transform.forward, toOther) > 0)
            {
                if (Vector3.Angle(transform.forward, toOther) <= m_Angle / 2)
                {
                    if(m_RefInteraction.Etat == EtatDuPlayer.DeuxBras)
                    {
                        transform.LookAt(hit.gameObject.transform.position, Vector3.down);
                        if(hit.gameObject.TryGetComponent(out m_InteractObj))
                        {
                            m_InteractObj.Use();
                        }
                    }
                }
            }
        }
    }

    //public void ActionLevier()
    //{
    //    Debug.Log("i'm here");
    //    RaycastHit hit;
    //    Debug.DrawRay(transform.position, transform.forward, Color.cyan);
    //    if (Physics.Raycast(transform.position, transform.forward, out hit, 10, m_Layer))
    //    {
    //        if (m_RefInteraction.Etat == EtatDuPlayer.DeuxBras)
    //        {
    //            if (TryGetComponent(out m_LevierInteract))
    //            {
    //                m_LevierInteract.Levier();
    //            }
    //            else
    //            {
    //                return;
    //            }
    //            Debug.Log("target : " + hit.collider.gameObject.name);
    //        }
    //    }
    //}
}

