using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionPlayer : MonoBehaviour
{
    private bool m_HitDetect = false;
    [SerializeField]
    private Collider m_Collider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        RaycastHit m_Hit;
        m_HitDetect = Physics.BoxCast(m_Collider.bounds.center, transform.localScale, transform.forward, out m_Hit, transform.rotation, 0f);
        
        if(m_HitDetect)
        {
            transform.position = new Vector3(m_Hit.transform.position.x, transform.position.y, m_Hit.transform.position.z);
        }
        
    }
}
