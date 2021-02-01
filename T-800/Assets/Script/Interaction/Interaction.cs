using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Target;

    [SerializeField]
    private LayerMask m_Layer;

    private void Start()
    {
    }

    private void Update()
    {

    }

    public void ActionLevier()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward, Color.cyan);
        if (Physics.Raycast(transform.position, transform.forward, out hit, 10, m_Layer))
        {
            Debug.Log("je suis touché");
        }
    }
}
