using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSTrash : MonoBehaviour
{
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("!");
        if (collision.gameObject.tag == "IconDestructible")
        {
            DestructibleIconCollision(collision.gameObject);
        }
    }

    private void DestructibleIconCollision(GameObject icon)
    {
        // For now, just destroy it.
        Destroy(icon.transform.parent.gameObject);
    }
}
