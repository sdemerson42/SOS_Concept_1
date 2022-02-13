using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSCursor : MonoBehaviour
{
    public float m_movementSpeed;
    public Color m_captureColor;

    private Rigidbody2D m_rigidBody;
    private Vector2 m_movementDirection;

    private SpriteRenderer m_spriteRenderer;

    private bool m_isCapturing = false;
    private GameObject m_capturedIcon = null;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        SetDirection(new Vector2(-1f, 1f));
    }
    
    private void Update()
    {
        CaptureLogic();
    }

    private void CaptureLogic()
    {
        m_isCapturing = Input.GetButton("Capture");
        if (m_isCapturing)
        {
            m_spriteRenderer.color = m_captureColor;
            if (m_capturedIcon != null)
            {
                m_capturedIcon.transform.Translate(
                    m_rigidBody.velocity * Time.deltaTime);
            }
        }
        else
        {
            m_spriteRenderer.color = Color.white;
            m_capturedIcon = null;
        }
    }

    public void SetDirection(Vector2 direction)
    {
        m_movementDirection = direction.normalized * m_movementSpeed;
        var currentVelocity = m_rigidBody.velocity;
        var velocityDelta = m_movementDirection - currentVelocity;
        m_rigidBody.AddForce(velocityDelta, ForceMode2D.Impulse);
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "IconDestructible")
        {
            DestructibleIconCollision(collision.gameObject);
        }
    }

    private void DestructibleIconCollision(GameObject icon)
    {
        
        if (!m_isCapturing || m_capturedIcon != null) return;

        m_capturedIcon = icon.transform.parent.gameObject;
    }

}
