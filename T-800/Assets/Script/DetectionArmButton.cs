using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class DetectionArmButton : MonoBehaviour
{
    [SerializeField]
    private Interaction_Button m_ButtonInteraction;
    [SerializeField]
    private CapsuleCollider m_CapsuleCollider = null;
    [SerializeField]
    private float m_CastDistance = 0;
    [SerializeField]
    private LayerMask m_ButtonDetection;

    
    void Update()
    {
       

        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, m_CastDistance, m_ButtonDetection))
        {
            
        
            m_ButtonInteraction.Use();
            m_CapsuleCollider.gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        

        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * m_CastDistance);
    }


  

}
