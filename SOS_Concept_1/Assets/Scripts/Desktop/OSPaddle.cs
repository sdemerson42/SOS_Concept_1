using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSPaddle : MonoBehaviour
{
    public enum PaddleOrientation
    {
        HORIZONTAL,
        VERTICAL
    };

    // Exposed fields for editor

    public float m_movementSpeed = 0.0f;
    public PaddleOrientation m_orientation;

    private Rigidbody2D m_rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();

        if (m_orientation == PaddleOrientation.HORIZONTAL)
        {
            m_rigidBody.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        }
        else if (m_orientation == PaddleOrientation.VERTICAL)
        {
            m_rigidBody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
    }

    
    // Update is called once per frame
    void Update()
    {
       
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        Vector2 moveForce = Vector2.zero;
        if (m_orientation == PaddleOrientation.HORIZONTAL)
        {
            float moveInput = Input.GetAxis("Horizontal");
            moveForce.x = moveInput * m_movementSpeed;
        }
        else if (m_orientation == PaddleOrientation.VERTICAL)
        {
            float moveInput = Input.GetAxis("Vertical");
            moveForce.y = moveInput * m_movementSpeed;
        }

        m_rigidBody.MovePosition(m_rigidBody.position + moveForce);
    }

}
