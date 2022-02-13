using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesktopSceneManager : MonoBehaviour
{
    public GameObject m_refPaddle;

    public Vector2 m_hPaddlePosition;
    public Vector2 m_vPaddlePosition;
    public float m_paddleInitialMovementSpeed = 20.0f;

    private List<GameObject> m_paddles;
    
    void Start()
    {
        m_paddles = new List<GameObject>();

        CreateSceneObjects();
    }

    void CreateSceneObjects()
    {
        // Logic will be fixed in code for now. In the future this should
        // be data-driven even for a small prototype.

        // Note that only objects that the SceneManager owns will be created here.

        var paddle = GameObject.Instantiate(
            m_refPaddle,
            new Vector3(m_hPaddlePosition.x, m_hPaddlePosition.y, 0f),
            Quaternion.Euler(0f, 0f, 0f));
        
        var paddleScript = paddle.GetComponent<OSPaddle>();
        paddleScript.m_orientation = OSPaddle.PaddleOrientation.HORIZONTAL;
        paddleScript.m_movementSpeed = m_paddleInitialMovementSpeed;
        
        m_paddles.Add(paddle);

        paddle = GameObject.Instantiate(
            m_refPaddle,
            new Vector3(m_vPaddlePosition.x, m_vPaddlePosition.y, 0f),
            Quaternion.Euler(0f, 0f, 90f));

        paddleScript = paddle.GetComponent<OSPaddle>();
        paddleScript.m_orientation = OSPaddle.PaddleOrientation.VERTICAL;
        paddleScript.m_movementSpeed = m_paddleInitialMovementSpeed;

        m_paddles.Add(paddle);
    }
    
    void Update()
    {
        
    }
}
