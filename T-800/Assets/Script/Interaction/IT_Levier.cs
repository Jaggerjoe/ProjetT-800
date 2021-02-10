using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IT_Levier : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Levier;
    private Animator m_Anim;
    private void Start()
    {
        m_Anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update

    public void Levier()
    {
        m_Anim.SetTrigger("Interact");
    }
}
