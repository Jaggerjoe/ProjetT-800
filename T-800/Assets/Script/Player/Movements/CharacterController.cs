using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController : MonoBehaviour
{
  
    [SerializeField]
    private SO_PlayerController m_PlayerInput;

    private Collider m_Collider = null;
    [SerializeField]
    private CapsuleCollider m_CapsuleCollider = null;
    private Animator m_Anim;

  
    #region MovementInfo
    [Header("Movement")]

    [SerializeField]
    float m_MaxSpeed = 12;

    [SerializeField]
    private float m_TimeZeroToMax = 0f;

    [SerializeField]
    private float m_TimeMaxToZero = 0f;

    [SerializeField]
    float m_Velocity = 0f;

    [SerializeField]
    private LayerMask m_LayerDeplacement;
     [SerializeField]
    private float m_HeighPadding = .015f;

    [SerializeField]
    private float m_Height = .015f;
    //Variable d'acceleration et de deceleration
    float m_AccRatePerSec = 0;

    float m_DecRatePerSec = 0;
    RaycastHit l_hit;

    Vector3 m_MovementDirection;

    [SerializeField]
    private float m_MaxGroundAnge = 120;

    private float m_GroundAngle = 0;

    private RaycastHit m_HitInfos;

    private Vector3 m_VectorMovement = Vector3.zero;
    #endregion

    #region JumpInfo
    [Header("Jump Info")]
   
    [SerializeField]
    private float m_JumpTimer = 0;

    [SerializeField]
    private AnimationCurve m_JumpCurve;

    [SerializeField]
    private float m_TimeMaxJumping = .2f;

    [SerializeField]
    private LayerMask m_CollisionLayerDetection;

    [SerializeField]
    private float m_GravityScale = 1f;
    [SerializeField]
    private float m_HeightJumpDetection = 0;
    private float m_YVelocity = 0f;

    private bool m_IsOnTheFloor = false;

    private bool m_IsJumping = false;

    private Vector3 m_InitialPosPlayer = Vector3.zero;
    #endregion
    #region JumpEvent
    [System.Serializable]
    private class JumpEvents
    {
        public JumpInfoEvent m_OnJump = new JumpInfoEvent();
    }

    [SerializeField]
    JumpEvents m_JumpEvent = new JumpEvents();
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        m_AccRatePerSec = m_MaxSpeed / m_TimeZeroToMax;
        m_DecRatePerSec = m_MaxSpeed / m_TimeMaxToZero;
        m_Collider = GetComponent<Collider>();
        m_CapsuleCollider = GetComponent<CapsuleCollider>();
        m_Anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move(m_PlayerInput.MoveVector, Time.deltaTime);
        Jump(Time.deltaTime);
        CheckGround();
        CalculateGroundAngle(); 
    }

    void Move(Vector3 p_Direction, float p_DeltaTime)
    {
        p_Direction = Vector3.ClampMagnitude(p_Direction, 1f);
   
        //je recupère le m_MovementDirection de la camera
        //je recupère le vecteur droit de la camera.
        Vector3 l_CameraForward = Camera.main.transform.forward;
        Vector3 l_CameraRight = Camera.main.transform.right;

        //Mise a zero la valeur en Y du m_MovementDirection et du right de la camera
        //Et nomrmaliztion des vecteur.
        l_CameraForward.y = 0f;
        l_CameraRight.y = 0f;
        l_CameraForward.Normalize();
        l_CameraRight.Normalize();

        //Deplacement en fct de la camera.
        //Normalize notre vecteur DesireDirection pour eviter l'acceleration en diagonale
        Vector3 l_DesireDirection = p_Direction.y * l_CameraForward + p_Direction.x * l_CameraRight;
        l_DesireDirection = Vector3.ClampMagnitude(l_DesireDirection, 1f);
        m_MovementDirection = l_DesireDirection;
        CalculateForward();

        //si je le deplace, j'augmente mon acceleration.
        //j'enregistre mon sens de deplacement ainsi que ma vitesse dans un vecteur Velocity.
        //Je clamp ma valuer a la vitesse maximale.
        //J'ajoute la valuer de mon vecteur velocity a mon transform.position, afin de me deplacer.
        if (p_Direction != Vector3.zero)
        {
            if(p_Direction.y > 0){transform.forward = l_DesireDirection; }
         
            if(p_Direction.y < 0){transform.forward = -l_DesireDirection;}
        
            if(p_Direction.z < 0){transform.right = -l_DesireDirection;}

            if(p_Direction.z > 0) {transform.right = l_DesireDirection;}

            m_Velocity += m_AccRatePerSec * p_DeltaTime;
            m_Velocity = Mathf.Min(m_Velocity, m_MaxSpeed);
            float l_CastDist = m_MovementDirection.magnitude * m_Velocity * p_DeltaTime;
            if (!Physics.CapsuleCast(PointStartCapsule, PointEndCapsule + new Vector3(0,.2f,0), m_CapsuleCollider.radius, m_MovementDirection, out RaycastHit hitInfo, l_CastDist, m_LayerDeplacement))
            {
                if(m_GroundAngle >= m_MaxGroundAnge) return;
                Vector3 lastPosition = transform.position;
                transform.position += m_MovementDirection * l_CastDist;
                m_VectorMovement = m_MovementDirection;
                m_Anim.SetFloat("Speed", m_Velocity);
                m_Anim.SetFloat("MoveDir", p_Direction.y);
                m_Anim.SetFloat("MoveDirX", p_Direction.x);
                
                
                Collider[] hitCollider2 = Physics.OverlapCapsule(PointStartCapsule,PointEndCapsule + new Vector3(0,.2f,0) ,m_CapsuleCollider.radius,m_LayerDeplacement);
                if (hitCollider2.Length >= 1)
                {
                    transform.position = lastPosition;
                    if (Physics.Raycast(transform.position, m_MovementDirection, out RaycastHit HitRay, l_CastDist, m_LayerDeplacement))
                    {
                        Vector3 closestPoint = m_Collider.ClosestPoint(HitRay.point);
                        transform.position = closestPoint - HitRay.normal;
                    }
                }
            }
            else
            {
                m_Velocity = 0;
                m_Anim.SetFloat("Speed", m_Velocity);
            }
        }
        //Sinon j'enregistre mon vecteur que je desire en soustrayant ma velocity a mon vecteur de direction qui est egale a VECTEUR3.Zero
        //Je decremente ma vitesse
        //Je recupère la plus garnde valeur.
        //Mon vecteur de steering est la soustraction de mon vecteur de velocité désiré a ma velocity actuelle
        //Mon vecteur de velocité est alors enregistré en fct de mon steering et de ma deceleration
        //J'ajoute mon vecteur velocité a mon transform.position.
        else
        {
            m_Velocity -= m_DecRatePerSec * p_DeltaTime;
            m_Velocity = Mathf.Max(m_Velocity, 0);

            float l_CastDist = m_VectorMovement.magnitude * m_Velocity * p_DeltaTime;
            if (!Physics.CapsuleCast(PointStartCapsule, PointEndCapsule + new Vector3(0,.2f,0), m_CapsuleCollider.radius, m_VectorMovement, out RaycastHit hitInfo, l_CastDist, m_LayerDeplacement))
            {
                if(m_GroundAngle >= m_MaxGroundAnge) return;
                Vector3 lastPosition = transform.position;
                transform.position += m_VectorMovement * m_Velocity * p_DeltaTime;

                m_Anim.SetFloat("Speed", m_Velocity);
                
                Collider[] hitCollider2 = Physics.OverlapCapsule(PointStartCapsule,PointEndCapsule + new Vector3(0,.2f,0) ,m_CapsuleCollider.radius,m_LayerDeplacement);
                if (hitCollider2.Length >= 1)
                {
                    transform.position = lastPosition;
                    if (Physics.Raycast(transform.position, m_VectorMovement, out RaycastHit HitRay, l_CastDist, m_LayerDeplacement))
                    {
                        Vector3 closestPoint = m_Collider.ClosestPoint(HitRay.point);
                        transform.position = closestPoint - HitRay.normal;
                    }
                }
            }
             else
            {
                m_Velocity = 0;
                m_Anim.SetFloat("Speed", m_Velocity);
            }

        }
    }

    void Jump(float p_DeltaTime)
    {
        //Si mon booleen IsGrounded est a vrai et que j'appuie sur ma touche je lance mon timer
        //Sinon mon timer est remis a 0
        if (m_IsJumping)
        {
            if (m_JumpTimer >= m_TimeMaxJumping)
            {
                StopJump();
                return;
            }
            m_Anim.SetBool("Jump", true);

            float lastJumpTime = m_JumpTimer;
            m_JumpTimer += p_DeltaTime;

            float lastHeight = m_JumpCurve.Evaluate(lastJumpTime);
            float targetHeight = m_JumpCurve.Evaluate(m_JumpTimer);
            float castDistance = (targetHeight - lastHeight);

            if (Physics.CapsuleCast(PointStartCapsule, PointEndCapsule, m_CapsuleCollider.radius * 0.95f, Vector3.up, out RaycastHit hit, castDistance, m_CollisionLayerDetection))
            {
                Vector3 l_TargetPositionCollision = transform.position;
                l_TargetPositionCollision.y = m_InitialPosPlayer.y + lastHeight + hit.distance;
                transform.position = l_TargetPositionCollision;
                StopJump();

                Vector3 lastPosition = transform.position;
                Collider[] hitCollider2 = Physics.OverlapCapsule(PointStartCapsule ,PointEndCapsule , m_CapsuleCollider.radius , m_CollisionLayerDetection);
                if (hitCollider2.Length >= 1)
                {
                    transform.position = lastPosition;
                    if (Physics.Raycast(transform.position, transform.up, out RaycastHit HitRay, m_HeightJumpDetection, m_LayerDeplacement))
                    {
                        Vector3 l_TargetPositionCollisionHead = transform.position;
                        l_TargetPositionCollisionHead.y = m_InitialPosPlayer.y + lastHeight + hit.distance;
                        transform.position = l_TargetPositionCollisionHead;
                        StopJump();
                    }
                }
            }
            else
            {
                Vector3 l_TargetPosition = transform.position;
                l_TargetPosition.y = m_InitialPosPlayer.y  + m_JumpCurve.Evaluate(m_JumpTimer);
                transform.position = l_TargetPosition;
            }
        }
        //Else is character not jumping
        else
        {
            if (m_IsOnTheFloor)
            {
                m_YVelocity = 0f;
                m_JumpTimer = 0;
                m_Anim.SetBool("Jump", false);
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
        if (m_PlayerInput.Jumping && m_IsOnTheFloor)
        {
            m_IsOnTheFloor = false;
            m_IsJumping = true;
            m_InitialPosPlayer = transform.position;
            m_JumpTimer = 0f;
            m_YVelocity = 1f;
            m_JumpEvent.m_OnJump.Invoke(new JumpInfo { JumpOrigin = m_InitialPosPlayer });
        }
    }

    private void StopJump()
    {
        if (m_IsJumping)
        {
            m_IsJumping = false;
            m_YVelocity = 0;
        }
    }
    void CheckGround()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out l_hit, (m_Height + m_HeighPadding), m_CollisionLayerDetection))
        {
            if (Vector3.Distance(transform.position, l_hit.point) < m_Height)
            {
                transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.up * m_Height, 5 * Time.deltaTime);
            }
            m_IsOnTheFloor = true;
        }
        else
        {
            m_IsOnTheFloor = false;
        }
    }
    void CalculateGroundAngle()
    {
        if(!m_IsOnTheFloor)
        {
            m_GroundAngle = 90f;
        }
         
        m_GroundAngle = Vector3.Angle(m_HitInfos.normal, m_MovementDirection);
    }
    void CalculateForward()
    {
        if(!m_IsOnTheFloor)
        {
            m_VectorMovement = m_MovementDirection;
            return;
        }
        m_MovementDirection = Vector3.Cross(l_hit.normal, Quaternion.AngleAxis(-90, Vector3.up) * m_MovementDirection);
    }

    private float DistanceBetweenTheStartSphereAndTheEndSphere
    {
        get
        {
            return m_CapsuleCollider.height / 2 - m_CapsuleCollider.radius;
        }
    }
    private Vector3 PointStartCapsule
    {
        get
        {
            return transform.position + m_CapsuleCollider.center + Vector3.up * DistanceBetweenTheStartSphereAndTheEndSphere;
        }
    }

    private Vector3 PointEndCapsule
    {
        get
        {
            return transform.position + m_CapsuleCollider.center - Vector3.up * DistanceBetweenTheStartSphereAndTheEndSphere;
        }
    }

    public Vector3 MovementDirection
    {
        get { return m_MovementDirection; }
    }



    public float Speed
    {
        get { return m_MaxSpeed; }
        set { m_MaxSpeed = value; }
    }
    public float Velocity
    {
        get{ return m_Velocity; } 
        set{ m_Velocity = value; }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, -transform.up * (m_Height + m_HeighPadding));
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(PointStartCapsule, m_CapsuleCollider.radius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(PointEndCapsule, m_CapsuleCollider.radius);

        Gizmos.color = Color.white;
        Gizmos.DrawRay(transform.position, Vector3.up * m_HeightJumpDetection);
    }
}
