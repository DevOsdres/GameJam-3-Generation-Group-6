using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAtPlayer : MonoBehaviour
{
    
    public GameObject bulletPrefab; // Prefab de la bala
    public GameObject particleEffectPrefab; // Prefab del efecto de partículas
    public Transform shootPoint; // Punto desde donde se dispara la bala

    public float bulletSpeed = 20f; // Velocidad de la bala
    public float shootInterval = 2f; // Intervalo de tiempo entre disparos
    private float nextShootTime = 0f;

    private Transform playerCamera; // Referencia a la cámara del jugador

    private void Start()
    {
        // Obtener la referencia a la cámara del jugador
        playerCamera = Camera.main.transform;

    }

    private void Update()
    {
        if (Time.time >= nextShootTime)
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        // Calcula la dirección hacia la cámara del jugador
        Vector3 direction = (playerCamera.position - shootPoint.position).normalized;

        // Instancia la bala
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.LookRotation(direction));
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.velocity = direction * bulletSpeed; // Aplica velocidad a la bala

        // Instancia el efecto de partículas en el shootPoint
        Instantiate(particleEffectPrefab, shootPoint.position, Quaternion.identity);

        Debug.Log("Aldeano disparó al jugador.");

        // Actualiza el tiempo para el próximo disparo
        nextShootTime = Time.time + shootInterval;
    }
}