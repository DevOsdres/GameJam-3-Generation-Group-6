using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    public delegate void ObjectDestroyed();
    public static event ObjectDestroyed OnObjectDestroyed;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
            OnObjectDestroyed?.Invoke();
        }
    }
}
