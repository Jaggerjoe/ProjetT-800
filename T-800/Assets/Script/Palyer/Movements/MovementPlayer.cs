using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    [SerializeField]
    private SO_PlayerController m_PlayerController;
    #region Deplacement acceleration et Deceleration
    [SerializeField]
    float m_MaxSpeed = 12;

    [SerializeField]
    private float m_TimeZeroToMax;

    [SerializeField]
    private float m_TimeMaxToZero;

    [SerializeField]
    float m_SpeedAccel = 0;

    [SerializeField]
    Vector3 m_Velocity;

    //Variable d'acceleration et de deceleration
    float m_AccRatePerSec;
    float m_DecRatePerSec;
    #endregion
    #region Jump
    [SerializeField]
    private float m_JumpTimer = 0;

    [SerializeField]
    private AnimationCurve m_JumpHeight;

    [SerializeField]
    private float m_CheckDist;

    [SerializeField]
    private float m_TimeMaxJumping = 1.5f;

    private float m_TimeBeforeNextJump = 0;

    [SerializeField]
    private float m_TimeBeforeNextJumpMax = .3f;
    private bool m_IsGrounded = false;
    Vector3 l_PlayerPos;
    Vector3 m_PosPlayerY;

    [SerializeField]
    private LayerMask m_IsItGround;
    #endregion
    #region Rotation to aim
    [SerializeField]
    private float m_XAngleMin = -80.0f;
    [SerializeField]
    private float m_XAngleMax = 0;
    private float m_RotationX = 0f;
    [SerializeField]
    private float m_SensitivityY = 0.1f;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //Ratio de l'acceleration en fct de la vitesse max et tu tps.
        //Ratio de la deceleration en fct de la vitesse mex et du tps
        m_AccRatePerSec = m_MaxSpeed / m_TimeZeroToMax;
        m_DecRatePerSec = m_MaxSpeed / m_TimeMaxToZero;
    }

    // Update is called once per frame
    void Update()
    {
        RotateAiming(m_PlayerController.RotationVector);
        Jump();
        Move(m_PlayerController.MoveVector, Time.deltaTime);
    }

    public void Move(Vector3 _Direction, float p_DeltaTime)
    {
        //je recupère le forward de la camera
        //je recupère le vecteur droit de la camera.
        Vector3 l_CameraForward = Camera.main.transform.forward;
        Vector3 l_CameraRight = Camera.main.transform.right;

        //Mise a zero la valuer en Y du forward et du right de la camera
        //Et nomrmaliztion des vecteur.
        l_CameraForward.y = 0f;
        l_CameraRight.y = 0f;
        l_CameraForward.Normalize();
        l_CameraRight.Normalize();

        //Deplacement en fct de la camera.
        //Normalize notre vecteur DesireDirection pour eviter l'acceleration en diagonale
        Vector3 l_DesireDirection = _Direction.y * l_CameraForward + _Direction.x * l_CameraRight;
        l_DesireDirection.Normalize();

        //si je le deplace, j'augmente mon acceleration.
        //j'enregistre mon sens de deplacement ainsi que ma vitesse dans un vecteur Velocity.
        //Je clamp ma valuer a la vitesse maximale.
        //J'ajoute la valuer de mon vecteur velocity a mon transform.position, afin de me deplacer.
        if (_Direction != Vector3.zero)
        {
            transform.forward = l_DesireDirection;
            m_SpeedAccel += m_AccRatePerSec * Time.deltaTime;

            m_Velocity = new Vector3(l_DesireDirection.x * m_SpeedAccel * p_DeltaTime, 0, l_DesireDirection.z * m_SpeedAccel * p_DeltaTime);
            m_SpeedAccel = Mathf.Min(m_SpeedAccel, m_MaxSpeed);
            transform.position += m_Velocity;
           
        }
        //Sinon j'enregistre mon vecteur que je desire en soustrayant ma velocity a mon vecteur de direction qui est egale a VECTEUR3.Zero
        //Je decremente ma vitesse
        //Je recupère la plus garnde valeur.
        //Mon vecteur de steering est la soustraction de mon vecteur de velocité désiré a ma velocity actuelle
        //Mon vecteur de velocité est alors enregistré en fct de mon steering et de ma deceleration
        //J'ajoute mon vecteur velocité a mon transform.position.
        else
        {
            Vector3 m_DesireVelocity = m_Velocity - _Direction;
            m_DesireVelocity.Normalize();

            m_SpeedAccel -= m_DecRatePerSec * Time.deltaTime;
            m_SpeedAccel = Mathf.Max(m_SpeedAccel, 0);

            Vector3 steering = m_DesireVelocity - m_Velocity;
            m_Velocity = new Vector3(steering.x * m_SpeedAccel * p_DeltaTime, 0, steering.z * m_SpeedAccel * p_DeltaTime);

            transform.position += m_Velocity;
        }
        //Debug.Log("velocity : " + m_SpeedAccel);
    }

    public void Jump()
    {
        //Rajout Coldown pour le saut.
        if (Physics.Raycast(transform.position, Vector3.down, m_CheckDist, m_IsItGround))
        {
            m_PosPlayerY.y = transform.position.y;
            m_IsGrounded = true;
            m_JumpTimer = 0;
           
        }

        if (m_PlayerController.Jumping && m_IsGrounded)
        {
            if (m_JumpTimer <= m_TimeMaxJumping)
            {
                m_JumpTimer += Time.deltaTime;
            }
            else
            {
                m_JumpTimer = 0;
            }
            
            //Je recupère la position du player en X et en Z, mais le Y est tjr égale a 0;
            //je récupère la position du player en ajoutant la valeur en Y par rapport a ma curve
            //J'ajoute cettes valeur a mon transform.position.
            l_PlayerPos = new Vector3(transform.position.x, 0, transform.position.z);
            Vector3 l_TargetPos = m_PosPlayerY + new Vector3(l_PlayerPos.x, m_JumpHeight.Evaluate(m_JumpTimer), l_PlayerPos.z);
            transform.position = l_TargetPos;
        }
        else
        {
            m_JumpTimer = 0;
            m_IsGrounded = false;
        }
    }
   
    private void RotateAiming(Vector3 p_MouseAim)
    {
        m_RotationX += p_MouseAim.y * m_SensitivityY;
        m_RotationX = Mathf.Clamp(m_RotationX, m_XAngleMin, m_XAngleMax);
        Quaternion l_Rotation = Quaternion.Euler(m_RotationX, 1, 0);
        transform.rotation =  l_Rotation;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, m_CheckDist, 0));
    }
}

