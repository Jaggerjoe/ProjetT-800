using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    [SerializeField]
    private SO_PlayerController m_PlayerController;

    [SerializeField]
    private Collider m_colldier;

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
    private AnimationCurve m_JumpCurve;

    [SerializeField]
    private float m_TimeMaxJumping = .2f;

    private float m_YVelocity = 0f;

    [SerializeField]
    private LayerMask m_LayerCollision;

    private bool m_IsOnTheFloor = false;

    private float m_GravityScale = 1f;


    #endregion

    private float m_DistEditor;
    private Vector3 dir;
    private Quaternion targetRotation;
    Vector3 forward;
    RaycastHit l_hit;
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
        Jump(Time.deltaTime);
        Move(m_PlayerController.MoveVector, Time.deltaTime);
        CheckGround(Time.deltaTime);
    }
    private void FixedUpdate()
    {
        m_YVelocity += Physics.gravity.y * Time.fixedDeltaTime;
    }

    void CheckGround(float p_DeltaTime)
    {
       
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
        
        Vector3 l_TargetPosition = transform.position;
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

            float l_CastDistXZ = m_MaxSpeed * Mathf.Abs(l_DesireDirection.z + l_DesireDirection.x) * p_DeltaTime;
            m_DistEditor = l_CastDistXZ;

            RaycastHit[] l_hit = Physics.BoxCastAll(transform.position, Extents, transform.forward, Quaternion.identity, l_CastDistXZ, m_LayerCollision);

            foreach (RaycastHit item in l_hit)
            {
                if(!item.transform)
                {
                    l_TargetPosition = transform.position + m_Velocity;
                }
                else
                {
                    m_SpeedAccel = 0;

                }
            }

            //if (!Physics.BoxCast(transform.position, Extents, transform.forward, out RaycastHit hit, Quaternion.identity ,l_CastDistXZ, m_LayerCollision))
            //{              
            //    l_TargetPosition = transform.position + m_Velocity;
            //}
            //else
            //{
            //    m_SpeedAccel = 0;
            //}
            transform.position = l_TargetPosition;
        }
        //Sinon j'enregistre mon vecteur que je desire en soustrayant ma velocity a mon vecteur de direction qui est egale a VECTEUR3.Zero
        //Je decremente ma vitesse
        //Je recupère la plus garnde valeur.
        //Mon vecteur de steering est la soustraction de mon vecteur de velocité désiré a ma velocity actuelle
        //Mon vecteur de velocité est alors enregistré en fct de mon steering et de ma deceleration
        //J'ajoute mon vecteur velocité a mon transform.position.
        else
        {
            Vector3 l_DesireVelocity = m_Velocity - _Direction;
            l_DesireVelocity.Normalize();

            m_SpeedAccel -= m_DecRatePerSec * Time.deltaTime;
            m_SpeedAccel = Mathf.Max(m_SpeedAccel, 0);

            Vector3 steering = l_DesireVelocity - m_Velocity;
            m_Velocity = new Vector3(steering.x * m_SpeedAccel * p_DeltaTime, 0, steering.z * m_SpeedAccel * p_DeltaTime);

            transform.position += m_Velocity;
        }
    }

    void CalculateForward()
    {
        if (!m_IsOnTheFloor)
        {
            forward = transform.forward;
            return;
        }
        forward = Vector3.Cross(l_hit.normal, -transform.right);
    }
    private Vector3 Extents
    {
        get
        {
            if (m_colldier != null)
            {
                return m_colldier.bounds.extents;
            }
            return Vector3.one / 2;
        }
    }
    public void Jump(float p_DeltaTime)
    {
        //Si mon booleen IsGrounded est a vrai et que j'appuie sur ma touche je lance mon timer
        //Sinon mon timer est remis a 0
        if (m_PlayerController.Jumping && m_IsOnTheFloor)
        {
            if (m_JumpTimer <= m_TimeMaxJumping)
            {
                m_JumpTimer += p_DeltaTime;
            }
            else
            {
                m_JumpTimer = 0;
                StopJump();
            }
            //Je recupère la position du player en X et en Z, mais le Y est tjr égale a 0;
            //je récupère la position du player en ajoutant la valeur en Y par rapport a ma curve
            //J'ajoute cettes valeur a mon transform.position.
            Vector3 l_TaregtPosition = transform.position;
            l_TaregtPosition.y = transform.position.y + m_JumpCurve.Evaluate(m_JumpTimer);
            //l_PlayerPos = new Vector3(transform.position.x, 0, transform.position.z);
            //Vector3 l_TargetPos = m_PosPlayerY + new Vector3(l_PlayerPos.x, m_JumpHeight.Evaluate(m_JumpTimer), l_PlayerPos.z);
            transform.position = l_TaregtPosition;
        }
        else
        {
            float castDist = Mathf.Abs(m_YVelocity) * p_DeltaTime + 1;
            if (Physics.BoxCast(transform.position + Vector3.up * 1, m_colldier.bounds.extents, Vector3.down, out RaycastHit m_Hit, Quaternion.identity, castDist, m_LayerCollision))
            {
                if (!m_IsOnTheFloor)
                {
                    Vector3 targetPosition = transform.position;
                    targetPosition.y = m_Hit.point.y + m_colldier.bounds.extents.y;
                    transform.position = targetPosition;

                    m_YVelocity = 0f;
                    m_IsOnTheFloor = true;
                    m_JumpTimer = 0;
                }
            }
            else
            {
                Vector3 targetPosition = transform.position;
                targetPosition.y += m_YVelocity * p_DeltaTime;
                transform.position = targetPosition;

                if (m_IsOnTheFloor)
                {
                    m_IsOnTheFloor = false;
                }
                m_YVelocity += Physics.gravity.y * m_GravityScale * p_DeltaTime;

            }
        }
    }

    private void StopJump()
    {
        if(m_PlayerController.Jumping)
        {
            m_PlayerController.Jumping = false;
            m_YVelocity = 0;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + (transform.forward ), m_colldier.bounds.size);
        Gizmos.DrawLine(transform.position, transform.position + forward *2);
    }
}

