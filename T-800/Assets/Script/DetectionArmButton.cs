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
        //    if(Physics.CapsuleCast(PointStartCapsule,PointEndCapsule,m_CapsuleCollider.radius,Vector3.up, out RaycastHit hit,m_CastDistance,m_ButtonDetection))
        //    {
        //        Debug.Log("oukoulew");
        //        m_ButtonInteraction.Use();

        //    }

        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, m_CastDistance, m_ButtonDetection))
        {
            Debug.Log("oukoulew");
            m_ButtonInteraction.Use();
            m_CapsuleCollider.gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawWireSphere(PointStartCapsule, m_CapsuleCollider.radius);
        ////Gizmos.color = Color.green;
        //Gizmos.DrawWireSphere(PointEndCapsule, m_CapsuleCollider.radius);

        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * m_CastDistance);
    }

    private float DistanceBetweenTheStartSphereAndTheEndSphere
    {
        get
        {
            return m_CapsuleCollider.height / 2 - m_CapsuleCollider.radius;
        }
    }
    private Vector3 PointStartCapsule
    {
        get
        {
            return transform.position + m_CapsuleCollider.center + Vector3.forward * DistanceBetweenTheStartSphereAndTheEndSphere;
        }
    }

    private Vector3 PointEndCapsule
    {
        get
        {
            return transform.position + m_CapsuleCollider.center - Vector3.forward * DistanceBetweenTheStartSphereAndTheEndSphere;
        }
    }
}
