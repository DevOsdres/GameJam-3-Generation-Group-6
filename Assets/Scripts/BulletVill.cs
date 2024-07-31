using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletVill : MonoBehaviour
{
    public float speed = 20.0f;
    private Vector3 direction;

    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection;
    }

    private void Update()
    {
        // Mueve la bala en la dirección establecida
        transform.position += direction * speed * Time.deltaTime;
    }
}