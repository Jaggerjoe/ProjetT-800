using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class BallisticTrajectoryRenderer : MonoBehaviour
{
    [SerializeField]
    SO_PlayerController m_Controller;
    // Référence au line renderer
    private LineRenderer line;
    // Position initiale de la trajectoire
    [SerializeField]
    private Vector3 startPosition;
    //[SerializeField]
    //private Transform m_Firepoint;
    // Vitesse initiale de la trajectoire
    [SerializeField]
    private Vector3 startVelocity;
    // Distance de pas pour la trajectoire
    [SerializeField]
    private float trajectoryVertDist = 0.25f;
    // Longueur maximale de la trajectoire
    [SerializeField]
    private float maxCurveLength = 5;
    [Header("Debug")]

    [SerializeField]
    private bool _debugAlwaysDrawTrajectory = false;



    //boolean pour le switch de camera

    private bool m_IsAiming = false;


    /// <summary>
    /// Method called on initialization.
    /// </summary>
    private void Awake()
    {
        m_IsAiming = false;
        //startPosition = new Vector3(m_Firepoint.position.x, m_Firepoint.position.y, m_Firepoint.position.z);
        // ref du line Renderer
        line = GetComponent<LineRenderer>();
    }
    /// <summary>
    /// Method called on every frame.
    /// </summary>
    private void Update()
    {
        //Je dessine la trajectoire lorsque j'appuie sur le click gauche
        if (m_Controller.Aiming /*|| _debugAlwaysDrawTrajectory*/)
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


    public void SetBallisticValues(Vector3 startPosition, Vector3 startVelocity)
    {
        this.startPosition = startPosition;
        this.startVelocity = startVelocity;
    }

    // Dessine la trajectoire du line renderer

    private void DrawTrajectory()
    {
        m_IsAiming = true;
        //  Créer une liste de points de trajectoire
        var curvePoints = new List<Vector3>();
        curvePoints.Add(startPosition);
        // Valeurs initiales de la trajectoire
        var currentPosition = startPosition;
        var currentVelocity = startVelocity;
        //  Variables de physique init
        RaycastHit hit;
        Ray ray = new Ray(currentPosition, currentVelocity.normalized);
        // Bouclez jusqu'à ce que vous touchiez quelque chose ou que la distance soit trop grande
        while (!Physics.Raycast(ray, out hit, trajectoryVertDist) && Vector3.Distance(startPosition, currentPosition) < maxCurveLength)
        {
            // Temps pour parcourir la distance de la trajectoireVertDist
            var t = trajectoryVertDist / currentVelocity.magnitude;
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
        line.positionCount = curvePoints.Count;
        line.SetPositions(curvePoints.ToArray());
    }

    //efface la traj

    private void ClearTrajectory()
    {
        m_IsAiming = false;
        // cache la ligne
        line.positionCount = 0;
    }


    public bool Aiming { get { return m_IsAiming; } }
}

