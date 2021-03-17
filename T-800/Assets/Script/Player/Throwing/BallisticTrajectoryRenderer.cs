using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class BallisticTrajectoryRenderer : MonoBehaviour
{
    #region References Scripts
    
    [SerializeField]
    private IntercationBodyPlayer m_Etat;
    
    [SerializeField]
    private SO_PlayerController m_Controller;

    [SerializeField]
    private SimpleGrabSystem ThrowScript;
    #endregion

    #region Trajectory
    // Référence au Line renderer
    private LineRenderer m_Line;

    // Position initiale de la trajectoire
    [SerializeField]
    private Vector3 m_StartPosition;

    //[SerializeField]
    //private Transform m_Firepoint;

    // Vitesse initiale de la trajectoire
    [SerializeField]
    private Vector3 m_StartVelocity;

    // Distance de pas pour la trajectoire
    [SerializeField]
    private float m_TrajectoryVertDist = 0.25f;

    // Longueur maximale de la trajectoire
    [SerializeField]
    private float m_MaxCurveLength = 5;

    //Layer obstacle ignoré par la trajectoire
    [SerializeField]
    private LayerMask m_IgnoreTrajectoryMask;

    // Liste des points de la trajectoires récupérable par autres scripts
    public List<Vector3> m_CurvePoints = new List<Vector3>();
    #endregion

    //boolean pour le switch de camera
    private bool m_IsAiming = false;


    private void Awake()
    {
        m_IsAiming = false;

        //m_StartPosition = new Vector3(m_Firepoint.position.x, m_Firepoint.position.y, m_Firepoint.position.z);

        // ref du m_Line Renderer
        m_Line = GetComponent<LineRenderer>();
    }
 
    private void Update()
    {
        
        
            //Je dessine la trajectoire lorsque j'appuie sur le click gauche
            if (m_Controller.Aiming )
            {
                if (m_Etat.Etat == EtatDuPlayer.DeuxBras)
                {
                    
                    DrawTrajectory();
                    ThrowScript.IsArming = true;
            }    
                    
            }
            //J'éfface la traj quand je relache le click
            else
            {

                ClearTrajectory();
                ThrowScript.IsArming = false;
            }

        
      
    }

    // Définit les valeurs balistiques de la trajectoire.


    public void SetBallisticValues(Vector3 m_StartPosition, Vector3 m_StartVelocity)
    {
        this.m_StartPosition = m_StartPosition;
        this.m_StartVelocity = m_StartVelocity;
    }

    // Dessine la trajectoire du m_Line renderer

    private void DrawTrajectory()
    {
        m_IsAiming = true;

        //  Créer une liste de points de trajectoire
        List<Vector3> l_CurvePoints = new List<Vector3>();
        l_CurvePoints.Add(m_StartPosition);

        // Valeurs initiales de la trajectoire
        var currentPosition = m_StartPosition;
        var currentVelocity = m_StartVelocity;

        //  Variables de physique init
        RaycastHit hit;
        Ray ray = new Ray(currentPosition, currentVelocity.normalized);

        // Boucle jusqu'à ce que ca touche quelque chose ou que la distance soit trop grande
        while (!Physics.Raycast(ray, out hit, m_TrajectoryVertDist,m_IgnoreTrajectoryMask) && Vector3.Distance(m_StartPosition, currentPosition) < m_MaxCurveLength)
        {
            // Temps pour parcourir la distance de la trajectoireVertDist
            var t = m_TrajectoryVertDist / currentVelocity.magnitude;

            // Mise à jour de la position et de la vitesse
            currentVelocity = currentVelocity + t * Physics.gravity;
            currentPosition = currentPosition + t * currentVelocity;

            // Ajouter un point à la trajectoire
            l_CurvePoints.Add(currentPosition);

            // Créer un nouveau rayon
            ray = new Ray(currentPosition, currentVelocity.normalized);
        }
       
        // Si quelque chose a été touché, ajoutez le dernier point ici
        if (hit.transform)
        {
            l_CurvePoints.Add(hit.point);
        }
        
        // Afficher la ligne avec tous les points
        m_Line.positionCount = l_CurvePoints.Count;
        m_Line.SetPositions(l_CurvePoints.ToArray());
        m_CurvePoints = l_CurvePoints;
    }


    //efface la traj
    private void ClearTrajectory()
    {
        m_IsAiming = false;
       
        // cache la ligne
        m_Line.positionCount = 0;
    }


    public bool Aiming { get { return m_IsAiming; } }
}

