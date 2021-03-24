using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Global_Interaction : MonoBehaviour
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
    private SO_PlayerController m_PlayerController;

    [SerializeField]
    private GameObject m_Camera = null;

    private Material m_CurrentMat = null;

    
    private Image m_CurrentUI = null;

    private bool m_UseObject = false;

    private InteractionMother m_InteractObj;

    private float m_Time = 0.001f;

    private void OnEnable()
    {
        m_PlayerController.InteractionObj.AddListener(DetectionInteraction);
    }

    private void OnDisable()
    {
        m_PlayerController.InteractionObj.RemoveListener(DetectionInteraction);
    }

    private void Update()
    {
        DetectionObjectUI();
        DetectionObjectInteractable();
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
                    if (m_RefInteraction.Etat != EtatDuPlayer.SansBras)
                    {
                        if (hit.gameObject.TryGetComponent(out m_InteractObj))
                        {
                            if(!m_UseObject)
                            {
                                m_InteractObj.PlayerControllerSO = m_PlayerController;
                                m_UseObject = true;
                                m_InteractObj.Use();
                            }
                            else
                            {
                                m_InteractObj.StopUse();
                            }
                        }
                    }
                    else if(m_RefInteraction.Etat == EtatDuPlayer.SansBras)
                    {
                        if (hit.gameObject.TryGetComponent(out m_InteractObj))
                        {
                            m_InteractObj.UseWithOneArm();
                        }
                    }
                }
            }
        }
    }

    public void DetectionObjectInteractable()
    {
        Collider[] l_Collides = Physics.OverlapSphere(transform.position, 5f, m_Layer);
        {
            foreach (var item in l_Collides)
            {
                if(m_CurrentMat == null)
                {
                    m_CurrentMat = item.GetComponentInChildren<Renderer>().materials[1];
                }
                if (Vector3.Distance(transform.position, item.gameObject.transform.position) < 3f)
                {
                    m_CurrentMat.SetColor("_Color_Outline", Color.Lerp(Color.black, Color.yellow, m_Time));
                    m_Time += Time.deltaTime;
                }
                else 
                {
                    m_CurrentMat.SetColor("_Color_Outline", Color.black);
                    m_Time = 0;
                    m_CurrentMat = null;
                }
            }
        }
    }

    public void DetectionObjectUI()
    {
        Collider[] l_Collides = Physics.OverlapSphere(transform.position, 10f, m_Layer);
        {
            foreach (var item in l_Collides)
            {
                if (m_CurrentUI == null)
                {
                    m_CurrentUI = item.GetComponentInChildren<Image>();
                }
                if (Vector3.Distance(transform.position, item.gameObject.transform.position) < 3f)
                {
                    Sequence l_ESequence = DOTween.Sequence();
                    l_ESequence.Insert(0, m_CurrentUI.DOFade(1, 1));
                    l_ESequence.Insert(0, m_CurrentUI.transform.DOScale(1, 1));
                    m_CurrentUI.transform.LookAt(m_Camera.transform);
                }
                else
                {

                    Sequence l_ESequence = DOTween.Sequence();
                    l_ESequence.Insert(0, m_CurrentUI.DOFade(0, 1));
                    l_ESequence.Insert(0, m_CurrentUI.transform.DOScale(0, 1));
                    m_CurrentUI = null;
                    //m_Time = 0;
                }
            }
        }
    }
    // public void Interaction()
    // {
    //     if(Physics.BoxCast(transform.position, m_Collider.bounds.extents, Vector3.down, out RaycastHit m_hit,Quaternion.identity, .5f, m_Layer))
    //     {
    //         if(m_hit.collider.gameObject.TryGetComponent(out m_InteractObj))
    //         {
    //             m_InteractObj.Use();
    //         }
    //     }
    // }

    public bool UseObject
    {
        get { return m_UseObject; }
        set { m_UseObject = value; }
    }
}

