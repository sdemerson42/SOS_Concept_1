using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSCursor : MonoBehaviour
{
    public float m_movementSpeed;

    private Rigidbody2D m_rigidBody;
    private Vector2 m_movementDirection;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
        SetDirection(new Vector2(-1f, 1f));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    public void SetDirection(Vector2 direction)
    {
        m_movementDirection = direction.normalized * m_movementSpeed;
        var currentVelocity = m_rigidBody.velocity;
        var velocityDelta = m_movementDirection - currentVelocity;
        m_rigidBody.AddForce(velocityDelta, ForceMode2D.Impulse);
    }

}
