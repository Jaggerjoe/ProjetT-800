using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class Respawning : MonoBehaviour
{

    //[SerializeField]
    //private Transform m_Player; 
    //[SerializeField]
    //private Transform m_SpawnPoint;
    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("coucoulou");
    //    m_Player.position = m_SpawnPoint.position;
    //}
    [SerializeField]
    private CharacterController m_Controller;
    [SerializeField]
    private CapsuleCollider m_CapsuleCollider = null;
    [SerializeField]
    private float m_CastDistance = 0;
    [SerializeField]
    private LayerMask m_RespawnDetection; 
    [SerializeField]
    private LayerMask m_RespawnPointDetection;
    [SerializeField]
    private LayerMask m_FadeDetection;
    [SerializeField]
    private Transform m_SpawnPoint;
    [SerializeField]
    private Image m_BlackScreen;

    private Vector3 m_AdjustRespawn = new Vector3(0, 5, 0);


    private void Update()
    {
        if (Physics.CapsuleCast(PointStartCapsule, PointEndCapsule, m_CapsuleCollider.radius * 0.95f, Vector3.up, out RaycastHit hit, m_CastDistance, m_RespawnDetection))
        {
          
            transform.position = m_SpawnPoint.position ;
        }

        if (Physics.CapsuleCast(PointStartCapsule, PointEndCapsule, m_CapsuleCollider.radius * 0.95f, Vector3.up, out RaycastHit hitFade, m_CastDistance, m_FadeDetection))
        {
            Sequence l_MySequence = DOTween.Sequence();
            l_MySequence.Insert(0, m_BlackScreen.DOFade(1, 1f));
            l_MySequence.Insert(1f, m_BlackScreen.DOFade(0, 1f));

            
        }


        float l_CastDist = m_Controller.MovementDirection.magnitude * m_Controller.Velocity * Time.deltaTime;

        if (Physics.CapsuleCast(PointStartCapsule, PointEndCapsule + new Vector3(0, .2f, 0), m_CapsuleCollider.radius, m_Controller.MovementDirection, out RaycastHit hitInfo, l_CastDist, m_RespawnPointDetection))
        {
            Debug.Log("holaholé");
            m_SpawnPoint.position = transform.position + m_AdjustRespawn;
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
