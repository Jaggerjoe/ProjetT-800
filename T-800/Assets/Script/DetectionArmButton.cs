using UnityEngine;
using UnityEngine.Events;
using UnityEditor;


public class DetectionArmButton : MonoBehaviour
{
    [SerializeField]
    private CapsuleCollider m_CapsuleCollider = null;

    [SerializeField]
    private float m_CastDistance = 0;

    [SerializeField]
    private LayerMask m_ButtonDetection;

    private Interaction_Button m_ButtonInteraction;

    [System.Serializable]
    private class ArmEvent
    {
        public UnityEvent m_ArmCollision = new UnityEvent();
    }
    [SerializeField]
    private ArmEvent m_ArmEvents = new ArmEvent();

    void Update()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, m_CastDistance, m_ButtonDetection))
        {
            m_ButtonInteraction = hit.collider.gameObject.GetComponent<Interaction_Button>();
            m_ButtonInteraction.Use();
            m_ArmEvents.m_ArmCollision.Invoke();
            m_CapsuleCollider.gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * m_CastDistance);
    }


  

}
