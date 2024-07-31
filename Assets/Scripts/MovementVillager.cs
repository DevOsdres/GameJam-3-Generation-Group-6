using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementVillager : MonoBehaviour
{
    public Transform point1; 
    public Transform point2; 
    public float speed = 10.0f;
    public float x, y;
    private Vector3 targetPosition; 
    private Animator anim;
    private bool isShooting = false;
    private Transform playerCamera; 
    private Quaternion initialRotation; 
    public GameObject bulletPrefab;    
    public Transform shootPoint; 
    private float shootingDelay = 1f;

    private void Start()
    {
        targetPosition = point2.position;
        anim = GetComponent<Animator>();

        playerCamera = Camera.main.transform;

        initialRotation = transform.rotation;
    }

    private void Update()
    {
        if (!isShooting && !anim.GetBool("isDead")) 
        {
            MoveVillager();
        }
    }

    private void MoveVillager()
    {
        // Mueve el aldeano hacia la posición objetivo
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Si el aldeano alcanza la posición objetivo, cambia la dirección
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            anim.SetFloat("VelX", x);
            anim.SetFloat("VelY", y);

            if (targetPosition == point1.position)
            {
                targetPosition = point2.position;
            }
            else
            {
                targetPosition = point1.position;
            }

            // Reproduce la animación de disparo si llega al point2
            if (targetPosition == point1.position)
            {
                PlayShootAnimation();
            }
            else
            {
                RotateVillager();
            }
        }
    }

        private void RotateVillager()
    {
        // Calcula la nueva rotación sumando 180 grados al ángulo actual en el eje Y
        float currentRotationY = transform.eulerAngles.y;
        float newRotationY = currentRotationY + 180f;

        transform.rotation = Quaternion.Euler(0f, newRotationY, 0f);
    }

    private void PlayShootAnimation()
    {
        FaceCamera();

        isShooting = true; // Detiene el movimiento mientras dispara
        anim.SetBool("IsShootingAnim", true); // Activa la animación de disparo
        Debug.Log("Started shooting animation"); // Depuración
        Invoke("StartShooting", 0.6f);

        // Disparar 2 balas en 2 segundos

        // Después de 2 segundos, continúa el ciclo (ajusta el tiempo según la duración de tu animación)
        Invoke("ContinueMovement", 2f);
    }
       private void StartShooting()
    {
        StartCoroutine(ShootMultipleBullets());
    }
      private IEnumerator ShootMultipleBullets()
    {
        // Dispara la primera bala
        ShootBullet();
        yield return new WaitForSeconds(shootingDelay); // Espera 0.5 segundos
        // Dispara la segunda bala
        ShootBullet();
    }

     private void ShootBullet()
    {
        // Asegúrate de que el prefab de la bala y el punto de disparo están asignados
        if (bulletPrefab != null && shootPoint != null)
        {
            // Instanciar la bala
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);

            // Obtener el script de la bala y configurarla para que apunte a la cámara
            BulletVill bulletScript = bullet.GetComponent<BulletVill>();
            if (bulletScript != null)
            {
                bulletScript.SetDirection((playerCamera.position - shootPoint.position).normalized);
            }
            else
            {
                Debug.LogError("El prefab de la bala no tiene un script 'BulletVill' adjunto.");
            }

            // Puedes agregar un tiempo de vida a la bala si lo deseas
            Destroy(bullet, 5f);
        }
        else
        {
            Debug.LogError("Prefab de bala o punto de disparo no están asignados.");
        }
    }


    private void ContinueMovement()
    {
        isShooting = false;
        anim.SetBool("IsShootingAnim", false); // Desactiva la animación de disparo
        Debug.Log("Stopped shooting animation"); // Depuración
        transform.rotation = initialRotation;

        RotateVillager();
    }
     private void FaceCamera()
    {
        // Calcula la dirección hacia la cámara
        Vector3 directionToCamera = playerCamera.position - transform.position;
        directionToCamera.y = 0; // Mantiene la rotación en el plano horizontal

        // Calcula la rotación deseada
        Quaternion targetRotation = Quaternion.LookRotation(directionToCamera);

        // Aplica la rotación
        transform.rotation = targetRotation;
    }
        private void OnDestroy()
    {
        // Detener el disparo cuando el aldeano es destruido
        CancelInvoke("ShootBullet");
    }
}