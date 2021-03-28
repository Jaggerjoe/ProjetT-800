using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField]
    private CharacterController m_Controller;
    [SerializeField]
    private CapsuleCollider m_CapsuleCollider = null;
    [SerializeField]
    private float m_CastDistance = 0;
    [SerializeField]
    private LayerMask m_RespawnDetection;

    private void Update()
    {
        if (Physics.CapsuleCast(PointStartCapsule, PointEndCapsule, m_CapsuleCollider.radius * 0.95f, Vector3.up, out RaycastHit hit, m_CastDistance, m_RespawnDetection))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
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
            return transform.position + m_CapsuleCollider.center + Vector3.up * DistanceBetweenTheStartSphereAndTheEndSphere;
        }
    }

    private Vector3 PointEndCapsule
    {
        get
        {
            return transform.position + m_CapsuleCollider.center - Vector3.up * DistanceBetweenTheStartSphereAndTheEndSphere;
        }
    }
}
