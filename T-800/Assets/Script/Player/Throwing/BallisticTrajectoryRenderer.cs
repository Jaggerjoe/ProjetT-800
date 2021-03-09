using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class BallisticTrajectoryRenderer : MonoBehaviour
{
    [SerializeField]
    SO_PlayerController m_Controller;
    // Référence au m_Line renderer
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
    [Header("Debug")]

    [SerializeField]
    private bool m_DebugAlwaysDrawTrajectory = false;



    //boolean pour le switch de camera

    private bool m_IsAiming = false;


    /// <summary>
    /// Method called on initialization.
    /// </summary>
    private void Awake()
    {
        m_IsAiming = false;
        //m_StartPosition = new Vector3(m_Firepoint.position.x, m_Firepoint.position.y, m_Firepoint.position.z);
        // ref du m_Line Renderer
        m_Line = GetComponent<LineRenderer>();
    }
    /// <summary>
    /// Method called on every frame.
    /// </summary>
    private void Update()
    {
        //Je dessine la trajectoire lorsque j'appuie sur le click gauche
        if (m_Controller.Aiming /*|| m_DebugAlwaysDrawTrajectory*/)
        {
            
            DrawTrajectory();
        }
        //J'éfface la traj quand je relache le click
        else
        {

            ClearTrajectory();
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
        var curvePoints = new List<Vector3>();
        curvePoints.Add(m_StartPosition);
        // Valeurs initiales de la trajectoire
        var currentPosition = m_StartPosition;
        var currentVelocity = m_StartVelocity;
        //  Variables de physique init
        RaycastHit hit;
        Ray ray = new Ray(currentPosition, currentVelocity.normalized);
        // Bouclez jusqu'à ce que vous touchiez quelque chose ou que la distance soit trop grande
        while (!Physics.Raycast(ray, out hit, m_TrajectoryVertDist) && Vector3.Distance(m_StartPosition, currentPosition) < m_MaxCurveLength)
        {
            // Temps pour parcourir la distance de la trajectoireVertDist
            var t = m_TrajectoryVertDist / currentVelocity.magnitude;
            // Mise à jour de la position et de la vitesse
            currentVelocity = currentVelocity + t * Physics.gravity;
            currentPosition = currentPosition + t * currentVelocity;
            // Ajouter un point à la trajectoire
            curvePoints.Add(currentPosition);
            // Créer un nouveau rayon
            ray = new Ray(currentPosition, currentVelocity.normalized);
        }
        // Si quelque chose a été touché, ajoutez le dernier point ici
        if (hit.transform)
        {
            curvePoints.Add(hit.point);
        }
        // Afficher la ligne avec tous les points
        m_Line.positionCount = curvePoints.Count;
        m_Line.SetPositions(curvePoints.ToArray());
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

