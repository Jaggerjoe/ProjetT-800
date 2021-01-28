using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movements : MonoBehaviour
{

    [SerializeField]
    private float m_Speed = 6;

    public Vector2 movementInput = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Move(Vector2 _Direction, float _DeltaTime)
    {
        _Direction.Normalize();
        Vector3 movement = new Vector3(_Direction.x, 0f, _Direction.y);
        transform.position += movement * m_Speed * _DeltaTime;
        Debug.DrawRay(transform.position, _Direction, Color.cyan, .2f);
    }
}
