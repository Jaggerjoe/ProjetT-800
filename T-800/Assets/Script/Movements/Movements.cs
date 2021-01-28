using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Movements : MonoBehaviour
{

    [SerializeField]
    private float m_Speed = 6;

    [SerializeField]
    private Rigidbody m_rb;
    public Vector2 movementInput = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Move(Vector2 _Direction, float _DeltaTime)
    {
        Vector3 l_CameraForward = Camera.main.transform.forward;
        Vector3 l_CameraRight = Camera.main.transform.right;

        l_CameraForward.y = 0f;
        l_CameraRight.y = 0f;
        l_CameraForward.Normalize();
        l_CameraRight.Normalize();

        Vector3 l_DesireDirection = _Direction.y * l_CameraForward + _Direction.x * l_CameraRight;
        l_DesireDirection.Normalize();

        if (l_DesireDirection != Vector3.zero)
        {
            transform.forward = l_DesireDirection;
        }
        //transform.position += l_DesireDirection * m_speed * p_deltaTime;
        transform.position += new Vector3(l_DesireDirection.x * m_Speed * Time.deltaTime, 0, l_DesireDirection.z * m_Speed*Time.deltaTime);

        //_Direction.Normalize();
        //Vector3 movement = new Vector3(_Direction.x, 0f, _Direction.y);
        //transform.position += movement * m_Speed * _DeltaTime;
        //Debug.DrawRay(transform.position, _Direction, Color.cyan, .2f);
    }
}
