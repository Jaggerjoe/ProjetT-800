using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private SO_PlayerController m_PlayerInput;

    private Collider m_Collider = null;
    [SerializeField]
    private CapsuleCollider m_CapsuleCollider = null;
    #region Movement
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

    //Variable d'acceleration et de deceleration
    float m_AccRatePerSec = 0;

    float m_DecRatePerSec = 0;
    [SerializeField]
    private float m_HeighPadding = .015f;

    [SerializeField]
    private float m_Height = .015f;

    RaycastHit l_hit;

    Vector3 forward;

    bool grounded = false;


    #endregion

    #region Jump
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

    private float m_YVelocity = 0f;

    private bool m_IsOnTheFloor = false;

    private bool m_IsJumping = false;

    private Vector3 m_InitialPosPlayer = Vector3.zero;

    private float m_coucoutoi;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        m_AccRatePerSec = m_MaxSpeed / m_TimeZeroToMax;
        m_DecRatePerSec = m_MaxSpeed / m_TimeMaxToZero;
        m_Collider = GetComponent<Collider>();
        m_CapsuleCollider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        Move(m_PlayerInput.MoveVector, Time.deltaTime);
        Jump(Time.deltaTime);
        CalculateForward();
        CheckGround();
        Debug.Log(grounded);

    }

    void Move(Vector3 p_Direction, float p_DeltaTime)
    {
        p_Direction = Vector3.ClampMagnitude(p_Direction, 1f);
        //je recupère le forward de la camera
        //je recupère le vecteur droit de la camera.
        Vector3 l_CameraForward = Camera.main.transform.forward;
        Vector3 l_CameraRight = Camera.main.transform.right;

        //Mise a zero la valeur en Y du forward et du right de la camera
        //Et nomrmaliztion des vecteur.
        l_CameraForward.y = 0f;
        l_CameraRight.y = 0f;
        l_CameraForward.Normalize();
        l_CameraRight.Normalize();

        //Deplacement en fct de la camera.
        //Normalize notre vecteur DesireDirection pour eviter l'acceleration en diagonale
        Vector3 l_DesireDirection = p_Direction.y * l_CameraForward + p_Direction.x * l_CameraRight;
        l_DesireDirection = Vector3.ClampMagnitude(l_DesireDirection, 1f);

        //si je le deplace, j'augmente mon acceleration.
        //j'enregistre mon sens de deplacement ainsi que ma vitesse dans un vecteur Velocity.
        //Je clamp ma valuer a la vitesse maximale.
        //J'ajoute la valuer de mon vecteur velocity a mon transform.position, afin de me deplacer.
        if (p_Direction != Vector3.zero)
        {
            transform.forward = l_DesireDirection;
            m_Velocity += m_AccRatePerSec * Time.deltaTime;
            m_Velocity = Mathf.Min(m_Velocity, m_MaxSpeed);
            //Collider[] hitCollider = Physics.OverlapBox(gameObject.transform.position, Extents, transform.rotation, m_LayerDeplacement);

            //if (hitCollider.Length >= 1)
            //{
            //    Debug.Log("je touche : " + hitCollider[0].name);
            //    transform.forward = l_RotationSave;
            //}

            float l_CastDist = l_DesireDirection.magnitude * Time.deltaTime * m_Velocity;
            if (!Physics.CapsuleCast(PointStartCapsule, PointEndCapsule, m_CapsuleCollider.radius, transform.forward, out RaycastHit hitInfo, l_CastDist, m_LayerDeplacement))
            {
                Vector3 lastPosition = transform.position;
                transform.position += forward * l_CastDist;
                
                Collider[] hitCollider2 = Physics.OverlapCapsule(PointStartCapsule,PointEndCapsule ,m_CapsuleCollider.radius,m_LayerDeplacement);
                if (hitCollider2.Length >= 1)
                {
                   Debug.Log("Inside wall");
                   transform.position = lastPosition;
                   if (Physics.Raycast(transform.position, transform.forward, out RaycastHit HitRay, l_CastDist, m_LayerDeplacement))
                   {
                       Vector3 closestPoint = m_Collider.ClosestPoint(HitRay.point);
                       transform.position = closestPoint - HitRay.normal * 1f;
                   }
                }
            }
            else
            {
                m_Velocity = 0;
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
            m_Velocity -= m_DecRatePerSec * Time.deltaTime;
            m_Velocity = Mathf.Max(m_Velocity, 0);

            transform.position += forward * m_Velocity * p_DeltaTime;
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
            m_JumpTimer += p_DeltaTime;



            if (Physics.CapsuleCast(PointStartCapsule, PointEndCapsule, m_CapsuleCollider.radius * 0.95f, Vector3.up, out RaycastHit hit, 1f, m_CollisionLayerDetection))
            {
                Vector3 l_TargetPositionCollision = transform.position;
                l_TargetPositionCollision.y = m_InitialPosPlayer.y + hit.distance + m_CapsuleCollider.height / 2;
                transform.position = l_TargetPositionCollision;
                StopJump();
            }
            else
            {
                //Je recupère la position du player en X et en Z, mais le Y est tjr égale a 0;
                //je récupère la position du player en ajoutant la valeur en Y par rapport a ma curve
                //J'ajoute cettes valeur a mon transform.position.
                Vector3 l_TargetPosition = transform.position;
                l_TargetPosition.y = m_InitialPosPlayer.y + m_JumpCurve.Evaluate(m_JumpTimer);
                transform.position = l_TargetPosition;
            }
        }
        //Else is character not jumping
        else
        {
            float castDist = Mathf.Abs(m_YVelocity) * p_DeltaTime + .1f;
            m_coucoutoi = castDist;
            if (Physics.CapsuleCast(PointStartCapsule, PointEndCapsule, m_CapsuleCollider.radius * 0.95f, Vector3.down, out RaycastHit m_Hit, castDist, m_CollisionLayerDetection))
            {
                if (!m_IsOnTheFloor)
                {
                    Vector3 targetPosition = transform.position;
                    targetPosition.y = m_Hit.point.y + m_CapsuleCollider.bounds.extents.y;
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
        if (m_PlayerInput.Jumping && m_IsOnTheFloor)
        {
            m_IsOnTheFloor = false;
            m_IsJumping = true;
            m_InitialPosPlayer = transform.position;
            m_JumpTimer = 0f;
            m_YVelocity = 1f;
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
                transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.up * m_Height, 3 * Time.deltaTime);
            }
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }
    void CalculateForward()
    {
        if (!grounded)
        {
            forward = transform.forward;
            return;
        }
        forward = Vector3.Cross(l_hit.normal, -transform.right);
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

    public float Speed
    {
        get { return m_MaxSpeed; }
        set { m_MaxSpeed = value; }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, -transform.up * m_Height);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(PointStartCapsule, m_CapsuleCollider.radius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(PointEndCapsule, m_CapsuleCollider.radius);

        Gizmos.color = Color.white;
        Gizmos.DrawRay(transform.position, Vector3.up * 1f);
    }
}
