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
    private BoxCollider2D m_boxCollider;
    private Vector2 m_boxColliderHalfExtents;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
        m_boxCollider = GetComponent<BoxCollider2D>();
        m_boxColliderHalfExtents =
            new Vector2(m_boxCollider.size.x / 2f, m_boxCollider.size.y / 2f);

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Cursor")
        {
            ReflectCursor(collision.gameObject);
            Debug.Log("!");
        }
    }

    private void ReflectCursor(GameObject cursor)
    {
        // Interpolate along length of paddle and modify angle of deflection accordingly.
        // Somewhat arbitrary algorithm but good enough for experimentation.
        // Give the player a lot of control over ball movement.

        var cursorScript = cursor.GetComponent<OSCursor>();
        const float bounceNormalCeiling = 2f;
        
        if (m_orientation == PaddleOrientation.HORIZONTAL)
        {
            float cx = cursor.transform.position.x;
            float px = transform.position.x;
            float delta = Mathf.Clamp(cx - px, -1f * m_boxColliderHalfExtents.x,
                m_boxColliderHalfExtents.x);
            float deltaPercentage = delta / m_boxColliderHalfExtents.x;
            float adjustedOffset = deltaPercentage * bounceNormalCeiling;

            float collisionNormal = cursor.transform.position.y > transform.position.y ?
                1f : -1f;

            cursorScript.SetDirection(new Vector2(adjustedOffset, collisionNormal));
        }
        else if (m_orientation == PaddleOrientation.VERTICAL)
        {
            float cy = cursor.transform.position.y;
            float py = transform.position.y;
            float delta = Mathf.Clamp(cy - py, -1f * m_boxColliderHalfExtents.y,
                m_boxColliderHalfExtents.y);
            float deltaPercentage = delta / m_boxColliderHalfExtents.y;
            float adjustedOffset = deltaPercentage * bounceNormalCeiling;

            float collisionNormal = cursor.transform.position.x > transform.position.x ?
                1f : -1f;

            cursorScript.SetDirection(new Vector2(collisionNormal, adjustedOffset));
        }
    }

   
}
