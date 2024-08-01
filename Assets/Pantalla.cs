using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Pantalla : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Debug.Log("Bullet has collided with the screen!");

            // En caso de que se requiera saber que tipo de bala impacto
            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet != null)
            {
                switch (bullet.bulletType)
                {
                    case BulletType.Fire:
                        Debug.Log("The bullet is of type Fire.");
                        break;
                    case BulletType.Water:
                        Debug.Log("The bullet is of type Water.");
                        break;
                    case BulletType.Plant:
                        Debug.Log("The bullet is of type Plant.");
                        break;
                }
            }
        }
    }
}