using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class SimpleGrabSystem : MonoBehaviour
{
    [SerializeField]
    private IntercationBodyPlayer m_Etat;
    [SerializeField]
    private SO_PlayerController m_Controller;
    [SerializeField]
    private BallisticTrajectoryRenderer m_Trajectory;
    // Référence au point sur lequel se rend l'objet 
    [SerializeField]
    private Transform m_Slot;
    


    private bool m_IsArming = false;
 

    // Ref de l'objet
    [SerializeField]
    private GameObject m_Arm; 
    [SerializeField]
    private GameObject m_SkeletArm;

    [SerializeField]
    private GameObject m_Automaton;

    [Header("Throw")]
    // 
    [SerializeField]
    private Vector3 m_ThrowVelocity = new Vector3(0, 0, 5);

    [SerializeField]
    private List<Vector3> m_ThrowPoints = new List<Vector3>();

    #region Lancé
    [SerializeField]
    private float m_ThrowSpeed;

    private float m_Timer;

    private int m_CurrentPoint;

    private Vector3 m_StartPositon;

    static Vector3 m_CurrentPositionHolder;
    #endregion

    // Event class which will be displayed in the inspector.
    [System.Serializable]
    public class LocationChanged : UnityEvent<Vector3, Vector3> { }

    // Event for location change. Used to update ballistic trajectory.
    public LocationChanged OnLocationChanged;


    private void Start()
    {
       
        m_Arm.transform.position = m_Slot.position;
        CheckPoint();
    }

    private void Update()
    {

       
        if ( m_Etat.Etat == EtatDuPlayer.DeuxBras)
        {

            if (m_IsArming )
            {

                SetArmThrow();


            }
            else
            {
               
                SetArmPos();
                
            }
        }

     


        OnLocationChanged?.Invoke(m_Slot.position, m_Slot.rotation * m_ThrowVelocity);
    }





    private void OnEnable()
    {
        
        m_Controller.onThrow.AddListener(Throwing);
    }


    private void OnDisable()
    {
        m_Controller.onThrow.RemoveListener(Throwing);
    }


    void CheckPoint()
    {
        m_StartPositon = m_Arm.transform.position;
       
        if (m_CurrentPoint < m_ThrowPoints.Count - 1)
        {
            m_Timer = 0;
            m_CurrentPositionHolder = m_ThrowPoints[m_CurrentPoint];

         
        }
    }
    void Throwing()
    {
        
        m_ThrowPoints = m_Trajectory.m_CurvePoints;
        m_Arm.transform.SetParent(null);
        m_Etat.Etat = EtatDuPlayer.UnBras;


        CheckPoint();
        
        StartCoroutine(FollowCurve());
    }
    public void SetArmThrow()
    {

        m_SkeletArm.SetActive(false);
      
        
        
    }

    public void SetArmPos()
    {

        m_SkeletArm.SetActive(true);

    }

    IEnumerator FollowCurve()
    {

       
        while (m_CurrentPoint < m_ThrowPoints.Count - 1)
        {

            m_Timer += Time.deltaTime * m_ThrowSpeed;
            if (m_Arm.transform.position != m_CurrentPositionHolder)
            {
                m_Arm.transform.position = Vector3.Lerp(m_StartPositon, m_CurrentPositionHolder, m_Timer);
                Debug.Log("ok");
            }
            else
            {
                
                Debug.Log("vui");
                m_CurrentPoint++;
                CheckPoint();

            }
            yield return null;
        }


        m_Arm = null;
        

       

    }
    

    private void OnDrawGizmos()
    {
        Vector3 position = transform.position + transform.forward * 3;
        Gizmos.DrawLine(transform.position,position);
    }

    public bool IsArming { get { return m_IsArming; } set { m_IsArming = value; } }
}

